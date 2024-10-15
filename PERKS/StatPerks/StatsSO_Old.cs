using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatsSO_Old : ScriptableObject
{
    /*[SerializeField]
    //public Dictionary<Stat, float> instanceStats = new Dictionary<Stat, float>();

    public Dictionary<Stat, float> stats = new Dictionary<Stat, float>();
    
    public List<StatInfo> statInfo = new List<StatInfo>();
    //public List<InstanceStatsInfo> instanceStatsInfo = new List<InstanceStatsInfo>();
    public List<StatsUpgrade> appliedUpgradePerks = new List<StatsUpgrade>();
    public List<StatPerk> appliedStatPerks = new List<StatPerk>();
    
    public event Action<Stats, StatPerk> perkUpgradeApplied;
    public void Awake() 
    {
        foreach (var s in statInfo)
        {
            stats.Add(s.statType, s.statValue);
        }

        foreach (var s in instanceStatsInfo)
        {
            stats.Add(s.statType, s.instanceValue);
        }
        //foreach(var s in appliedStatPerks)
       
    }

    public float GetStatAsFloat(Stat stat)
    {
        return (float)GetStat(stat);
    }
    public float GetStat(Stat stat)
    {
        /*if (instanceStats.TryGetValue(stat, out var instanceValue))
            return GetUpgradedValue(stat, instanceValue);
        if (stats.TryGetValue(stat, out float value))
            return GetUpgradedValue(stat, value);
        else
        {
            Debug.LogError($"No stat value found for {stat} on {this.name}");
            return 0;
        }
    }
    private float GetUpgradedValue(Stat stat, float baseValue)
    {
        foreach (var perk in appliedUpgradePerks)
        {
            if (!perk.perkToApply.TryGetValue(stat, out float perkValue))
                continue;

            /*if (perk.isPercentPerk)
                baseValue *= (perkValue / 100f) + 1f;
            else
                baseValue += perkValue;
        }

        return baseValue;
    }
    
 
    public void AquireUpgradePerk(StatsUpgrade perk)
    {
        if (!appliedUpgradePerks.Contains(perk))
        {
            appliedUpgradePerks.Add(perk);
            perkUpgradeApplied?.Invoke(this, perk);
        }
    }
    public void AquireStatPerk(StatPerk perk)
    {
        if (!appliedStatPerks.Contains(perk))
        {
            appliedStatPerks.Add(perk);
            perkUpgradeApplied?.Invoke(this, perk);
        }
    }

    
    public float ChangeStat(Stat stat, float amount)
    {
        foreach (var s in statInfo)
        {
            if (s.statType == stat)
            {
                return s.statValue;
            }
        }
        return 0;
    }
    

   
   
    public void ResetPerks()
    {
        appliedUpgradePerks.Clear();
        appliedStatPerks.Clear();
    }*/
    
}

