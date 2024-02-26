using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Player : MonoBehaviour
{
    // Runtime
    PlayerControls pControls;

    [Header("Input Containers")]
    [SerializeField] Vector2 rawMoveDirection;


    private void Awake() {
        pControls = new PlayerControls();

        // Get move direction
        pControls.Gameplay.Movement.performed += ctx => rawMoveDirection = ctx.ReadValue<Vector2>();
    }

#region Getters
// Get raw move direction.
    public Vector2 GetRawMoveDirection(){
        return rawMoveDirection;
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
