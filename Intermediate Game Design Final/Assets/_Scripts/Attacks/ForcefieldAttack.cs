using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ForcefieldAttack : MonoBehaviour
{
    [Header("Forcefield")]
    [SerializeField] GameObject forcefield;
    [SerializeField] GameObject thisForcefield;
    [SerializeField] float radius;

    private void Update() {
        if(thisForcefield)
            thisForcefield.transform.position = transform.position;
    }

#region Powerup
// If we have this attack already, power it up!
    public void PowerUpAttack(){
        if(!thisForcefield){
            SetUpForcefield();
            return;
        }

        if(radius < 10){
            radius += 1;
            thisForcefield.transform.localScale = Vector3.one * radius;
        }
    }

// Set up our new forcefield attack.
    private void SetUpForcefield(){
        thisForcefield = Instantiate(forcefield, transform.position, quaternion.identity);
    }
#endregion
}
