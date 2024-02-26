using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class_Player : MonoBehaviour
{
    // Runtime
    Health_Player pHealth;
    Movement_Player pMovement;

    [Header("Default Character")]
    [SerializeField] string name;
    [SerializeField] float moveSpeed;
    [SerializeField] float health;


    private void Awake() {
        pHealth = GetComponent<Health_Player>();
        pMovement = GetComponent<Movement_Player>();

        SetUpCharacter();
    }

#region Character Setup
// Set up our character. (THIS IS WHERE WE PASS THROUGH A CHARACTER REFERENCE.)
    private void SetUpCharacter(){
        pHealth.SetHealth(health);
        pMovement.SetMoveSpeed(moveSpeed);
    }
#endregion
}
