using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Movement_Player : MonoBehaviour
{
    // Runtime
    Input_Player pInput;
    Rigidbody rb;
    bool facingRight = true;
    bool canMove = true;

    [Header("Movement Attributes")]
    [SerializeField] float moveSpeed;

    [Header("Player Graphics")]
    public Transform pGraphics;


    private void Awake() {
        pInput = GetComponent<Input_Player>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        // Fix this??
        if(canMove && ((pInput.GetRawMoveDirection().x > 0 && !facingRight) || (pInput.GetRawMoveDirection().x < 0 && facingRight)) && !pInput.GetIsPaused())
            Flip();
    }

    private void FixedUpdate() {
        if(canMove)
            MovePlayer();
    }

#region Set Move Speed
// Set our move speed.
    public void SetMoveSpeed(float moveSpeed){
        this.moveSpeed = moveSpeed;
    }
#endregion

#region Movement Methods
// Move player based on raw move direction.
    private void MovePlayer(){
        // Get raw move direction.
        Vector3 rawMoveDirection = pInput.GetRawMoveDirection();

        // Fix move direction for 3D space and account for current y velocity.
        Vector3 fixedMoveDirection = new Vector3(rawMoveDirection.x, 0, rawMoveDirection.y);

        rb.velocity = (fixedMoveDirection * moveSpeed * Time.fixedDeltaTime) + new Vector3(0, rb.velocity.y, 0);
    }

// Flip our sprite.
    private void Flip(){
        if(facingRight)
            pGraphics.rotation = Quaternion.Euler(0, 180, 0);
        else
            pGraphics.rotation = Quaternion.Euler(0, 0, 0);

        facingRight = !facingRight;
    }

// Get facingRight.
    public bool GetFacingRight(){
        return facingRight;
    }

// Disable Movement.
    public void DisableMovement(){
        canMove = false;
    }

// Disable Graphics.
    public void DisableGraphics(){
        pGraphics.gameObject.SetActive(false);
    }
#endregion
}
