using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStats : MonoBehaviour
{
    private Transform endGameScreen;
    public GameObject PlayerStatCard;
    public int wonRoundNum, playerKillsNum, playerDeathsNum;
    public int hitsLanded, hitPercentNum;
    public int successfulParriesNum, meleeAttacksNum, rangedAttacksNum, blastardsThrownNum;
    public int damageDealtNum, damageTakenNum, staminaUsedNum;

    public static TMP_Text roundWins, kills, deaths,  damageDealt, damageTaken, staminaUsed, hitPercentage, rangedAttacksTxt, meleeAttacks, blastardsThrownTxt, successfulParries;
    public TMP_Text[] textArray = {roundWins, kills, deaths,  damageDealt, damageTaken, staminaUsed, hitPercentage, rangedAttacksTxt, meleeAttacks, blastardsThrownTxt};

    // Start is called before the first frame update
    void Start()
    {
        
        //textArray.Add(ranged, roundWins, kills, deaths, hitPercentage, parries, dashes, blastards);
    }

    public void AddWin()
    {
        wonRoundNum++;
    }
    public void AddKills()
    {
        playerKillsNum++;
    }
    public void AddDeaths()
    {
        playerDeathsNum++;
    }
    public void AddMeleeAttacks()
    {
        meleeAttacksNum++;
    }
    public void AddRangedAttacks(){
        rangedAttacksNum++;
    }
    public void AddHitsLanded()
    {
        hitsLanded++;
    }
    public void AddBlastardsThrown()
    {
        blastardsThrownNum++;
    }
    public void AddSuccessfulParries()
    {
        successfulParriesNum++;
    }
    public void HitPercent()
    {
        hitPercentNum = hitsLanded / (meleeAttacksNum + rangedAttacksNum);
    }
    public void DamageDealt(int damage){
        damageDealtNum += damage;
    }
    public void DamageTaken(int damage){
        damageTakenNum += damage;
    }
    public void StaminaUsed(int stamina){
        staminaUsedNum += stamina;
    }

    public void SetWinScores()
    {
        textArray[1].text = wonRoundNum.ToString();
        textArray[1].text = playerKillsNum.ToString();
        textArray[2].text = playerDeathsNum.ToString();
        textArray[3].text = damageDealtNum.ToString();
        textArray[4].text = damageTakenNum.ToString();
        textArray[5].text = staminaUsedNum.ToString();
        textArray[6].text = hitPercentNum.ToString();
        textArray[7].text = meleeAttacksNum.ToString();
        textArray[8].text = rangedAttacksNum.ToString();
        textArray[9].text = blastardsThrownNum.ToString();

    }
    public void CreateStatPanel(){
        endGameScreen = GameObject.Find("StatCardParent").transform;
        GameObject statCard = Instantiate(PlayerStatCard, endGameScreen) as GameObject;
        for(int i = 0; i < textArray.Length; i++){
            textArray[i] = statCard.GetComponent<StatHolder>().textArray[i];
        }
        SetWinScores();
        
    }

}
