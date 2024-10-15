using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuleSOManager : MonoBehaviour
{
    public GameRuleUIManager Rounds, TimePerRound;
    public GameRulesSO rulesSO;

    public void SetRules(){
        rulesSO.Rounds = Rounds.count;
        rulesSO.TimePerRound = TimePerRound.count;
    }
}
