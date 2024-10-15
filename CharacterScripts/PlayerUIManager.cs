using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerUIManager : MonoBehaviour
{
    private PlayerHealth health;
    private StatsManager stats;

    
    public GameObject multiplayerEventSystemPrefab;
    private GameObject EventSystemInstance;

    // Start is called before the first frame update
    void Start()
    {
        
        EventSystemInstance = Instantiate(multiplayerEventSystemPrefab) as GameObject;

        //EventSystemInstance.GetComponent<MultiplayerEventSystem>().playerRoot = GameObject.Find("GameplayUI");
    }
    


    
}
