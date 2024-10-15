using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public GameRulesSO gameRules;
    private string frontierCapitol = "FrontierCapitol";
    private string trapsiteOne = "TrapsiteOne";
    private string dwarfJuno = "DwarfJuno";
    private string floppingtonCity = "FloppingtonCity";
    public LoadingScreen loadingScreen;
    public List<string> MapList = new List<string>();
    private void Awake()
    {
        MapList.Add(frontierCapitol);
        MapList.Add(trapsiteOne);
        MapList.Add(dwarfJuno);
        MapList.Add(floppingtonCity);

        DontDestroyOnLoad(this.gameObject);
    }
    public void SelectRandomMap(){
        int num = Random.Range(0, 4);

        if(num == 0){
            SelectFrontierCapitol();
        }
        else if(num == 1){
            SelectTrapsiteOne();
        }
        else if(num == 2){
            SelectDwarfJuno();
        }
        else if(num == 3){
            SelectFloppingtonCity();
        }
    }
    public void SelectFrontierCapitol(){
        SetMap(frontierCapitol);
    }
    public void SelectTrapsiteOne(){
        SetMap(trapsiteOne);
    }
    public void SelectDwarfJuno(){
        SetMap(dwarfJuno);
    }
    public void SelectFloppingtonCity(){
        SetMap(floppingtonCity);
    }
    public void CharacterSelect()
    {
        OnCharacterSelectButtonPressed();
        SceneManager.LoadScene("Character_Select 1");
    }
    public void MapSelect()
    {
        SceneManager.LoadScene("Map_Select");
    }
    public void SetMap(string MapName){
        gameRules.MapChosen = MapName;
        Debug.Log(MapName);
    }
    public void LoadMap(){

        if(gameRules.MapChosen == frontierCapitol){
            FrontierCapitol();
        }
        else if(gameRules.MapChosen == trapsiteOne){
            TrapsiteOne();
        }
        else if(gameRules.MapChosen == dwarfJuno){
            DwarfJuno();
        }
        else if(gameRules.MapChosen == floppingtonCity){
            FloppingtonCity();
        }
        Destroy(this.gameObject);
    }
    public void FrontierCapitol()
    {

        loadingScreen.LoadScene("FrontierCapitol");

    }
    public void TrapsiteOne()
    {
        loadingScreen.LoadScene("TrapsiteOne");
    }
    public void DwarfJuno()
    {
        loadingScreen.LoadScene("DwarfJuno");
    }
    public void FloppingtonCity()
    {
        Debug.Log("hasntLoaded");
        loadingScreen.LoadScene("FloppingtonCity");
        Debug.Log("hasLoaded");
    }

    public void SetPlayerCount(int playerCount){
        gameRules.PlayerCount = playerCount;
        Debug.Log("player Count is " + gameRules.PlayerCount);
        //LoadMap();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void System_Settings()
    {
        SceneManager.LoadScene("System_Settings");
    }
    public void Game_Settings()
    {
        SceneManager.LoadScene("Game_Rules");
    }
    public void Loading_Screen()
    {
        SceneManager.LoadScene("Loading_Screen");
    }
    public void Quit()
    {
        UnityEngine.Application.Quit();
    }
    public void OnCharacterSelectButtonPressed()
    {
        Destroy(PlayerConfigurationManager.Instance.gameObject);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Character_Select 1")
        {
            //OnCharacterSelectButtonPressed();
            if (PlayerConfigurationManager.Instance != null)
            {
                //PlayerConfigurationManager.Instance.UpdatePlayerPrefabsForCharacterSelect();
                //PlayerConfigurationManager.Instance.SwitchPlayerPrefabToGameplay();
            }
            else
            {
                Debug.LogError("PlayerConfigurationManager instance not found.");
            }
        }
    }
}