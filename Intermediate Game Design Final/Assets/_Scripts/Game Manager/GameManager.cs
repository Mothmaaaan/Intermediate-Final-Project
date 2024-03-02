using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player Creation")]
    [SerializeField] GameObject player;
    [SerializeField] Vector3 spawnPosition;


#region Create Player
// Instantiate player prefab.
    public void CreatePlayer(){
        GameObject thisPlayer = Instantiate(player, spawnPosition, quaternion.identity);
    }
#endregion
}
