using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class_Player : MonoBehaviour
{
    // Runtime
    Health_Player pHealth;
    Movement_Player pMovement;

    [Header("Default Character")]
    [SerializeField] string className;


    private void Awake() {
        pHealth = GetComponent<Health_Player>();
        pMovement = GetComponent<Movement_Player>();
    }

#region Character Setup
// Set up our character. (THIS IS WHERE WE PASS THROUGH A CLASS REFERENCE.)
    public void SetUpCharacter(Class thisClass){
        className = thisClass.name;
        pHealth.SetHealth(thisClass.health);
        pMovement.SetMoveSpeed(thisClass.speed);
    }
#endregion
}
