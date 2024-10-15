using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class perkManager : MonoBehaviour
{

    //[SerializeField] public GameObject Player;

    private GameplayManager gm;
    private GameplayUIManager guim;
    public StatPerk perk;
    public GameEvent perkChosen;

    private void Awake() {
        gm = FindObjectOfType<GameplayManager>();
        guim = FindObjectOfType<GameplayUIManager>();
    }
    public void PerkChosen(){
        //perkChosen.Raise(this, null);
        guim.activePerkList.Remove(this.gameObject);
        gm.AddPerkToPlayer(perk);
    }
    public void DestroyPerk(){
        Destroy(this.gameObject);
    }
}
