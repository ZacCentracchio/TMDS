using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class TedPlayerCombat : MonoBehaviour
{
    private InputActionAsset inputAsset;
    public InputActionMap controls;
    public PlayerHealth health;
    public RangedAttack rangedAttack;
    public Parry parry;

    //private Dash dash;
    //public MeleeAttack meleeAttack;
    public TedMeleeAttack meleeAttackTed;

    public PlayerAnimationManager animator;
    public GameStats GameStats;
    public GameplayManager GameplayManager;
    public StatsManager stats;

    //public float currStaminaCount = 0; // Number of bullets in a shotgun blast
    private float nextMeleeTime = 0f; // Time when the last shot was fired

    public bool meleePressed, pausePressed;
    public bool rangedPressed;
    public bool parryPressed;
    public bool canAttack = true;
    public bool isPunching;

    public GameEvent PausePressed;


    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        controls = inputAsset.FindActionMap("Gameplay");
    }


    public void onRanged(InputAction.CallbackContext context)
    {
        //Debug.Log("fire");
        if (context.performed)
        {
            rangedPressed = true;
        }
        if (context.canceled)
        {
            rangedPressed = false;
        }
        //&& Time.time - lastShotTime >= cooldownTime
        if (rangedPressed)
        {
            rangedAttack.Ranged();
            //animator.Ranged();
        }
        rangedPressed = false;
    }
    public void onMelee(InputAction.CallbackContext context)
    {
        Debug.Log("melee action");
        if (context.performed)
        {
            meleePressed = true;
        }
        if (context.canceled)
        {
            meleePressed = false;
            Debug.Log("melee action canceled");
        }


        //meleeAttack.ToggleMeleePressed();

        if (meleePressed && canAttack && Time.time > nextMeleeTime)
        {
            Debug.Log("melee attack code called");
            meleeAttackTed.Melee();
            //AudioManager.instance.PlayOneShot(FMODEvents.instance.Melee, this.transform.position);
            //meleeCount += 1;
            //animator.Melee();
        }
        //meleePressed = false;

    }
    public void onParry(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            parryPressed = true;
        }
        if (context.canceled)
        {
            parryPressed = false;
        }
        //&& Time.time - lastShotTime >= cooldownTime
        if (parryPressed)
        {
            parry.ParryDown();
            animator.Parry();
        }
        parryPressed = false;
    }
    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pausePressed = true;
            Debug.Log("pressed");
            PausePressed.Raise(this, pausePressed);
        }
        if (context.canceled)
        {
            pausePressed = false;
        }


        pausePressed = false;

    }
    public void GameOver(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > .5)
        {
            GameplayManager.GameOver();
        }
    }
    private void OnEnable()
    {
        controls.FindAction("Ranged").started += onRanged;
        controls.FindAction("Parry").started += onParry;
        controls.FindAction("Melee").started += onMelee;
        controls.FindAction("MenuOpenClose").started += OpenPauseMenu;
        controls.FindAction("GameOver").started += GameOver;
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.FindAction("Ranged").started -= onRanged;
        controls.FindAction("Parry").started -= onParry;
        controls.FindAction("Melee").started -= onMelee;
        controls.FindAction("MenuOpenClose").started -= OpenPauseMenu;
        controls.FindAction("GameOver").started -= GameOver;
        controls.Disable();

    }
}
