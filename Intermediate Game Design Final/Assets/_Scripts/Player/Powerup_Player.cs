using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Player : MonoBehaviour
{
    [Header("Attacks")]
    [SerializeField] MeleeAttack mAttack;
    [SerializeField] RangedAttack rAttack;
    [SerializeField] ForcefieldAttack fAttack;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 12){
            if(other.CompareTag("pMelee")){
                mAttack.PowerUpAttack();
            }else if(other.CompareTag("pRanged")){
                rAttack.PowerUpAttack();
            }else{
                fAttack.PowerUpAttack();
            }

            Destroy(other.gameObject);
        }
    }
}
