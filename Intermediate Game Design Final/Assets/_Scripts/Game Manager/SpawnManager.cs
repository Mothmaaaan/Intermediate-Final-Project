using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Runtime
    Transform playerTransform;
    float timer;

    [Header("Enemies")]
    [SerializeField] private GameObject crawler;

    [Header("Spawn Location Attributes")]
    [SerializeField] private float spawnDistance;

    [Header("Spawn Timing Attributes")]
    [SerializeField] private float timeBetweenSpawns;


    private void OnEnable() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        timer = timeBetweenSpawns;  
    }

    private void Update() {
        if(timer < 0){
            Spawn();

            timer = timeBetweenSpawns;
        }

        timer -= Time.deltaTime;
    }

#region Spawning
// Spawn an enemy when the timer is below 0.
    private void Spawn(){
        // Get a spawn position.
        Vector3 spawnPosition = GetSpawnPosition();

        // If our spawn position is on top of the player, do not spawn.
        if(spawnPosition == Vector3.zero){
            return;
        }

        // Spawn!
        GameObject thisSpawn = Instantiate(crawler, spawnPosition, quaternion.identity);
    }
#endregion

#region Get Spawn Position
// Return a valid spawn position.
    private Vector3 GetSpawnPosition(){
        // Get a random point inside of the unit circle.
        Vector2 randomPoint = UnityEngine.Random.insideUnitCircle;

        // Normalize that result and multiply by spawnDistance to get a point spawnDistance away from player.
        Vector2 normalizedPosition = randomPoint.normalized * spawnDistance;

        // Apply this position relative to the player.
        Vector3 spawnPosition = new Vector3(normalizedPosition.x, 0, normalizedPosition.y) + playerTransform.position;

        // Check to ensure this position is above ground.
        if(GroundCheck(spawnPosition)){
            return spawnPosition;
        }else{
            return Vector3.zero;
        }
    }

// Perform a ground check to ensure this spawn is on ground.
// THIS WORKS FOR NOW BUT COULD BE BETTER I THINK.
    private bool GroundCheck(Vector3 pos){
        if(Physics.CheckSphere(pos, 1)){
            return true;
        }else{
            return false;
        }
    }    
#endregion
}
