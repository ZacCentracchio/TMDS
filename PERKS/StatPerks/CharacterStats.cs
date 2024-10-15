using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class CharacterStats : ScriptableObject
{
    //Health Variables
    public float maxHealth;
    public float maxStaminaCount;
    public float staminaRechargeSpeed;

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
}
