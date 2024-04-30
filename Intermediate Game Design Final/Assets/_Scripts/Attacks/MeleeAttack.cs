using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    // Runtime
    Movement_Player mPlayer;

    public LayerMask enemyLayer;

    [Header("This Melee Attack")]
    [SerializeField] Melee thisMelee;
    [SerializeField] float damage;
    [SerializeField] float cooldown;

    [Header("Melee Graphic")]
    [SerializeField] GameObject meleeGraphic;
    [SerializeField] float graphicTime;
    [SerializeField] float graphicLength;
    [SerializeField] Color graphicColor;
    
    [Header("Melee Cooldown")]
    [SerializeField] float meleeCooldown;


    private void Awake() {
        mPlayer = GetComponent<Movement_Player>();

        SetUpMelee();
    }

    private void Update() {
        if(thisMelee.aName != "" && meleeCooldown <= 0){
            PerformMelee();

            meleeCooldown = cooldown;
        }

        if(thisMelee.aName != "")
            meleeCooldown -= Time.deltaTime;
    }

    private void PerformMelee(){
        if(thisMelee.aName == "")
            return;

        if(mPlayer.GetFacingRight()){
            AttackHitbox(transform.position + (Vector3.right * thisMelee.length));
            StartCoroutine(MeleeGraphic(transform.position + Vector3.right * graphicLength, true));
        }   
        else{
            AttackHitbox(transform.position + (-Vector3.right * thisMelee.length));   
            StartCoroutine(MeleeGraphic(transform.position + -Vector3.right * graphicLength, false));
        }             
    }

#region Attack Hitbox
// Create attack hitbox.
    private void AttackHitbox(Vector3 pos){
        Collider[] cols = Physics.OverlapSphere(pos, thisMelee.radius, enemyLayer);

        for(int i=0; i<cols.Length; i++){
            cols[i].GetComponent<Health_Enemy>().TakeDamage(damage);
            print(cols[i].name + " took " + damage + " damage!");
        }
    }
#endregion

#region Melee Graphic
// Make the melee graphic slowly disappear.
    IEnumerator MeleeGraphic(Vector3 pos, bool isFacingRight){
        SpriteRenderer thisMeleeSprite = Instantiate(meleeGraphic, pos, meleeGraphic.transform.rotation).GetComponent<SpriteRenderer>();

        // Fix the direction of the melee by multiplying scale by -1.
        if(!isFacingRight)
            thisMeleeSprite.transform.localScale *= -1;

        Color startColor = graphicColor;
        Color targetColor = new Color(graphicColor.r, graphicColor.g, graphicColor.b, 0);

        float lerpTime = 0;
        
        // Lerp to target color.
        while(lerpTime < graphicTime){
            thisMeleeSprite.color = Color.Lerp(startColor, targetColor, lerpTime / graphicTime);
            lerpTime += Time.deltaTime;
            yield return null;
        }
        Destroy(thisMeleeSprite.gameObject);
    }

#endregion

#region Powerup
// If we have this attack already, power it up!
    public void PowerUpAttack(){
        if(thisMelee.aName == ""){
            SetUpMelee();
            return;
        }

        int randIndex = UnityEngine.Random.Range(0, 2);

        if(randIndex == 0 && damage < 20){
            damage += 5;
        }else if(cooldown > 0.25f){
            cooldown -= 0.15f;
        }  
    }

// Set up our new melee attack.
    private void SetUpMelee(){
        thisMelee = new Melee("Slash", 5, 1, 1, 1, Color.red);
        this.damage = thisMelee.damage;
        this.cooldown = thisMelee.cooldown;
        this.graphicColor = thisMelee.color;

    }
#endregion

    // Debugging.
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;

        if(thisMelee != null)
            Gizmos.DrawSphere(transform.position + (Vector3.right * thisMelee.length), thisMelee.radius);
    }
}
