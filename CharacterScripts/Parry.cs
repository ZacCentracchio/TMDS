using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Parry : MonoBehaviour
{
    public PlayerHealth health;
    bool canParry;

    public void ParryDown()
    {
        if(health.currStaminaCount  > 0 && canParry)
        {
            Debug.Log("parry");
        }

    }
}
