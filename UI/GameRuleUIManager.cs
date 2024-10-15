using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameRuleUIManager : MonoBehaviour
{

    public TextMeshProUGUI countTxt;
    public float count;
    public float countModifier;
    //public Button minus, add;

    public void AddCount(){
        count += countModifier;

        countTxt.text = count.ToString();
    }
    public void MinusCount(){
        count -= countModifier;
    
        countTxt.text = count.ToString();
    }

        
    
}
