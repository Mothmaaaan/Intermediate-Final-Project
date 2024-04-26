using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Obstacle")){
            Destroy(other.gameObject);
        }
    }
}
