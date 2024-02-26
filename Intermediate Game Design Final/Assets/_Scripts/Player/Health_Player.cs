using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Player : MonoBehaviour
{
    // Runtime
    SpriteRenderer playerSprite;

    [Header("Heath")]
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    
    [Header("Damage Flash Time")]
    [SerializeField] float flashTime;
    [SerializeField] int numFlashes;


    private void Awake() {
        playerSprite = GetComponent<SpriteRenderer>();
    }

#region Set Health
// Set our health.
    public void SetHealth(float health){
        maxHealth = health;
        currentHealth = maxHealth;
    }
#endregion

#region Take Damage
// Take Damage if we hit an enemy.
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            TakeDamage(5, "Physical");
            Destroy(other.gameObject);
        }

        if(other.CompareTag("Magic Enemy")){
            TakeDamage(5, "Magic");
            Destroy(other.gameObject);
        }           
    }

// Applies damage to health and initiates damage flash.
    private void TakeDamage(float damage, string damageType){
        currentHealth -= damage;

        StartCoroutine(DamageFlash(damageType, 0));
    }

#endregion

#region Damage Flash
// Perform a damage flash depending on what kind of damagae.
    IEnumerator DamageFlash(string damageType, int flashes){
        Color startColor = playerSprite.color;
        Color targetColor = new Color(1, 0, 0, 1);

        switch(damageType){
            case "Physical":
                targetColor = new Color(1, 0, 0, 1);
                break;
            case "Magic":
                targetColor = new Color(0, 0, 1, 1);
                break;
        }

        float lerpTime = 0;
        
        // Lerp to target color.
        while(lerpTime < flashTime){
            playerSprite.color = Color.Lerp(startColor, targetColor, lerpTime / flashTime);
            lerpTime += Time.deltaTime;
            yield return null;
        }
        playerSprite.color = targetColor;

        lerpTime = 0;
        // Lerp back to start color.
        while(lerpTime < flashTime){
            playerSprite.color = Color.Lerp(targetColor, startColor, lerpTime / flashTime);
            lerpTime += Time.deltaTime;
            yield return null;
        }
        playerSprite.color = startColor;

        flashes++;

        if(flashes < numFlashes)
            StartCoroutine(DamageFlash(damageType, flashes));
        
    }

#endregion
}
