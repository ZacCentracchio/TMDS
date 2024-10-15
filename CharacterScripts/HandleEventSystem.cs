using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
public class HandleEventSystem : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public GameObject eventSystemPrefab;
    public MultiplayerEventSystem eventSystem;
    private Transform eventSystemParent;
    // Start is called before the first frame update
    void Start()
    {
        eventSystemParent = GameObject.Find("EventSystems").transform;
        GameObject eventSystemObject = Instantiate(eventSystemPrefab, eventSystemParent) as GameObject;
        eventSystem = eventSystemObject.GetComponent<MultiplayerEventSystem>();
        eventSystem.playerRoot = GameObject.Find("UICanvas");
        eventSystemObject.GetComponent<InputSystemUIInputModule>().actionsAsset = playerInputHandler.playerInput.actions;
    }

    public void EventSystemOff(){
        eventSystem.enabled = false;
    }
    public void EventSystemOn(){
        eventSystem.enabled = true;
    }
//m_ActionsAsset
    // Update is called once per frame
    void Update()
    {
        
    }
}
