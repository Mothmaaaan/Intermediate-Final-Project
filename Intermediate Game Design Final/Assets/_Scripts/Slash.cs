using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Slash : Attack
{
    [Header("Slash Graphic")]
    [SerializeField] GameObject slashGraphic;
    [SerializeField] float graphicTime;
    [SerializeField] float graphicLength;

    protected override void PerformAttack(){
        print("Slash attack!");

        if(mPlayer.GetFacingRight()){
            AttackHitbox(transform.position + (Vector3.right * length));
            StartCoroutine(SlashGraphic(transform.position + Vector3.right * graphicLength, true));
        }   
        else{
            AttackHitbox(transform.position + (-Vector3.right * length));   
            StartCoroutine(SlashGraphic(transform.position + -Vector3.right * graphicLength, false));
        }            
    }

#region Attack Hitbox
// Create attack hitbox.
    private void AttackHitbox(Vector3 pos){
        Collider[] cols = Physics.OverlapSphere(pos, radius, enemyLayer);

        for(int i=0; i<cols.Length; i++){
            cols[i].GetComponent<Health_Enemy>().TakeDamage(damage);
        }
    }
#endregion

#region Slash Graphic
// Make the slash graphic slowly disappear.
    IEnumerator SlashGraphic(Vector3 pos, bool isFacingRight){
        SpriteRenderer thisSlash = Instantiate(slashGraphic, pos, slashGraphic.transform.rotation).GetComponent<SpriteRenderer>();

        // Fix the direction of the slash by multiplying scale by -1.
        if(!isFacingRight)
            thisSlash.transform.localScale *= -1;

        Color startColor = new Color(1, 1, 1, 1);
        Color targetColor = new Color(1, 1, 1, 0);

        float lerpTime = 0;
        
        // Lerp to target color.
        while(lerpTime < graphicTime){
            thisSlash.color = Color.Lerp(startColor, targetColor, lerpTime / graphicTime);
            lerpTime += Time.deltaTime;
            yield return null;
        }
        Destroy(thisSlash.gameObject);
    }

#endregion

    // Debugging.
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + (Vector3.right * length), radius);
    }
}
