using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class MonchPlayerCombat : MonoBehaviour
{
    private InputActionAsset inputAsset;
    private InputActionMap controls;
    public PlayerHealth health;
    public RangedAttack rangedAttack;
    public Parry parry;

    //private Dash dash;
    public MonchMeleeAttack meleeAttack;

    public PlayerAnimationManager animator;
    public GameStats GameStats;
    public GameplayManager GameplayManager;
    public StatsManager stats;

    public GameEvent PausePressed;

    public void Ranged()
    {
        rangedAttack.Ranged();
    }
    public void Melee()
    {
        meleeAttack.Melee();

    }
    public void Parry()
    {
        parry.ParryDown();
        animator.Parry();
    }
}
