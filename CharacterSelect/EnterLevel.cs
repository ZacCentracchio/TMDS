using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLevel : MonoBehaviour
{
    [SerializeField] private Transform[] PlayerSpawns;
    [SerializeField] private GameObject[] playerPrefab;

    void Start()
    {
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            if (i < playerPrefab.Length && i < PlayerSpawns.Length) // Ensure index is within range
            {
                GameObject player = Instantiate(playerPrefab[i], PlayerSpawns[i].position, PlayerSpawns[i].rotation, transform);
                //player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            }
            else
            {
                Debug.LogError("Player spawn or prefab array index out of range.");
            }
        }
    }
}
