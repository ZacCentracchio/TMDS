using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatInfo 
{
    [SerializeField]
    public Stat statType;
    [SerializeField]
    public float statValue;

    
}

public class InstanceStatsInfo
{
    [SerializeField]
    public Stat statType;
    [SerializeField]
    public float instanceValue;


}
//public enum Stat
//{
//    maxHealth, maxStamina,
//    speed, dashSpeed, rangeSpeed,
//    ultimateChargeSpeed, staminaRechargeSpeed,
//    parryLength,
//    rangeDamage, meleeDamage,
//}
