using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour
{



    private InputActionAsset inputAsset;
    //private InputActionMap controls;
    public PlayerHealth health;
    public RangedAttack rangedAttack;
    public Parry parry;

    //private Dash dash;
    public MeleeAttack meleeAttack;


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


    //----------------------------------



    private PlayerConfiguration playerConfig;
    private PlayerMovement mover;
    private GameplayUIManager guim;
    private PlayerCombat combat;
    private DeadSFX deadSFX;
    private MiniGameUIManager mgm;
    

    [SerializeField]
    private MeshRenderer playerMesh;

    //private PlayerControls controls;
    [SerializeField] public PlayerInput playerInput;
    private InputActionMap Pcontrols, menuControls, combatControls, deadControls, tieControls, gameUIControls;
    
    
    public bool MenuOpenCloseInput {get; private set;}



    private void Awake()
    {
        
        //guim = GameObject.Find("GameplayUI").GetComponent<GameplayUIManager>();
        mover = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
        playerInput = GetComponent<PlayerInput>();
        deadSFX = GetComponentInChildren<DeadSFX>();
        
        menuControls = playerInput.actions.FindActionMap("Menus");
        combatControls = playerInput.actions.FindActionMap("Gameplay");
        deadControls = playerInput.actions.FindActionMap("DeadControls");
        tieControls = playerInput.actions.FindActionMap("TieControls");
        gameUIControls = playerInput.actions.FindActionMap("UI");
        Pcontrols = deadControls;
        //controls.Disable();
        menuControls.Disable();
        combatControls.Disable();
        deadControls.Disable();
        tieControls.Disable();
        gameUIControls.Disable();
        playerInput.SwitchCurrentActionMap("DeadControls");
        Debug.Log(playerInput.currentActionMap.name);
    }

    //Change between Control funcitons
    public void SetMenuControls(){
        Pcontrols.Disable();
        playerInput.SwitchCurrentActionMap("UI");
        Pcontrols = menuControls;
        Debug.Log(playerInput.currentActionMap.name);
    }
    public void SetDeadControls(){
        Pcontrols.Disable();
        playerInput.SwitchCurrentActionMap("DeadControls");
        Pcontrols = deadControls;
        Debug.Log(playerInput.currentActionMap.name);
    }
    public void SetCombatControls(){
        Pcontrols.Disable();
        playerInput.SwitchCurrentActionMap("Gameplay");
        Pcontrols = combatControls;
        Debug.Log(playerInput.currentActionMap.name);
    }
    public void SetTieControls(){
        Pcontrols.Disable();
        playerInput.SwitchCurrentActionMap("TieControls");
        Pcontrols = tieControls;
    }
    public void SetGameUIControls(){
        Pcontrols.Disable();
        playerInput.SwitchCurrentActionMap("UI");
        Pcontrols = gameUIControls;
        Debug.Log(playerInput.currentActionMap.name + playerInput.playerIndex);
    }
    


    public void InitializePlayer(PlayerCharacterInfoSO pc)
    {
        //playerConfig = pc;
        //playerInput = pc.Input;
        //playerInput.playerIndex = pc.PlayerIndex;
        playerMesh.material = pc.playerCharacterColor;
        //playerInput.onActionTriggered += Input_onActionTriggered;
    }

    
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (mover != null)
        {
            mover.onMove(context.ReadValue<Vector2>());
        }
            
    }

    //Dead Controls
    public void OnBoo(InputAction.CallbackContext context)
    {
        if(deadSFX != null && context.performed){
            deadSFX.PlayBooSFX();
        }
        
    }
    public void OnClap(InputAction.CallbackContext context)
    {
       if(deadSFX != null && context.performed){
            deadSFX.PlayClapSFX();
        }
    }

    //Game UI Controls
    public void OnUIMove(InputAction.CallbackContext context)
    {
  
    }
    public void OnSelect(InputAction.CallbackContext context)
    {
        
    }

    //Tie Controls
    public void OnMash(InputAction.CallbackContext context)
    {
        if(mgm != null && context.performed){
            mgm.OnMash();
        }
    }
    
    public void OnDash(InputAction.CallbackContext context)
    {
        if (mover != null)
        {
            mover.onDash(context);
        }
    }
    
    public void OnMenuOpenClose(InputAction.CallbackContext context)
    {
        if (combat != null)
        {
            OpenPauseMenu(context);
        }
        
        
    }
   

   
    public void OnRanged(InputAction.CallbackContext context)
    {
        //Debug.Log("fire");
        if(context.performed){
            rangedPressed = true;
        }
        if(context.canceled){
            rangedPressed = false;
        }
        //&& Time.time - lastShotTime >= cooldownTime
        if (rangedPressed)
        {
            combat.Ranged();
            
            //animator.Ranged();
        }
        rangedPressed = false;
    }
    public void OnMelee(InputAction.CallbackContext context)
    {
        if(context.performed){
            meleePressed = true;
        }
        if(context.canceled){
            meleePressed = false;
        }
            

        //meleeAttack.ToggleMeleePressed();
        
        if (meleePressed && canAttack && Time.time > nextMeleeTime)
        {
            
            meleeAttack.Melee();
            //AudioManager.instance.PlayOneShot(FMODEvents.instance.Melee, this.transform.position);
            //meleeCount += 1;
            //animator.Melee();
        }
        meleePressed = false;
        
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        if(context.performed){
            parryPressed = true;
        }
        if(context.canceled){
            parryPressed = false;
        }
        //&& Time.time - lastShotTime >= cooldownTime
        if (parryPressed)
        {
            combat.Parry();
        }
        parryPressed = false;
    }
    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        if(context.performed){
            pausePressed = true;
            Debug.Log("pressed");
            PausePressed.Raise(this, pausePressed);
        }
        if(context.canceled){
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

    /*void OnEnable()
    {
        combatControls.FindAction("Move").started += OnMove;
        combatControls.FindAction("Dash").started += OnDash;
        combatControls.FindAction("Ranged").started += OnRanged;
        combatControls.FindAction("Melee").started += OnMelee;
        combatControls.FindAction("Parry").started += OnParry;
        gameUIControls.FindAction("Submit").started += OnSelect;
        gameUIControls.FindAction("Move").started += OnUIMove;
        deadControls.FindAction("Boo").started += OnBoo;
        deadControls.FindAction("Clap").started += OnClap;
        tieControls.FindAction("Mash").started += OnMash;
        Pcontrols.FindAction("MenuOpenClose").started += OnMenuOpenClose;
        //Pcontrols["Ultimate"].started += OnUltimate;
        Pcontrols.Enable();
    }

    void OnDisable()
    {
        combatControls.FindAction("Move").started -= OnMove;
        combatControls.FindAction("Dash").started -= OnDash;
        combatControls.FindAction("Ranged").started -= OnRanged;
        combatControls.FindAction("Melee").started -= OnMelee;
        combatControls.FindAction("Parry").started -= OnParry;
        gameUIControls.FindAction("Submit").started -= OnSelect;
        gameUIControls.FindAction("Move").started -= OnUIMove;
        deadControls.FindAction("Boo").started -= OnBoo;
        deadControls.FindAction("Clap").started -= OnClap;
        tieControls.FindAction("Mash").started -= OnMash;
        Pcontrols.FindAction("MenuOpenClose").started -= OnMenuOpenClose;
        //Pcontrols["Ultimate"].started -= OnUltimate;
        Pcontrols.Disable();
    }*/
}