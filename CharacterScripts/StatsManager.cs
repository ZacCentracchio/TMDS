using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatsManager : MonoBehaviour
{
    //Health Variables
    public float maxHealth;
    public float maxStaminaCount;

    //Movement Variables
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    public float bulletDashMax;
    public float moveSpeed;

    //Attack Variables
    public float bulletSpeed;
    public float reloadCooldown;
    public float meleeDamage;
    public float bulletDamage;
    public float bulletRange;
    public float parryLength;
    public float roundPlacement;

    public int pointCount;

    public List<StatPerk> statPerks;

    //public float statNum;
    //[SerializeField] public Dictionary<Stat,float> stats;
    public CharacterStats stats;
    void Awake(){
        ResetStats();
    }
    public void ResetStats()
    {
        //Health Variables
        maxHealth = stats.maxHealth;
        maxStaminaCount = stats.maxStaminaCount;

        //Movement Variables
        dashSpeed = stats.dashSpeed;
        dashTime = stats.dashTime;
        dashCooldown = stats.dashCooldown;
        moveSpeed = stats.moveSpeed;

        //Attack Variables
        bulletSpeed = stats.bulletSpeed;
        reloadCooldown = stats.reloadCooldown;
        meleeDamage = stats.meleeDamage;
        bulletDamage = stats.bulletDamage;
        bulletRange = stats.bulletRange;
        parryLength = stats.parryLength;
    }
    public void SetNewStats(StatPerk perk){
       
        
        //Health Variables
        maxHealth += perk.maxHealth;
        maxStaminaCount += perk.maxStaminaCount;

        //Movement Variables
        dashSpeed += perk.dashSpeed;
        dashTime += perk.dashTime;
        dashCooldown += perk.dashCooldown;
        moveSpeed += perk.moveSpeed;

        //Attack Variables
        bulletSpeed += perk.bulletSpeed;
        reloadCooldown += perk.reloadCooldown;
        meleeDamage += perk.meleeDamage;
        bulletDamage += perk.bulletDamage;
        bulletRange += perk.bulletRange;
        parryLength += perk.parryLength;
        
    }
    /*public void Start()
    {
        stats = statSO.stats;
        Debug.Log(stats[Stat.maxHealth]);
        maxHealth = stats[Stat.maxHealth]; 
        maxStaminaCount = stats[Stat.maxStamina];
        moveSpeed = stats[Stat.moveSpeed];
        dashSpeed = stats[Stat.dashSpeed];
        bulletSpeed = stats[Stat.bulletSpeed];
        reloadCooldown = stats[Stat.staminaRechargeSpeed];
        bulletDamage = stats[Stat.bulletDamage];
        bulletRange = stats[Stat.bulletRange];
        meleeDamage = stats[Stat.meleeDamage];
        dashCooldown = stats[Stat.dashCooldown];
        parryLength = stats[Stat.parryLength];
        dashTime = stats[Stat.dashTime];
    }*/



}
