using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class RangedAttack : MonoBehaviour
{
    public CaneManager cane;

    public PlayerHealth health;
    private PlayerCombat combat;
    //public InputAction range;
    public GameStats gameStats;
    public StatsManager characterStats;
    public GameObject projectilePrefab; // Prefab of the projectile
    public Transform shootPoint; // Point from where the projectile is spawned
    public PlayerAnimationManager animator;
    public float bulletCount;
    public bool rangedPressed;
    public bool canShoot = true;
    private float lastShotTime; // Time when the last shot was fired
    public bool blastardEquipped;
    public GameEvent onStaminaChange;

    void Start(){
        canShoot = true;
    }
    public void Ranged()
    {
        if (!blastardEquipped)
        {
            if (health.currStaminaCount > 1f && Time.time - lastShotTime >= characterStats.reloadCooldown && canShoot)
            {
                
                cane.meleeCaneOn();
                Debug.Log("fired");
                animator.Ranged();
                health.reloadTimer = 0f;

                // Calculate direction based on character's rotation and spread angle
                Vector3 direction = Quaternion.Euler(0f, 45f, 0f) * transform.forward;

                // Calculate bullet force with slight variation
                float bulletForce = characterStats.bulletSpeed;
                projectilePrefab.GetComponent<BulletDamage>().SetBulletDamage(characterStats.bulletDamage);

                // Instantiate bullet prefab
                GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, Quaternion.LookRotation(transform.forward, Vector3.up));
                //Debug.Log("Huh");
                // Apply force to the bullet in the calculated direction
                bullet.GetComponent<Rigidbody>().velocity = direction * bulletForce;

                // Clean up the bullet after the desired lifetime
                Destroy(bullet, characterStats.bulletRange);
                health.UseStamina(1);

                //onStaminaChange.Raise(this, health.currStaminaCount);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Range, this.transform.position);
                
                gameStats.AddRangedAttacks();
                canShoot = false;
                //StartCoroutine(OnShoot());
                
            }
            rangedPressed = false;
            //cane.marchCaneOn();
        }       
        
    }
    /*IEnumerator OnShoot(){
        Debug.Log("noton");
        yield return new WaitForSeconds(.5f);
        
        canShoot = true;
    }*/
}
