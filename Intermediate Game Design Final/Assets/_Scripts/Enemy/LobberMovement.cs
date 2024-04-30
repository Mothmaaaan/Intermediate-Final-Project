using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobberMovement : MonoBehaviour
{
    // Runtime
    protected Transform target;
    protected Rigidbody rb;
    bool move = true;

    [Header("Enemy Movement Attributes")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float moveThreshold;

    [Header("Lob Tweaks")]
    [SerializeField] float chargeSpeed;
    [SerializeField] float lobCooldown;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform lobPoint;
    [SerializeField] float lobStrength;


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
            StartCoroutine(Lob());
        }
            
    }
#endregion

#region Lob Projectile
// Lob a projectile towards the player.
    IEnumerator Lob(){
        // Start lob animation.

        yield return new WaitForSeconds(chargeSpeed);

        // Stop lob animation.

        // Lob this projectile.
        GameObject thisProjectile = Instantiate(projectile, lobPoint.position, lobPoint.rotation);
        Rigidbody pRB = thisProjectile.GetComponent<Rigidbody>();
        pRB.AddForce((PointTowardsPlayer() + Vector3.up) * lobStrength, ForceMode.Impulse);

        yield return new WaitForSeconds(lobCooldown);

        move = true;
    }

    private Vector3 PointTowardsPlayer(){
        Vector3 rawProjectileDirection = target.position - transform.position;
        return new Vector3(rawProjectileDirection.x, 0, rawProjectileDirection.z).normalized;
    }

#endregion
}
