using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    //Call the list of players here
    //public GameSettings settings;
    //public PlayerManager player;
    
    public GameRulesSO gameRules;
    public GameplayUIManager guim;
    public GameSceneManager gsm;
    public MultipleTargetCamera cam;
    public CallAnimationEvent callAnimationEvent;
    [SerializeField] 
    private Transform[] PlayerSpawns;
    
    public UIManager ui;
    public GameObject endScreen; 
    public List<GameObject> placements;
    public float gameTimer, gameTimerReset, gameTimerMax;
    public float roundCount = 0;
    public float roundCountMax;
    public int playerCount;
    public bool isPickingPerks, isPlaying;
    public float resetTimer = 5f;
    private float roundToggle = 1;
    public int currPlacementNum = 0;
    public Transform playerCoinParent;
    
    
    [SerializeField] 
    public GameObject[] Players;
    
    //public UnityEvent RoundOverEvent, PlayerDiesEvent;


    // Start is called before the first frame update
    void Awake()
    {
        roundCountMax = 3; 
        gameTimerMax = 60;
        
        /*
        roundCountMax = gameRules.Rounds; 
        gameTimerMax = gameRules.TimePerRound;
        */
        gsm = GetComponent<GameSceneManager>();
    }
    void Start(){
        AddPlayers();
        SetEachPlayerDeadControls();
        
    }
    // Update is called once per frame
    void Update()
    {
        if(playerCount == 1)
        {
            OnDeclaredWinner();
            
        }

    }
    public void AddPlayers()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }
    public void StartRound(){
        isPlaying = true;
        guim.timer.SetActive(true);
        guim.playerCoins.SetActive(true);
        cam.AddTargets();
        SetEachPlayerCombatControls();
        
    }
    public void FirstRoundLogic(){
        roundCountMax = gameRules.Rounds;
        gameTimerMax = gameRules.TimePerRound;

    }
    public void PlayerDies()
    {
        cam.RemoveTargets();
        playerCount-- ;
    }
    public void RoundFlip()
    {
        roundToggle = roundToggle * -1;
        
    }



    // everything that happens when the second round starts
    public void NextCombatRoundStart()
    {
        ResetPlayerHealth();
        PickPerksClose();
        ResetLocation();
        guim.ResetTimer();
        callAnimationEvent.StartRoundCountdown();
        
    }
    public void SetEachPlayerDeadControls(){
        Debug.Log("Dead Controls");
        foreach(GameObject player in Players){
            
            player.GetComponent<PlayerInputHandler>().SetDeadControls();
            player.GetComponent<HandleEventSystem>().EventSystemOff();
            //Debug.Log(player.);
        }
        //ChangePlayerChoosingPerk();
    }
    public void SetEachPlayerCombatControls(){
        Debug.Log("CombatControls");
        foreach(GameObject player in Players){
            player.GetComponent<PlayerInputHandler>().SetCombatControls();
        }
    }
    public void ResetPlayerHealth(){
        foreach(GameObject player in Players){
            player.GetComponent<PlayerHealth>().Respawn();
        }
    }

    public void PickPerksOpen()
    {
        guim.TurnOnPerkScreen();
        //ChangePlayerChoosingPerk();
        isPickingPerks = true;
    }
    public void PickPerksClose()
    {
        guim.TurnOffPerkScreen();
        isPickingPerks = false;
    }

    public void PlacePlayersOnTimeOut(){
        var mostPlayerHealth = Players[0];
        placements = Players.ToList().OrderByDescending(x => x.GetComponent<PlayerHealth>().currentHealth).ToList();
        for (int i = 1; i < Players.Length; i++){
            placements[i].GetComponent<StatsManager>().roundPlacement = (float)i;
            if(Players[i].GetComponent<PlayerHealth>().currentHealth >= mostPlayerHealth.GetComponent<PlayerHealth>().currentHealth){
                mostPlayerHealth = Players[i];
            }
        }
        isPlaying = false;
        
        //return mostPlayerHealth;
    }
    

    public void AwardPoints(){
        int pointAmount = Players.Length;
        for(int i = 0; i < Players.Length; i++){
            placements[i].GetComponent<StatsManager>().pointCount += pointAmount;
            pointAmount--;
        }
    }

    
    public void ChangePlayerChoosingPerk(){
       
        //placements[currPlacementNum].GetComponent<PlayerInputHandler>().SetGameUIControls();
        if(currPlacementNum < Players.Length){
             currPlacementNum++;
            if(currPlacementNum > 1){
                currPlacementNum = 1;
            }
            else if(currPlacementNum < 0){
                currPlacementNum = 0;
            }
            
            
            Debug.Log(placements[currPlacementNum].name);
            placements[currPlacementNum].GetComponent<PlayerInputHandler>().SetGameUIControls();
            placements[currPlacementNum].GetComponent<HandleEventSystem>().EventSystemOn();
            
        }

    }
    public void AddPerkToPlayer(StatPerk perk){
        StatsManager ActivePlayer = placements[currPlacementNum].GetComponent<StatsManager>();
        placements[currPlacementNum].GetComponent<HandleEventSystem>().EventSystemOff();
        ActivePlayer.statPerks.Add(perk);
        ActivePlayer.SetNewStats(perk);
        if(guim.activePerkList.Count <= 1){

            NextCombatRoundStart();
            currPlacementNum = -1;
            
        }
        else{
            guim.SetFirstPerk();
            ChangePlayerChoosingPerk();
        }
        
    }
    public void ResetLocation(){
         for (int i = 0; i < gameRules.PlayerCount ; i++)
        {
            //Debug.Log(PCInfo[i]);
            Players[i].transform.position = PlayerSpawns[i].position;
           
        
        }
    }
    //when the game timer runs out this function is called to identify which alive players have the most health
    public void OnTimeRunsOut(){
        
        PlacePlayersOnTimeOut();
        roundCount++;
        if(roundCount >= roundCountMax){
            GameOver();
            return;
        }
        
        isPlaying = false;
        PickPerksOpen();
        SetEachPlayerDeadControls();
        ChangePlayerChoosingPerk();
        AwardPoints();
    }
    public void OnDeclaredWinner(){
        roundCount++;
        if(roundCount >= roundCountMax){
            GameOver();
            return;
        }
        //PlayerPlacements();
        isPlaying = false;
        PickPerksOpen();
        SetEachPlayerDeadControls();
        ChangePlayerChoosingPerk();
        AwardPoints();
    }

    public void GameOver()
    {
        SetEachPlayerDeadControls();
        guim.EndGameScreenOn();
        guim.playerCoins.SetActive(false);
        //Time.timeScale = 0f;
        foreach(GameObject player in placements){
            player.GetComponent<GameStats>().CreateStatPanel();
        }

        StartCoroutine(StaticScreen());

    }
    IEnumerator StaticScreen()
    {
        Debug.Log("StartCountdown");
        yield return new WaitForSeconds(5f); 
        Debug.Log("StartFade");
        StartCoroutine(FadeToMenu());
    }
    IEnumerator FadeToMenu()
    {
        endScreen.SetActive(true);
        Color c = endScreen.GetComponent<Image>().color;
        for (float alpha = 0f; alpha >= 1; alpha += 0.03f)
        {
            c.a = alpha;
            endScreen.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log("FadeDone");
        gsm.MainMenu();
    }
    //public void 
}
