using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyEnemies : MonoBehaviour
{
    [Header("Enemy List")]
    [SerializeField] List<GameObject> nearbyEnemies = new List<GameObject>();


    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy")){
            nearbyEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Enemy") && nearbyEnemies.Contains(other.gameObject)){
            nearbyEnemies.Remove(other.gameObject);
        }
    }
}
