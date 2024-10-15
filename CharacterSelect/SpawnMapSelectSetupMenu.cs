using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public class SpawnMapSelectSetupMenu : MonoBehaviour
{
    public GameObject mapPrefab;

    private GameObject rootMenu;
    public PlayerInput input;

    private void Awake()
    {
        rootMenu = GameObject.Find("Map");
        if (rootMenu != null)
        {
            var menu = Instantiate(mapPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<MapSelectSetupMenuController>().setPlayerIndex(input.playerIndex);
        }

    }
}
