using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    // Runtime
    Movement_Player mPlayer;

    public LayerMask enemyLayer;

    [Header("This Ranged Attack")]
    [SerializeField] Ranged thisRanged;
    [SerializeField] float damage;
    [SerializeField] float radius;
    [SerializeField] float cooldown;
    
    [Header("Ranged Cooldown")]
    [SerializeField] float rangedCooldown;

    [Header("Player Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float throwSpeed;


    private void Awake() {
        mPlayer = GetComponent<Movement_Player>();
    }

    private void Update() {
        if(thisRanged != null && rangedCooldown <= 0){
            StartCoroutine(PerformRanged());

            rangedCooldown = thisRanged.cooldown;
        }

        if(thisRanged != null)
            rangedCooldown -= Time.deltaTime;
    }

#region Ranged Functions
// Perform a ranged attack.
    IEnumerator PerformRanged(){
        if(thisRanged.aName == "")
            yield break;
        
        print("Throw!");

        // Get the furthest enemy.
        GameObject furthestEnemy = FindFurthestEnemyInRange();
        if(furthestEnemy){
            // Throw projectile.
            GameObject thisProjectile = Instantiate(projectile, transform.position, projectile.transform.rotation);
            thisProjectile.GetComponent<Rigidbody>().AddForce(PointTowardsEnemy(furthestEnemy.transform.position) * throwSpeed, ForceMode.Impulse);

            yield return new WaitForSeconds(.1f);

            // Throw at the furthest enemy. (Recheck to see if it exists, melee could have killed it)
            if(furthestEnemy)
                furthestEnemy.GetComponent<Health_Enemy>().TakeDamage(5);
        }

        

    }

// Finds the furthest enemy in range of the player.
    private GameObject FindFurthestEnemyInRange(){
        // Capture the nearest enemies.
        Collider[] enemyCols = null;
        enemyCols = Physics.OverlapSphere(transform.position, thisRanged.rangedRadius, enemyLayer);

        // Sort through and determine the furthest enemy.
        if(enemyCols.Length > 0){
            Collider furthestCol = enemyCols[0];
            float furthestDistance = (transform.position - furthestCol.transform.position).magnitude;

            for(int i=0; i<enemyCols.Length; i++)
            {

                float thisDistance = (transform.position - enemyCols[i].transform.position).magnitude;

                if(thisDistance > furthestDistance){
                    furthestDistance = thisDistance;
                    furthestCol = enemyCols[i];
                }

            }

            return furthestCol.gameObject;

        }else
        {
            return null;
        }
    }

// Returns a vector that points from player to enemy.
    private Vector3 PointTowardsEnemy(Vector3 enemyPos){
        Vector3 rawProjectileDirection = enemyPos - transform.position;
        return new Vector3(rawProjectileDirection.x, 0, rawProjectileDirection.z).normalized;
    }
#endregion

#region Powerup
// If we have this attack already, power it up!
    public void PowerUpAttack(){
        if(thisRanged == null){
            SetUpRanged();
            return;
        }

        int randIndex = UnityEngine.Random.Range(0, 3);

        if(randIndex == 0 && damage < 10){
            damage += 5;
        }else if(randIndex == 1 && cooldown > 0.20f){
            cooldown -= 0.15f;
        }else if(radius < 10){
            radius += 1;
        }
    }

// Set up our new ranged attack.
    private void SetUpRanged(){
        thisRanged = new Ranged("Throw", 5, 2, 5);
        this.damage = thisRanged.damage;
        this.radius = thisRanged.rangedRadius;
        this.cooldown = thisRanged.cooldown;

    }
#endregion
}
