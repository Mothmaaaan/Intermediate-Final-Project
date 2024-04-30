using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement_Enemy : MonoBehaviour
{
    // Runtime
    protected Transform target;
    protected Rigidbody rb;

    [Header("Enemy Movement Attributes")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float moveThreshold;


    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        MoveEnemy();
    }

#region Enemy Movement
// Move this enemy towards the target.
    private void MoveEnemy(){
        // Get move direction.
        Vector3 rawMoveDirection = target.position - transform.position;

        // Fix the move direction to incorporate only x and z.
        Vector3 moveDirection = new Vector3(rawMoveDirection.x, 0, rawMoveDirection.z).normalized;

        // If we are out of the moveThreshold, keep moving towards player.
        if(rawMoveDirection.magnitude > moveThreshold)
            rb.velocity = (moveDirection * moveSpeed * Time.fixedDeltaTime) + new Vector3(0, rb.velocity.y, 0);
    }
#endregion
}
