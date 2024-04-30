using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycast : MonoBehaviour
{
    
    public float length;
    public LayerMask mask;
    public GameObject target;
    RaycastHit hit;
    Ray raycast;

    void Update()
    {
        if(Physics.Raycast(transform.position, -transform.up, out hit, length, mask)){
            target = hit.transform.gameObject;
        }else{
            target = null;
        }
    }
}
