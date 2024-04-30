using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlerMovement : MonoBehaviour
{
    // Runtime
    protected Transform target;
    protected Rigidbody rb;
    bool move = true;

    [Header("Enemy Movement Attributes")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float moveThreshold;

    [Header("Bowl Tweaks")]
    [SerializeField] float chargeSpeed;
    [SerializeField] float bowlCooldown;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform bowlPoint;
    [SerializeField] float bowlStrength;


    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if(move)
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
        if(rawMoveDirection.magnitude > moveThreshold){
            rb.velocity = (moveDirection * moveSpeed * Time.fixedDeltaTime) + new Vector3(0, rb.velocity.y, 0);
        }else{
            move = false;
            StartCoroutine(Bowl());
        }
            
    }
#endregion

#region Bowl Projectile
// Bowl a projectile towards the player.
    IEnumerator Bowl(){
        // Start Bowl animation.

        yield return new WaitForSeconds(chargeSpeed);

        // Stop Bowl animation.

        // Lob this projectile.
        GameObject thisProjectile = Instantiate(projectile, bowlPoint.position, bowlPoint.rotation);
        Rigidbody pRB = thisProjectile.GetComponent<Rigidbody>();
        pRB.AddForce(PointTowardsPlayer() * bowlStrength, ForceMode.Impulse);

        yield return new WaitForSeconds(bowlCooldown);

        move = true;
    }

    private Vector3 PointTowardsPlayer(){
        Vector3 rawProjectileDirection = target.position - transform.position;
        return new Vector3(rawProjectileDirection.x, 0, rawProjectileDirection.z).normalized;
    }

#endregion
}
