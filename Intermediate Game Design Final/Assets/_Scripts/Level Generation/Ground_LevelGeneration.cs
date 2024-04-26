using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground_LevelGeneration : MonoBehaviour
{
    // Runtime
    Vector3Int origin = Vector3Int.zero;
    public LevelSettings lSettings;

    [Header("Camera")]
    [SerializeField] Camera mainCam;
    [SerializeField] Color lightColor;
    [SerializeField] Color darkColor;

    [Header("Tilemaps")]
    [SerializeField] Tilemap groundTileMap;

    [Header("Tiles")]
    [SerializeField] Tile lightTile;
    [SerializeField] Tile darkTile;

    [Header("Obstacles")]
    [SerializeField] Transform obstacleHolder;
    [SerializeField] GameObject rockObstacle;
    [SerializeField] GameObject cubeObstacle;
    [SerializeField] LayerMask obstacleLayer;

    [Header("Level Attributes")]
    [SerializeField] string biome;
    [SerializeField] int xSize;
    [SerializeField] int ySize;
    [SerializeField] int obstacleDensity;
    [SerializeField] int obstacleTreeSize;

    [Header("Cube Materials")]
    [SerializeField] Material[] darkCubeMats;
    [SerializeField] Material[] lightCubeMats;
    [SerializeField] Material darkGround;
    [SerializeField] Material lightGround;


    public void StartLevelGeneration(){
        // Grab our settings.
        xSize = lSettings.levelSize;
        ySize = lSettings.levelSize;
        obstacleDensity = lSettings.obstacleDensity;
        obstacleTreeSize = lSettings.obstacelSize;

        // We want to start at the bottom left of our level, or
        // half of our width and length subtracted.
        origin = new Vector3Int(-(xSize / 2), -(ySize / 2));

        DetermineBiome();
    }

#region Determine Biome
// Determines what biome to generate based on the biome string.
    private void DetermineBiome(){
        switch(biome){
            case "Light":
                GenerateGround(lightTile, lightGround);
                GenerateObstacles(cubeObstacle, lightCubeMats);
                mainCam.backgroundColor = lightColor;
                break;
            case "Dark":
                GenerateGround(darkTile, darkGround);
                GenerateObstacles(cubeObstacle, darkCubeMats);
                mainCam.backgroundColor = darkColor;
                break;
            default:
                print("Cannot read biome.");
                break;
        }
    }
#endregion

#region Ground Generator
// Generates the ground layer of our level.
    private void GenerateGround(Tile groundTile, Material mat){
        // Create a cube.
        GameObject groundCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //groundCube.AddComponent<BoxCollider>();

        // Update groundCube transform to fit our level.
        groundCube.transform.position = new Vector3(0, -0.51f, 0);
        groundCube.transform.rotation = Quaternion.Euler(0, 45, 0);
        groundCube.transform.localScale = new Vector3(xSize, 1, ySize);
        groundCube.layer = 10;
        groundCube.GetComponent<MeshRenderer>().material = mat;

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
    private void GenerateObstacles(GameObject obstacle, Material[] mats){
        // Determine how many tiles are in our level.
        float halfX = xSize / 2;
        float halfY = ySize / 2;
        print(halfX);
        print(halfY);

        GameObject thisObstacle;
        int colorRandom;

        for(int i=0; i<obstacleDensity; i++){
            // Create new obstacle and parent to holder.
            thisObstacle = Instantiate(obstacle, Vector3.zero, obstacle.transform.rotation);
            thisObstacle.transform.SetParent(obstacleHolder);

            //thisObstacle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-xSize, xSize), 0.5f, UnityEngine.Random.Range(-ySize, ySize));
            thisObstacle.transform.localPosition = new Vector3((int)UnityEngine.Random.Range(-halfX, halfX) + 0.5f, 0.5f, (int)UnityEngine.Random.Range(-halfY, halfY) + 0.5f);

            // Get random color cube.
            colorRandom = UnityEngine.Random.Range(0, 3);
            thisObstacle.GetComponent<MeshRenderer>().material = mats[colorRandom];

            for(int j=0; j<obstacleTreeSize; j++){
                // Create new obstacle and parent to holder.
                thisObstacle = Instantiate(obstacle, thisObstacle.transform.position, thisObstacle.transform.rotation);
                thisObstacle.transform.SetParent(obstacleHolder);

                // Get random color cube.
                thisObstacle.GetComponent<MeshRenderer>().material = mats[colorRandom];

                // Move a random direction.
                int random = UnityEngine.Random.Range(0, 6);
                switch(random){
                    case 0:
                        thisObstacle.transform.localPosition += Vector3.forward;
                        break;
                    case 1:
                        thisObstacle.transform.localPosition += -Vector3.forward;
                        break;
                    case 2:
                        thisObstacle.transform.localPosition += Vector3.right;
                        break;
                    case 3:
                        thisObstacle.transform.localPosition += -Vector3.right;
                        break;
                    case 4:
                        thisObstacle.transform.localPosition += Vector3.up;
                        break;
                    case 5:
                        thisObstacle.transform.localPosition += Vector3.up;
                        break;
                }
            }
        }
    }
#endregion
}
