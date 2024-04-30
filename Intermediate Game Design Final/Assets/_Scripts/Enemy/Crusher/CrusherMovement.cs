using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherMovement : MonoBehaviour
{
    // Runtime
    Transform target;
    Rigidbody rb;
    bool move = true;

    [Header("Crusher Movement Attributes")]
    [SerializeField] float moveSpeed;
    [SerializeField] float moveThreshold;
    [SerializeField] float crushSpeed;
    [SerializeField] float riseDuration;
    [SerializeField] float riseHeight;
    [SerializeField] float groundTime;
    [SerializeField] float restTime;
    [SerializeField] float crushTime;

    [Header("Crush Trigger")]
    [SerializeField] Collider crushCol;

    [Header("Animator")]
    [SerializeField] Animator crusherAnim;


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
        Vector3 moveDirection = new Vector3(rawMoveDirection.x, 0, rawMoveDirection.z);

        // If we are out of the moveThreshold, keep moving towards player.
        if(moveDirection.magnitude > moveThreshold)
            rb.velocity = (moveDirection.normalized * moveSpeed * Time.fixedDeltaTime) + new Vector3(0, rb.velocity.y, 0);
        else{
            rb.velocity = Vector3.zero;
            
            // START CRUSHING!!!
            move = false;
            StartCoroutine(Crush());
        }
    }
#endregion

#region CRUSH
// Start the crush!
    IEnumerator Crush(){
        crusherAnim.SetBool("charging", true);

        yield return new WaitForSeconds(crushTime);

        crusherAnim.SetBool("charging", false);     

        // Death collider right here for a frame.
        crushCol.enabled = true;

        transform.position = new Vector3(transform.position.x, 0.76f, transform.position.z);

        crushCol.enabled = false;

        StartCoroutine(Rise());
    }

    IEnumerator Rise(){
        yield return new WaitForSeconds(groundTime);

        float time = 0;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, riseHeight, transform.position.z);

        while(time < riseDuration){
            transform.position = Vector3.Lerp(startPosition, endPosition, time / riseDuration);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;

        yield return new WaitForSeconds(restTime);

        move = true;
    }

#endregion
}
