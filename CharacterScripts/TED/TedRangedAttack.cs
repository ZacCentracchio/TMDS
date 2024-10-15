using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class TedRangedAttack : MonoBehaviour
{
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

    void Start()
    {
        canShoot = true;
    }

    void Update()
    {
        if (!canShoot && Time.time - lastShotTime >= characterStats.reloadCooldown)
        {
            canShoot = true;  // Reset canShoot after the cooldown
        }
    }

    public void Ranged()
    {
        Debug.Log($"Attempting to fire: CanShoot={canShoot}, Stamina={health.currStaminaCount}, TimeSinceLastShot={Time.time - lastShotTime}");
        if (!blastardEquipped && health.currStaminaCount > 0f && Time.time - lastShotTime >= characterStats.reloadCooldown && canShoot)
        {
            Debug.Log("Fired");
            FireProjectile();
            canShoot = false;
            lastShotTime = Time.time;
        }

    }
    private void FireProjectile()
    {
        Vector3 direction = Quaternion.Euler(0f, 45f, 0f) * transform.forward;
        GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, Quaternion.LookRotation(direction));
        bullet.GetComponent<Rigidbody>().velocity = direction * characterStats.bulletSpeed;
        Destroy(bullet, characterStats.bulletRange);
        //Debug.Log("fired");
        animator.Ranged();
        health.reloadTimer = 0f;

        // Calculate bullet force with slight variation
        float bulletForce = characterStats.bulletSpeed;
        projectilePrefab.GetComponent<BulletDamage>().SetBulletDamage(characterStats.bulletDamage);

        // Apply force to the bullet in the calculated direction
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletForce;

        lastShotTime = Time.time;  // Update the last shot time
        canShoot = false;  // Prevent further shots until cooldown passes

        // Clean up the bullet after the desired lifetime
        Destroy(bullet, characterStats.bulletRange);
        health.currStaminaCount--;

        AudioManager.instance.PlayOneShot(FMODEvents.instance.Range, this.transform.position);

        gameStats.AddRangedAttacks();
        canShoot = false;
        //StartCoroutine(OnShoot());
    }
}
