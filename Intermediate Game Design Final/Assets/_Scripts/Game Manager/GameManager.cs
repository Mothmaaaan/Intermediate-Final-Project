using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Runtime
    Class playerClass;
    ClassList cList;
    bool timeGame = false;
    GameObject thisPlayer;
    Health_Player pHealth;
    Movement_Player pMovement;

    [Header("Player Creation")]
    [SerializeField] GameObject player;
    [SerializeField] Vector3 spawnPosition;

    [Header("Class Attributes")]
    [SerializeField] TextMeshProUGUI labelText;

    [Header("Game Timer")]
    [SerializeField] float gameTimer;

    [Header("Lose Menu")]
    [SerializeField] GameObject loseMenu;

    public delegate void PlayerEvent();
    public event PlayerEvent OnKillPlayer;


    private void Awake() {
        cList = GetComponent<ClassList>();
    }

    private void Update() {
        if(timeGame){
            gameTimer += Time.deltaTime;
        }

        // This is where we want to kill the player.
        if(thisPlayer && pHealth.GetCurrentHealth() <= 0 && timeGame){
            KillPlayer();
        }
    }

#region Create Player
// Instantiate player prefab.
    public void CreatePlayer(){
        thisPlayer = Instantiate(player, spawnPosition, quaternion.identity);
        pHealth = thisPlayer.GetComponent<Health_Player>();
        pMovement = thisPlayer.GetComponent<Movement_Player>();

        // Subscribe disable methods to event.
        OnKillPlayer += pMovement.DisableGraphics;
        OnKillPlayer += pMovement.DisableMovement;

        AdoptClass(thisPlayer);
    }

// Set up character stats depending on class.
    public void AdoptClass(GameObject thisPlayer){
        thisPlayer.GetComponent<Class_Player>().SetUpCharacter(playerClass);
    }
#endregion

#region Destroy Player
// Kills the player.
    public void KillPlayer(){
        OnKillPlayer.Invoke();
        //OnKillPlayer -= pMovement.DisableGraphics;
        //OnKillPlayer -= pMovement.DisableMovement;

        loseMenu.SetActive(true);
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

#region Scene Management
// Reload the current scene.
    public void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
#endregion
}
