using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground_LevelGeneration : MonoBehaviour
{
    // Runtime
    Vector3Int origin = Vector3Int.zero;

    [Header("Tilemaps")]
    [SerializeField] Tilemap groundTileMap;
    [SerializeField] Tilemap obstacleTileMap;

    [Header("Tiles")]
    [SerializeField] Tile dirtTile;
    [SerializeField] Tile stoneTile;
    [SerializeField] Tile rockTile;
    [SerializeField] Tile sandTile;
    [SerializeField] Tile sandstoneTile;
    [SerializeField] Tile cactusTile;

    [Header("Level Attributes")]
    [SerializeField] string biome;
    [SerializeField] int xSize;
    [SerializeField] int ySize;
    [SerializeField] int obstacleDensity;


    private void Awake() {
        // We want to start at the bottom left of our level, or
        // half of our width and length subtracted.
        origin = new Vector3Int(-(xSize / 2), -(ySize / 2));

        DetermineBiome();
    }

#region Determine Biome
// Determines what biome to generate based on the biome string.
    private void DetermineBiome(){
        switch(biome){
            case "Dirt":
                GenerateGround(dirtTile, stoneTile);
                GenerateObstacles(rockTile);
                break;
            case "Desert":
                GenerateGround(sandTile, sandstoneTile);
                GenerateObstacles(cactusTile);
                break;
            default:
                print("Cannot read biome.");
                break;
        }
    }
#endregion

#region Ground Generator
// Generates the ground layer of our level.
    private void GenerateGround(Tile groundTile, Tile wallTile){
        // Generate our grond with walls on the outer edge.
        for(int i=0; i<xSize; i++){
            for(int j=0; j<ySize; j++){
                // If we are on the edge of the map, add a wall tile.
                if(i == 0 || i == xSize - 1 || j == 0 || j == ySize - 1)
                    obstacleTileMap.SetTile(origin + new Vector3Int(i, j), wallTile);
                else
                    groundTileMap.SetTile(origin + new Vector3Int(i, j), groundTile);
            }
        }
    }
#endregion

#region Obstacle Generator
// Generates obstacles for our level.
    private void GenerateObstacles(Tile obstacleTile){
        // Determine how many tiles are in our level.
        int numGroundTiles = (xSize - 2) * (ySize - 2);

        for(int i=0; i<obstacleDensity; i++){
            obstacleTileMap.SetTile(origin + new Vector3Int(Random.Range(1, xSize - 1),Random.Range(1, xSize - 1)), obstacleTile);
        }
    }
#endregion
}
