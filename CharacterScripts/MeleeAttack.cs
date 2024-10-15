using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements.Experimental;

public class MeleeAttack : MonoBehaviour
{
    public CaneManager cane;
    private GameStats gameStats;
    public static int meleeCount = 0;
    public float nextAttackTime;
    public float maxComboDelay;
    public float lastAttackTime = 0f;
    //public bool meleePressed;
    
    public float meleeCooldown;

    private Animator anim;
    private PlayerCombat combat;
    private StatsManager characterStats;
    //Reset Counters
    public bool isAttacking;
    public bool canAttack = true;

    public Collider hitCollider, headCollider;
    public Collider getHitCollider;
    public int playerID;

    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<PlayerCombat>();
        anim = GetComponent<Animator>();
        characterStats = GetComponent<StatsManager>();
        gameStats = GetComponent<GameStats>();
        hitCollider.enabled = false; // Initially disabled
    }

    // Update is called once per frame
    void Update()
    {   
        lastAttackTime += Time.deltaTime;
        lastAttackTime = Mathf.Clamp(lastAttackTime, 0, maxComboDelay);
        if (lastAttackTime == maxComboDelay && isAttacking)
        {     
            //lastAttackTime = 0f;
            FailedToAttack();   
            Debug.Log("ComboReset");    
            //StartCoroutine(MeleeCooldown());
            //canAttack = true;
            //meleePressed = false;
        }
        else if (lastAttackTime > nextAttackTime){
            canAttack = true;
        }

        //cooldown time

    }


    public void Melee(/*int meleeCount*/)
    {
        //LastAttackTimer();
        if(canAttack){
            //StopAllCoroutines();
            anim.SetBool("failedToAttack", false);
            lastAttackTime = 0f;
            isAttacking = true;
            meleeCount++;
            cane.meleeCaneOn();
            
            Debug.Log(meleeCount);
        }
        
        if(meleeCount > 3){
            meleeCount = 0;
            canAttack = false;
            isAttacking = false;
        }
        meleeCount = Mathf.Clamp(meleeCount, 0, 3);
        
        if (meleeCount == 1 && canAttack)
        {
            anim.SetBool("m1", true);
            isAttacking = true;
            canAttack = false;
            Debug.Log("m1 On");
            //EnableAttackCollider();
            gameStats.AddMeleeAttacks();
        }
        
 
        if (meleeCount == 2 && canAttack && anim.GetBool("m1"))
        {
            anim.SetBool("m2", true);
            anim.SetBool("m1", false);
            isAttacking = true;
            canAttack = false;
            //EnableAttackCollider();
            Debug.Log("m2 On");
            gameStats.AddMeleeAttacks();

        }
        if (meleeCount == 3 && canAttack && anim.GetBool("m2"))
        {
            anim.SetBool("m3", true);
            anim.SetBool("m2", false);
            isAttacking = true;
            canAttack = false;
            gameStats.AddMeleeAttacks();
            //StartCoroutine(MeleeCooldown());
            Debug.Log("m3 On");
            
        }

    }

    private void EnableAttackCollider()
    {
        if (isAttacking)  // Check if an attack has been initiated
        {
            hitCollider.enabled = true;
        }
    }
    private void DisableAttackCollider()
    {
        hitCollider.enabled = false;
    }
    private void EnableHeadButtCollider(){
        if (isAttacking)  // Check if an attack has been initiated
        {
            headCollider.enabled = true;
        }
    }
    private void DisableHeadButtCollider()
    {
        headCollider.enabled = false;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject != gameObject)
        {
            PlayerHealth otherPlayerHealth = other.GetComponent<PlayerHealth>();
            if (otherPlayerHealth != null && otherPlayerHealth.playerID != this.playerID && isAttacking)
            {  // Ensure that the player is in an attacking state
                float damage = CalculateDamage();
                otherPlayerHealth.TakeDamage(damage);
                gameStats.DamageDealt((int)damage);
            }
        }
    }
    private float CalculateDamage()
    {
        return characterStats.meleeDamage;
    }
    IEnumerator MeleeCooldown()
    {       
        yield return new WaitForSeconds(meleeCooldown);
        
        Debug.Log("Melee Cooldown finished");
        //isAttacking = false;
        canAttack = true;
    }
    private void FailedToAttack()
    {
        isAttacking = false;
        canAttack = false;
        cane.marchCaneOn();
        anim.SetBool("m1", false);
        anim.SetBool("m2", false);
        anim.SetBool("m3", false);
        anim.SetBool("failedToAttack", true);
        meleeCount = 0;
        
        StartCoroutine(MeleeCooldown());

    }

    

    //calculate damage


    /*else{
            anim.SetBool("m2", false);
            anim.SetBool("m3", false);
            anim.SetBool("m1", false);
        }
        /*if (meleeCount == 2 && isComboing && canAttack)
        {

            Debug.Log("m_3");
            anim.SetTrigger("ComboDone");

            meleeCount = meleeCountReset;
            anim.ResetTrigger("nextCombo");
            //return;
        }
        if (meleeCount == 1 && isComboing && canAttack)
        {
            anim.SetTrigger("nextCombo");
            Debug.Log("m_2");
            //meleeCount++;
            //return;
        }     
        if (meleeCount == 0 && canAttack)
        {
            Debug.Log("m_1");
            //StartCoroutine(MeleeCooldown());
            anim.SetTrigger("firstMelee");
            //meleeCount++;
            Debug.Log(meleeCount);
            isComboing = true;
            //return;
        }

        //return;*/
}
