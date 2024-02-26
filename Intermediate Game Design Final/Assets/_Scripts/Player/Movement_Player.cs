using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Movement_Player : MonoBehaviour
{
    // Runtime
    Input_Player pInput;
    Rigidbody2D rb;
    bool facingRight = true;

    [Header("Movement Attributes")]
    [SerializeField] float moveSpeed;


    private void Awake() {
        pInput = GetComponent<Input_Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if((rb.velocity.x > 0 && !facingRight) || (rb.velocity.x < 0 && facingRight))
            Flip();
    }

    private void FixedUpdate() {
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
        rb.velocity = pInput.GetRawMoveDirection() * moveSpeed * Time.fixedDeltaTime;
    }

// Flip our sprite.
    private void Flip(){
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        facingRight = !facingRight;
    }
#endregion
}
