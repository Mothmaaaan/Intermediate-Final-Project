using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground_LevelGeneration : MonoBehaviour
{
    // Runtime
    Vector3Int origin = Vector3Int.zero;

    [Header("Tilemaps")]
    [SerializeField] Tilemap groundTileMap;

    [Header("Tiles")]
    [SerializeField] Tile dirtTile;
    [SerializeField] Tile stoneTile;
    [SerializeField] Tile sandTile;
    [SerializeField] Tile sandstoneTile;

    [Header("Obstacles")]
    [SerializeField] Transform obstacleHolder;
    [SerializeField] GameObject rockObstacle;
    [SerializeField] GameObject cactusObstacle;

    [Header("Level Attributes")]
    [SerializeField] string biome;
    [SerializeField] int xSize;
    [SerializeField] int ySize;
    [SerializeField] int obstacleDensity;


    public void StartLevelGeneration(){
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
                GenerateObstacles(rockObstacle);
                break;
            case "Desert":
                GenerateGround(sandTile, sandstoneTile);
                GenerateObstacles(cactusObstacle);
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
        // Create a cube.
        GameObject groundCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        groundCube.AddComponent<BoxCollider>();

        // Update groundCube transform to fit our level.
        groundCube.transform.position = new Vector3(0, -0.51f, 0);
        groundCube.transform.rotation = Quaternion.Euler(0, 45, 0);
        groundCube.transform.localScale = new Vector3(xSize, 1, ySize);

        // Generate our ground with walls on the outer edge.
        for(int i=0; i<xSize; i++){
            for(int j=0; j<ySize; j++){
                // If we are on the edge of the map, add a wall tile.
                if(i == 0 || i == xSize - 1 || j == 0 || j == ySize - 1){
                    //obstacleTileMap.SetTile(origin + new Vector3Int(i, j), wallTile);
                }
                else{
                    groundTileMap.SetTile(origin + new Vector3Int(i, j), groundTile);
                }             
            }
        }
    }
#endregion

#region Obstacle Generator
// Generates obstacles for our level.
    private void GenerateObstacles(GameObject obstacle){
        // Determine how many tiles are in our level.
        float halfX = xSize / 2;
        float halfY = ySize / 2;
        print(halfX);
        print(halfY);

        for(int i=0; i<obstacleDensity; i++){
            GameObject thisObstacle = Instantiate(obstacle, Vector3.zero, quaternion.identity);

            thisObstacle.transform.SetParent(obstacleHolder);

            //thisObstacle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-xSize, xSize), 0.5f, UnityEngine.Random.Range(-ySize, ySize));
            thisObstacle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-halfX, halfX), 0.5f, UnityEngine.Random.Range(-halfY, halfY));

        }
    }
#endregion
}
