using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAnimationEvent : MonoBehaviour
{
    public GameplayManager gm;
    public GameplayUIManager guim;
    public SelectionUI selectionUI;

    public Animator countdownAnimator;
    public void StartRoundCountdown(){
        countdownAnimator.SetTrigger("RoundStart");
    }
    
    public void PlayEvent(){
        gm.StartRound();
    }
    public void SelectButton(){
        selectionUI.SetLocalPlayAsActive();
    }
    public void SelectBackButton(){
        selectionUI.SetLocalPlayAsActive();
    }
}
