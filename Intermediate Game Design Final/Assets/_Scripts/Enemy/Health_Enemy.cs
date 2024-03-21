using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health_Enemy : MonoBehaviour
{
    [Header("Heath")]
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    [Header("Death Effect")]
    [SerializeField] GameObject deathEffect;


#region Set Health
// Set our health.
    public void SetHealth(float health){
        maxHealth = health;
        currentHealth = maxHealth;
    }

// Get our current health.
    public float GetCurrentHealth(){
        return currentHealth;
    }
#endregion

#region Take Damage
// Applies damage to health.
    public void TakeDamage(float damage){
        currentHealth -= damage;

        // Check for death!
        if(currentHealth <= 0){
            // Die.
            Instantiate(deathEffect, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }
#endregion
}
