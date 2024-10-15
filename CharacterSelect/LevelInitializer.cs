using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] 
    private Transform[] PlayerSpawns;
    [SerializeField] 
    private GameObject[] playerPrefab;
    public GameRulesSO gameRules;
    public Transform PlayerParent;
    [SerializeField] 
    private PlayerCharacterInfoSO[] PCInfo;
    // Start is called before the first frame update
    void Start()
    {
        /*
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, PlayerSpawns[i].position, PlayerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
        }
        */
        
        //var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        
        for (int i = 0; i < gameRules.PlayerCount ; i++)
        {
            Debug.Log(PCInfo[i]);
            var player = Instantiate(playerPrefab[PCInfo[i].playerCharacterIndex], PlayerSpawns[i].position, PlayerSpawns[i].rotation, PlayerParent);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(PCInfo[i]);
            //playerConfigs[i].SetActive(false);
            //Debug.Log("check count");
            //Re-enable PlayerInput
            var playerInput = player.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.enabled = true;
            }
        }
            
    }
}
