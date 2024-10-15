using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    
    //Animation variables
    public PlayerAnimationManager animator;

    //int isWalkingHash;
    //int isDashingHash;

    //Player Settings
    //[SerializeField] public float moveSpeed = 4f;
    [SerializeField] public float gravityValue = -9.8f;

    Vector3 forward, right, heading;

    //calls the input system
    private InputActionAsset inputAsset;
    private InputActionMap controls;
    private CharacterController controller;
    private PlayerCombat combat;
    private PlayerHealth health;
    public StatsManager stats;

    //Player Variables
    Vector2 currentMovement;
    public bool movementPressed;
    public bool dashPressed;

    public bool canDash = true;
    public bool isDashing;
    //public float dashingPower = 14f;
    //public float dashingTime = 0.25f;
    //private float dashingCooldown = 1f;
    public float dashHeight = 1f;
    //public float bulletDashMax;
    public float currStaminaCount;
    private Animator anim;

    public bool canMove = true; 
    public ParticleSystem dashEffect;

    private Vector3 playerVelocity;

    void Start()
    {

        combat = GetComponent<PlayerCombat>();
        controller = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimationManager>();
        anim = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

    }
    void Awake()
    {
        //connects the input sytem when game starts
        inputAsset = this.GetComponent<PlayerInput>().actions;
        controls = inputAsset.FindActionMap("Gameplay");
        //dashPressed = false;
    }

    //Calls rotation and movement inputs every frame.
    void Update()
    {
        handleMovement();
        handleRotation();

    }

    void handleRotation()
    {

        Vector3 currrentPosition = transform.position;
        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);
        
        Vector3 positionToLookAt = currrentPosition + newPosition;

        transform.LookAt(positionToLookAt);
    }
    void handleMovement()
    {
        
        playerVelocity.y += gravityValue * Time.deltaTime;

        Vector3 rightMovement = right * stats.moveSpeed * Time.deltaTime * currentMovement.x;

        Vector3 upMovement = forward * stats.moveSpeed * Time.deltaTime * currentMovement.y;
        heading = Vector3.Normalize(rightMovement + upMovement);
        //transform.position += rightMovement;                                                        
        //transform.position += upMovement;

        controller.Move(upMovement);
        controller.Move(rightMovement);
        controller.Move(playerVelocity * Time.deltaTime);
        //animator.Moving();
        if (movementPressed)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);

        }
        if (!movementPressed)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
        }    

    }

    public IEnumerator Knockback(Vector3 direction, float duration)
    {
        float timer = 0;
        canMove = false;  // Disable other movement input
        anim.SetBool("onDamaged", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("onDamaged", false);

        float drag = 0.1f;  // Drag coefficient

        while (timer < duration)
        {
            direction *= (1 - drag);  // Apply drag
            controller.Move(direction * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        canMove = true;  // Re-enable movement input
    }
    
    public void onDash(InputAction.CallbackContext context)
    {
        
        if (canDash  && health.currStaminaCount >= 1f)
        {
            Debug.Log("Dashed");
            StartCoroutine(Dash());
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Dash, this.transform.position);

        }
        
    }
    
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        animator.Dash();
        health.currStaminaCount--;
        float startTime = Time.time;
        
        float originalGravity = gravityValue;
        gravityValue = 40f;
        while (Time.time < startTime + stats.dashTime){
            controller.Move(heading * stats.dashSpeed * Time.deltaTime);
            dashEffect.Play();
            yield return null;
        }
        dashPressed = false;
        isDashing = false;
        gravityValue = originalGravity;
        animator.AnimOff("isDashing");
        yield return new WaitForSeconds(stats.dashCooldown);
        
        canDash = true;
        dashEffect.Stop();
    }
    public void onMove(InputAction.CallbackContext context)
    {

        currentMovement = context.ReadValue<Vector2>();
        movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        //Debug.Log(movementPressed);
        //handleMovement();
        //handleRotation();

        //Debug.Log("Run");
    }
    void OnEnable()
    {
        controls.FindAction("Dash").started += onDash;
        controls.Enable();
    }

    void OnDisable()
    {
        controls.FindAction("Dash").started -= onDash;

        controls.Disable();

    }
    public void SetPlayer()
    {
        //GameObject pm = GameObject.Find("PerkManager");
        //pm.GetComponent<perkManager>().Player = gameObject.gameObject;
    }

    
    internal void onMove(Vector2 vector2)
    {
        currentMovement = vector2;
        handleMovement();
    }
}
