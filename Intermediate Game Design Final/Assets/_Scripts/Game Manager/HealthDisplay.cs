using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    // Runtime
    TextMeshProUGUI healthText;
    Health_Player pHealth;


    private void Awake() {
        pHealth = GetComponentInParent<Health_Player>();

        healthText = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        DisplayHealth();
    }

#region Display Health
// Grab the players health and display through healthText;
    private void DisplayHealth(){
        healthText.text = pHealth.GetCurrentHealth().ToString();
    }

#endregion
}
