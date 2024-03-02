using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Player : MonoBehaviour
{
    // Runtime
    PlayerControls pControls;
    bool isPaused = false;

    [Header("Input Containers")]
    [SerializeField] Vector2 rawMoveDirection;

    [Header("Pause Text")]
    [SerializeField] GameObject pauseText;


    private void Awake() {
        pControls = new PlayerControls();

        // Get move direction
        pControls.Gameplay.Movement.performed += ctx => rawMoveDirection = ctx.ReadValue<Vector2>();

        // Pausing
        pControls.Gameplay.Pause.started += ctx => TogglePause();
    }

#region Getters
// Get raw move direction.
    public Vector2 GetRawMoveDirection(){
        return rawMoveDirection;
    }

// Get isPaused.
    public bool GetIsPaused(){
        return isPaused;
    }
#endregion

#region Input Functions
// Toggle pause.
    private void TogglePause(){
        isPaused = !isPaused;

        if(isPaused){
            Time.timeScale = 0;
            pauseText.SetActive(true);
        }
        else{
            Time.timeScale = 1;
            pauseText.SetActive(false);
        } 
    }
#endregion

#region Enable/Disable
    private void OnEnable() {
        pControls.Enable();
    }

    private void OnDisable() {
        pControls.Disable();
    }
#endregion
}
