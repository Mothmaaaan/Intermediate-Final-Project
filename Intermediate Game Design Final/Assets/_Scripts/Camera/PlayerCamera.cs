using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Runtime
    bool canFollow = false;

    [Header("Player/Target")]
    [SerializeField] Transform target;

    [Header("Camera Attributes")]
    [SerializeField] float height;
    [SerializeField] float distance;
    [SerializeField] float offset;
    [SerializeField] float smoothAmount;
    Vector3 refVelocity = Vector3.zero;


    private void Update() {
        if(canFollow && target)
            SmoothCameraFollow();
    }

#region Smooth Camera Follow
// Follow the target.
    private void SmoothCameraFollow(){
        // Create target camera position.
        Vector3 targetPosition = target.position + new Vector3(offset, height, distance);

        // Camera look at player.
        transform.LookAt(target);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref refVelocity, smoothAmount);
    }
#endregion

#region Toggle Can Follow
// Determine if the camera should follow the player or not.
    public void ToggleCanFollow(bool value){
        canFollow = value;
    }
#endregion

#region Find Player
// Find the player object.
    public void FindPlayer(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
#endregion
}
