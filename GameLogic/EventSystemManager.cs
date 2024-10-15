/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public PlayerInput input;
    public GameObject playerSetupMenu; // Define a GameObject for the player setup menu
    public EventSystem eventSystem; // Reference to your MultiplayerEventSystem

    private GameObject playerSetupMenuInstance; // Instance of the player setup menu
    private GameObject firstSelectedPlayer1; // Reference to the first selected UI for Player 1
    private GameObject firstSelectedPlayer2; // Reference to the first selected UI for Player 2

    private void Awake()
    {
        // Find the "MainLayout" GameObject
        var rootMenu = GameObject.Find("MainLayout");
        
        if (eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }

        // Ensure the rootMenu exists
        if (rootMenu != null)
        {
            GameObject menu;

            // If a menu already exists, use that
            if (GameObject.FindWithTag("PlayerSetupMenu") != null)
            {
                menu = GameObject.FindWithTag("PlayerSetupMenu");
            }
            // If not, create a new menu
            else
            {
                menu = Instantiate(Resources.Load("PlayerSetupMenu") as GameObject, rootMenu.transform);
                firstSelectedPlayer1 = GameObject.Find("PlayerSetupMenu/SetupPanel/Menu/Red");
                firstSelectedPlayer2 = GameObject.Find("PlayerSetupMenu/SetupPanel/Menu/Red (1)");
                menu.tag = "PlayerSetupMenu";
            }

            // Get PlayerSetupMenuController from menu and set player index
            var setupController = menu.GetComponent<PlayerSetupMenuController>();
            var playerInput = GetComponent<PlayerInput>();
            if (playerInput.playerIndex == 0)
            {
                setupController.SetPlayer1Index(playerInput.playerIndex);
                eventSystem.firstSelectedGameObject = firstSelectedPlayer1;
            }
            else if (playerInput.playerIndex == 1)
            {
                setupController.SetPlayer2Index(playerInput.playerIndex);
                eventSystem.firstSelectedGameObject = firstSelectedPlayer2;
            }
        }
    }

    private void Update()
    {
        if (eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem != null)
            {
                //wait for the event system to be found
                eventSystem.firstSelectedGameObject = firstSelectedPlayer1;
            }
        }
    }

    private void Start()
    {
        // Find PlayerSetupMenuController in the scene
        var playerSetupMenuController = FindObjectOfType<PlayerSetupMenuController>();

        if (playerSetupMenuController != null)
        {
            if (input.playerIndex == 0)
            {
                playerSetupMenuController.SetPlayer1Index(input.playerIndex);
            }
            else if (input.playerIndex == 1)
            {
                playerSetupMenuController.SetPlayer2Index(input.playerIndex);
            }
        }
    }
}
*/