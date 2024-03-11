using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    // Runtime
    TextMeshProUGUI timerText;
    GameManager gManager;


    private void Awake() {
        gManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        DisplayTimer();
    }

#region Display Timer
// Grab the time and display it through timerText;
    private void DisplayTimer(){
        float rawTime = gManager.GetGameTimer();
        
        int minutes = Mathf.FloorToInt(rawTime / 60f);
        int seconds = Mathf.FloorToInt(rawTime - minutes * 60);

        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

#endregion
}
