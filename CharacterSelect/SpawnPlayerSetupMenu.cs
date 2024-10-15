using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public GameObject playerSetupMenuPrefab;
    public CharacterSwitchUI characterSwitchUI;
    private GameObject rootMenu;
    public PlayerInput input;

    private void Awake()
    {
        rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null)
        {
            var menu = Instantiate(playerSetupMenuPrefab, rootMenu.transform);
            
            InputSystemUIInputModule inputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            input.uiInputModule = inputModule;
            
            characterSwitchUI = menu.GetComponentInChildren<CharacterSwitchUI>();

            if (characterSwitchUI != null)
            {
                // initialize the CharacterSwitchUI with the PlayerInput instance
                //Debug.Log("SpawnPlayerSetupMenu: Initializing CharacterSwitchUI for player index " + input.playerIndex);
                characterSwitchUI.Initialize(input);
                //Debug.Log("")
            }
            //Debug.Log("SpawnPlayerSetupMenu: Setting player index " + input.playerIndex + " for PlayerSetupMenuController");
            menu.GetComponent<PlayerSetupMenuController>().setPlayerIndex(input.playerIndex);
            //DontDestroyOnLoad(menu);
        }

    }
}
