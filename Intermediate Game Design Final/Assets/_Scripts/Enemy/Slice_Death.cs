using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice_Death : MonoBehaviour
{
    [Header("Sprite Halves")]
    [SerializeField] SpriteRenderer sprTop;
    [SerializeField] SpriteRenderer sprBot;
    [SerializeField] Rigidbody rbTop;
    [SerializeField] Rigidbody rbBot;

    [Header("Death Settings")]
    [SerializeField] float torqueStrength;
    [SerializeField] float forceStrength;
    [SerializeField] float disappearTime;


    private void Awake() {
        forceStrength = Random.Range(0f, 2f);
        torqueStrength = Random.Range(0f, 2f);

        rbTop.AddTorque(Vector3.right * torqueStrength, ForceMode.Impulse);
        rbTop.AddForce(Vector3.up * forceStrength, ForceMode.Impulse);

        rbBot.AddTorque(Vector3.right * -torqueStrength, ForceMode.Impulse);
        rbTop.AddForce(Vector3.back * forceStrength, ForceMode.Impulse);

        StartCoroutine(DeathDisappear());
    }

#region Disappear Effect
    IEnumerator DeathDisappear(){
        Color startColor = new Color(1, 1, 1, 1);
        Color targetColor = new Color(1, 1, 1, 0);

        float lerpTime = 0;
        
        // Lerp to target color.
        while(lerpTime < disappearTime){
            sprTop.color = Color.Lerp(startColor, targetColor, lerpTime / disappearTime);
            sprBot.color = Color.Lerp(startColor, targetColor, lerpTime / disappearTime);
            lerpTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
#endregion
}
