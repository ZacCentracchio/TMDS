using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "GameRules")]
public  class GameRulesSO : ScriptableObject
{
    //Health Variables
    public float Rounds;
    public float TimePerRound;
    public int PlayerCount;
    public string MapChosen = "Map";


}