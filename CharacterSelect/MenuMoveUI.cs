using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class MenuMoveUI : MonoBehaviour
{
    public Button[] buttons;
    //private int index = 0;
    private PlayerControls controls;
    public PlayerInput input;
    //private bool isFirstButtonPressed = false;
    

    public PlayerMainMenuController playerMainMenuController;




    public void Initialize(PlayerInput playerInput)
    {
        controls = new PlayerControls();
        controls.devices = playerInput.devices;

        //controls.UI.Next.performed += ctx => Up();
        //controls.UI.Previous.performed += ctx => Down();
        //controls.UI.Select.performed += ctx => SelectButton();
        //controls.UI.Back.performed += ctx => Back();

        controls.UI.Enable();
        Debug.Log("CharacterSwitchUI: Initializing for player index " + playerInput.playerIndex);
    }

    private void OnDisable()
    {
        controls.UI.Disable();
    }
}
