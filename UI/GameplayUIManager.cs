using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
public class GameplayUIManager : MonoBehaviour
{
    //Timer Variables
    public TextMeshProUGUI timerTxt;
    public GameObject timer;
    public GameplayManager gm;
    public float gameTimer, gameTimerReset, gameTimerMax;
    public GameEvent TimeRunOut;
    //public GameObject es;


    public EventSystem[] eventSystems;

    //Perk Screen Variuables
    public GameObject perkScreen, playerCoins, endGameScreen;
    public List<GameObject> currPerkList, activePerkList, emptyList;
    public GameObject[] currPerkListArray;
    public GameObject PerkParent;

    //PauseMenu variables
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    private bool isPaused;

    [SerializeField] private GameObject pauseMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;

    void Start (){
        gameTimerMax = gm.gameTimerMax;
        //gameTimerMax = 10;
        gameTimer = gameTimerMax;
        //ResetTimer();
        if (eventSystems.Length < 1)
        {
            eventSystems = FindObjectsOfType<EventSystem>();
            RandomizePerks();
        }
    }

    void LateUpdate()
    {

        if(gm.isPlaying){
            //Logic to control the timer
            if(gameTimer > 0){
                gameTimer -= Time.deltaTime;
                UpdateTimer(gameTimer);
            }
            if(gameTimer <= 0 ){
                Debug.Log("Times up!");
                timer.SetActive(false);
                gm.OnTimeRunsOut();
                
            }



        }
        
    }
    //HelperFunctions for Timer
    public void ResetTimer(){
        gameTimer = gameTimerMax;
        
    }
     public void UpdateTimer(float currentTime){
        //currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        //Debug.Log(timerTxt);

    }

    //Perk Screen Managing
    public void TurnOnPerkScreen(){
        perkScreen.SetActive(true);
        playerCoins.SetActive(false);
        Time.timeScale = 0f;
        activePerkList[0].GetComponent<Button>().Select();

        //RandomizePerks();
        //var es = EventSystem.current;
        //SetFirstPerk();
    }
    public void TurnOffPerkScreen(){
        perkScreen.SetActive(false);
        playerCoins.SetActive(true);
        Time.timeScale = 1f;
        timer.SetActive(true);
        RandomizePerks();

    }

    public void SetFirstPerk(){
        foreach(EventSystem eventSystem in eventSystems){
            eventSystem.firstSelectedGameObject = activePerkList[0];
            Debug.Log(eventSystem.firstSelectedGameObject.name);
        }
    }
    public void RandomizePerks(){
        currPerkListArray = Resources.LoadAll<GameObject>("_Perks");

        currPerkList = currPerkListArray.ToList();
        
        //Debug.Log(activePerkList);
        for(int i = 0; activePerkList.Count < gm.Players.Length + 1; i++){
            Debug.Log("perk");
            GameObject perkToChoose = currPerkList[Random.Range (0, currPerkList.Count)];
            GameObject newPerk = Instantiate(perkToChoose, PerkParent.transform) as GameObject;
            activePerkList.Add(newPerk);
        }
        SetFirstPerk();

    }

    //Pause Screen Managing
    public void PausePlay(){
        
        if(!isPaused){
            OnPause();
            Debug.Log("pause");
        }
        else{
            OnPlay();
        }
    }
    public void OnPause(){
        isPaused = true;
        Time.timeScale = 0f;
        OpenPauseMenu();
        //mover.enabled = false;
        //combat.enabled = false;
    }
    void OnPlay(){
        isPaused = false;
        Time.timeScale = 1f;
        CloseAllMenus();
        //mover.enabled = true;
        //combat.enabled = true;
    }
    private void OpenPauseMenu(){
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(pauseMenuFirst);
    }
    private void OpenSettingsMenu(){
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
    }
    private void CloseAllMenus(){
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void EndGameScreenOn(){
        endGameScreen.SetActive(true);
    }


    public void OnSettingsPressed(){
        OpenSettingsMenu();
    }
    public void OnResumePressed(){
        OnPlay();
    }
    public void OnSettingsBackPressed(){
        OpenPauseMenu();
    }

}
