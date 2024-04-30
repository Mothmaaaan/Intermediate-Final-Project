using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PowerUpSpawning : MonoBehaviour
{
    public LevelSettings lSettings;
    public int halfX;
    public int halfY;

    [SerializeField] GameObject[] powerups;

    [Header("Powerup Cooldown")]
    [SerializeField] float powerupCooldown;
    private float cooldown;

    private void OnEnable() {
        halfX = lSettings.levelSize / 2;
        halfY = lSettings.levelSize / 2;

        cooldown = powerupCooldown;
    }

    private void Update() {
        if(cooldown <= 0){
            SpawnPowerup();

            cooldown = powerupCooldown;
        }

        cooldown -= Time.deltaTime;
    }

    private void SpawnPowerup(){
        Vector3 spawnPos = new Vector3((int)UnityEngine.Random.Range(-halfX, halfX), 10f, (int)UnityEngine.Random.Range(-halfY, halfY));

        int randIndex = UnityEngine.Random.Range(0, 3);
        Instantiate(powerups[randIndex], spawnPos, Quaternion.identity);
    }
}
