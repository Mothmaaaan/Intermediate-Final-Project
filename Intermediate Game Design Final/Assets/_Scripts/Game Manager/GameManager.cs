using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Runtime
    Class playerClass;
    ClassList cList;
    bool timeGame = false;

    [Header("Player Creation")]
    [SerializeField] GameObject player;
    [SerializeField] Vector3 spawnPosition;

    [Header("Class Attributes")]
    [SerializeField] TextMeshProUGUI labelText;

    [Header("Game Timer")]
    [SerializeField] float gameTimer;


    private void Awake() {
        cList = GetComponent<ClassList>();
    }

    private void Update() {
        if(timeGame){
            gameTimer += Time.deltaTime;
        }
    }

#region Create Player
// Instantiate player prefab.
    public void CreatePlayer(){
        GameObject thisPlayer = Instantiate(player, spawnPosition, quaternion.identity);

        AdoptClass(thisPlayer);
    }

// Set up character stats depending on class.
    public void AdoptClass(GameObject thisPlayer){
        thisPlayer.GetComponent<Class_Player>().SetUpCharacter(playerClass);
    }    
#endregion

#region Get Class
// Get the currently selected class from the dropdown.
    public void GetClass(){
        playerClass = cList.SearchForClass(labelText.text);
    }
#endregion

#region Game Timer
// Start the game timer!
    public void StartGameTimer(){
        timeGame = true;
    }

// Stop the game timer...
    public void StopGameTimer(){
        timeGame = false;
    }    

// Get the value of the game timer.
    public float GetGameTimer(){
        return gameTimer;
    }
#endregion
}
