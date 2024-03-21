using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Attack : MonoBehaviour
{
    // Runtime
    protected Movement_Player mPlayer;

    [Header("Attack Attributes")]
    [SerializeField] protected float damage;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected float radius;
    [SerializeField] protected float length;

    [Header("Attack Cooldown")]
    [SerializeField] float attackCooldownLength;
    private float attackCooldown;


    private void Awake() {
        mPlayer = GetComponent<Movement_Player>();

        ResetCooldown();
    }

    private void Update() {
        if(attackCooldown <= 0){
            PerformAttack();

            ResetCooldown();
        }

        attackCooldown -= Time.deltaTime;
    }

#region Cooldown Functions
// Reset the cooldown.
    protected void ResetCooldown(){
        attackCooldown = attackCooldownLength;
    }    
#endregion

#region Attack Functions
// Perform the attack.
    protected virtual void PerformAttack(){
        
    }
#endregion
}
