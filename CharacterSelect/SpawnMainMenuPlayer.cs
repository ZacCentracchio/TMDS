using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnMainMenuPlayer : MonoBehaviour
{
    /*public GameObject playerMainMenuPrefab;
    public GameObject EventSystemPrefab;
    public GameObject firstButton;
    private GameObject rootMenu;
    //private GameObject es;
    public PlayerInput input;

    private void Awake()
    {
        rootMenu = GameObject.Find("MenuPlayerSetup");
        if(rootMenu != null)
        {
            var menu = Instantiate(EventSystemPrefab, rootMenu.transform);
            //SetUIModule(menu);
            FindUIModule(menu);
        }
        
    }
    public void SetUIModule(GameObject menu){
        
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerMainMenuController>().setPlayerIndex(input.playerIndex);
    }
    public void SpawnEventSystem(){

    }
    public void FindUIModule(GameObject es){
        es = GameObject.Find("EventSystem");
        SetUIModule(es);
        //input.uiInputModule.playerRoot = rootMenu;
        //input.uiInputModule.SetSelectedGameObject = firstButton;
        
    }*/
}