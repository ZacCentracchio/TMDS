using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    [SerializeField]private GameSceneManager gm;

    public List<PlayerCharacterInfoSO> PCInfo;
    [SerializeField] private int maxPlayers = 4;

    public GameObject menuPlayerPrefab;
    public GameObject gameplayPlayerPrefab;

    public PlayerInputManager inputManager; 

    public static PlayerConfigurationManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("SINGLETON - Tryingto create another instance of singleton");
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(Instance);

        playerConfigs = new List<PlayerConfiguration>();
        //PCInfo = new List<PlayerCharacterInfoSO>(); // Initialize PCInfo here

        
        /*
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);

            playerConfigs = new List<PlayerConfiguration>();
            PCInfo = new List<PlayerCharacterInfoSO>(); // Initialize PCInfo here
        }
        */

        gm = FindObjectOfType<GameSceneManager>();
        if (gm == null)
        {
            Debug.LogError("GameSceneManager not found in the scene.");
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    //Cjaracter Select functions
    public void SetPlayerColour(int index, Material colour)
    {
        PCInfo[index].playerCharacterColor = colour;
        //playerConfigs[index].PlayerColour = colour;
    }
    public void SetPlayerCharacter(int index, int characterIndex)
    {
        PCInfo[index].playerCharacterIndex = characterIndex;
        //playerConfigs[index].PlayerCharacterIndex = characterIndex;
    }
    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        Debug.Log("Player " + (index + 1).ToString() + " is ready");
        if (playerConfigs.Count > 1 && playerConfigs.All(p => p.IsReady == true))
        {
            if (gm != null)
            {
                Debug.Log("Attempting to load scene...");
                //DontDestroyOnLoad(Instance);
                gm.SetPlayerCount(playerConfigs.Count);
                gm.Loading_Screen();
                //gm.LoadMap();
                
                
            }
            else
            {
                Debug.LogError("GameSceneManager reference is null.");
            }
        }
    }
    public void UnReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = false;
        Debug.Log("Player " + (index + 1).ToString() + " is not ready");

    }


    //Sets map in the map select scene

    public void SelectMap(int index)
    {
        Debug.Log("Player " + (index + 1).ToString() + " selected map");
        if (gm != null)
        {
            //Add Functionality for saving what map has been chosen
            //gm.FrontierCapitol();
        }
        else
        {
            Debug.LogError("GameSceneManager reference is null.");
        }
    }
    
    public void ToGameSettings(){
        gm.Game_Settings();
    }
    public void ToMapSelect(){
        gm.MapSelect();
    }
    public void ToMainMenu(){
        gm.MainMenu();
    }
    public void ToCharacterSelect()
    {
        //KMS();
        Debug.Log("Transitioning to Character Select Scene...");

        // Attempt to find and assign GameSceneManager if not already assigned or if null
        if (gm == null)
        {
            gm = FindObjectOfType<GameSceneManager>();
            if (gm == null)
            {
                Debug.LogError("GameSceneManager not found in the scene.");
                return; // Exit the function to avoid further errors
            }
        }

        // Switch to gameplay prefabs, ensuring all player configurations use the gameplay prefab
        if (PlayerConfigurationManager.Instance != null)
        {
            // Update the PlayerInputManager's player prefab
            if (inputManager != null && gameplayPlayerPrefab != null)
            {
                inputManager.playerPrefab = gameplayPlayerPrefab;
                Debug.Log("PlayerInputManager prefab switched to gameplay prefab.");
            }
            else
            {
                Debug.LogError("Input Manager or gameplayPlayerPrefab not set.");
            }
        }
        else
        {
            Debug.LogError("PlayerConfigurationManager instance not found.");
            return; // Exit the function to avoid further errors
        }

        // Load the character select scene
        if (gm != null)
        {
            //UpdatePlayerPrefabsForCharacterSelect();
            //gm.CharacterSelect();
        }
        else
        {
            Debug.LogError("GameSceneManager reference is still null.");
        }
    }
    public void SwitchPlayerPrefabToGameplay()
    {
        if (inputManager != null)
        {
            inputManager.playerPrefab = gameplayPlayerPrefab; // Switch to gameplay prefab
            Debug.Log("Player prefab switched to gameplay version.");
        }
        else
        {
            Debug.LogError("PlayerInputManager is not set.");
        }
    }
    public void UpdatePlayerPrefabsForCharacterSelect()
    {
        Debug.Log($"Updating player prefabs for {playerConfigs.Count} players.");

        foreach (var playerConfig in playerConfigs)
        {
            Debug.Log($"Processing Player {playerConfig.PlayerIndex} for prefab switch.");
            if (playerConfig.Input != null && playerConfig.Input.gameObject != null)
            {
                Destroy(playerConfig.Input.gameObject);

                GameObject newPlayerObject = Instantiate(gameplayPlayerPrefab);
                newPlayerObject.transform.SetParent(transform);

                var newPlayerInput = newPlayerObject.GetComponent<PlayerInput>();
                if (newPlayerInput != null)
                {
                    newPlayerInput.actions = playerConfig.Input.actions;
                    playerConfig.Input = newPlayerInput;
                    Debug.Log($"PlayerInput component reassigned for Player {playerConfig.PlayerIndex}.");
                }
                else
                {
                    Debug.LogError("Gameplay prefab does not have a PlayerInput component.");
                }
            }
            else
            {
                Debug.LogError($"Player configuration incorrect or missing for Player {playerConfig.PlayerIndex}.");
            }
        }

    }
    public void ToSystemSettings(){
        gm.System_Settings();
    }
    public void ToQuit(){
        gm.Quit();
    }
    public void ToCredits(){
        gm.Credits();
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        pi.transform.SetParent(transform);

        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            // check if maxPlayers has been reached
            if (playerConfigs.Count < maxPlayers)
            {
                var newConfig = new PlayerConfiguration(pi);
                playerConfigs.Add(newConfig);

                // Ensure PCInfo has enough elements and is synchronized
                if (PCInfo.Count < playerConfigs.Count)
                {
                    PlayerCharacterInfoSO newPCInfo = ScriptableObject.CreateInstance<PlayerCharacterInfoSO>();
                    newPCInfo.PlayerIndex = pi.playerIndex;
                    PCInfo.Add(newPCInfo);
                }
                else
                {
                    Debug.LogError("PCInfo count does not match playerConfigs count, synchronization issue.");
                }
            }
            else
            {
                Debug.LogWarning("Max Players reached. Cannot add more players.");
            }
        }
    }
    public void KMS(){
        Debug.Log("kms");
        Destroy(this.gameObject);
    }

}

public class PlayerConfiguration 
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Material PlayerColour { get; set; }
    public int PlayerCharacterIndex { get; set; }
    public Material playerCharacterColor { get; internal set; }
}
