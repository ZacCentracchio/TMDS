using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stat Perk")]
public  class StatPerk : ScriptableObject
{
    //public StatsManager stats;
    
    //Health Variables
    public float maxHealth;
    public float maxStaminaCount;

    //Movement Variables
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    public float moveSpeed;

    //Attack Variables
    public float bulletSpeed;
    public float reloadCooldown;
    public float meleeDamage;
    public float bulletDamage;
    public float bulletRange;
    public float parryLength;
    
    [SerializeField]
    public Sprite icon;
    public GameObject PerkPrefab;
    public string perkName;
    public string perkDescription;
    /*[Tooltip("The stats that this upgrade applies to.")]
    [SerializeField]
    //public Dictionary<Stat, float> perkToApply = new Dictionary<Stat, float>();
    public float DOTAmount;
    public float TickSpeed;
    public float Lifetime;
    public float MovementPenalty;
    public float SpeedMod;
    public float maxHealthMod;
    public float maxStaminaMod;
    public float dashSpeedMod;
    public float rangeSpeedMod;
    public float staminaRechargeSpeedMod;
    public float parryLengthMod;
    public float meleeDamageMod;
    public float rangeDamageMod;
    //public GameObject playerPrefab;
    
    public Dictionary<Stat, float> perkToApply = new Dictionary<Stat, float>();
    public bool isPercentPerk = false;

    //will need to connect this to the input manager for individual controls
    public virtual void ApplyPerk()
    {
        
        foreach (var perk in perkToApply)
        {
            playerStats.AquireStatPerk(this);
        }
        
    }*/
    /*public void SetPlayerStats()
    {
        //Health Variables
        stats.maxHealth = maxHealth;
        stats.maxStaminaCount = maxStaminaCount;

        //Movement Variables
        stats.dashSpeed = dashSpeed;
        stats.dashTime = dashTime;
        stats.dashCooldown = dashCooldown;
        stats.moveSpeed = moveSpeed;

        //Attack Variables
        stats.bulletSpeed = bulletSpeed;
        stats.reloadCooldown = reloadCooldown;
        stats.meleeDamage = meleeDamage;
        stats.bulletDamage = bulletDamage;
        stats.bulletRange = bulletRange;
        stats.parryLength = parryLength;
    }*/
}
