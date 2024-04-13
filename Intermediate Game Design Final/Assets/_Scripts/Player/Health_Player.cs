using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Health_Player : MonoBehaviour
{
    [Header("Heath")]
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    
    [Header("Damage Flash Time")]
    [SerializeField] float flashTime;
    [SerializeField] int numFlashes;

    [Header("Sprite Renderers")]
    [SerializeField] SpriteRenderer leftSprite;
    [SerializeField] SpriteRenderer rightSprite;


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
// Take Damage if we hit an enemy.
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy")){
            TakeDamage(5, "Physical");
        }

        if(other.CompareTag("Magic Enemy")){
            TakeDamage(5, "Magic");
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
        Color startColor = new Color(1, 1, 1, 1);
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
            leftSprite.color = Color.Lerp(startColor, targetColor, lerpTime / flashTime);
            rightSprite.color = Color.Lerp(startColor, targetColor, lerpTime / flashTime);
            lerpTime += Time.deltaTime;
            yield return null;
        }
        leftSprite.color = targetColor;
        rightSprite.color = targetColor;

        lerpTime = 0;
        // Lerp back to start color.
        while(lerpTime < flashTime){
            leftSprite.color = Color.Lerp(targetColor, startColor, lerpTime / flashTime);
            rightSprite.color = Color.Lerp(targetColor, startColor, lerpTime / flashTime);
            lerpTime += Time.deltaTime;
            yield return null;
        }
        leftSprite.color = startColor;
        rightSprite.color = startColor;

        flashes++;

        if(flashes < numFlashes)
            StartCoroutine(DamageFlash(damageType, flashes));       
    }
#endregion
}
