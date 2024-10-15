using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class perkHolder : MonoBehaviour//,IEffectable
{
    private StatPerk _data;



    public void ApplyPerk(StatPerk _data)
    {
       this._data = _data;
    }

    //private float currentEffectTime = 0f;
    //private float lastTickTime = 0f;
    /*public void HandleEffect()
    {
        currentEffectTime += Time.deltaTime;
        if(currentEffectTime >= _data.Lifetime)
        {
            RemoveEffect();
        }
        if(_data.DOTAmount != 0) 
        {
        
        }
    }*/

    public void RemoveEffect()
    {
        _data = null;
    }









    //[SerializeField] public perkSO perk;
    //float cooldownTime;
    //private float currUses;
    //float maxUses;
    //bool pressed = false;


    //enum PerkState
    //{
    //    ready, active, cooldown
    //}
    //PerkState state = PerkState.ready;


    //void Update()
    //{
    //    pressed = this.GetComponent<PlayerCombat>().rangedPressed;
    //    switch (state)
    //    {
    //        case PerkState.ready:
    //            if (pressed && currUses < maxUses)
    //            {
    //                perk.Activate(gameObject);
    //                state = PerkState.active;
    //            }
    //            break;
    //        case PerkState.active:
    //            if(currUses <= maxUses)
    //            {
    //                Debug.Log("used");
    //                //currUses = this.GetComponent<PlayerCombat>().shotCount;
    //            }
    //            else
    //            {
    //                state = PerkState.cooldown;
    //                cooldownTime = perk.cooldownTime;
    //                perk.BeginCooldown(gameObject);
    //            }
    //            break;
    //        case PerkState.cooldown:
    //            if (cooldownTime > 0)
    //            {
    //                cooldownTime -= Time.deltaTime;
    //            }
    //            else
    //            {
    //                state = PerkState.ready;
    //            }
    //            break;

    //    }
    //}

}
