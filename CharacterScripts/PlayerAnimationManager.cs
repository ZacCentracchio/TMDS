using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerCombat combat;
    private PlayerHealth health;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<PlayerCombat>();
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Moving()
    {
        /*if (movement.movementPressed)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);

        }
        if (!movement.movementPressed)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
        }*/

    }
    public void Dash()
    {
        anim.SetBool("isDashing", true);
    }
    public void Ranged()
    {
        anim.SetTrigger("isRanged");
    }
    public void Ultimate()
    {

        anim.SetTrigger("isUltimate");
    }
    public void Parry()
    {
        anim.SetTrigger("isParry");
    }
    public void Melee()
    {
        
    }
    public void AnimOff(string animName)
    {
        anim.SetBool(animName, false);
    }
}
