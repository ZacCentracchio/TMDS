using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinIndex : MonoBehaviour
{
    public int playerIndex;
    private Material healthMaterial;
    private Material staminaMaterial;
    public GameObject HealthBar;
    public GameObject ShotCounter;
    private GameObject Player;
    public GameplayManager gm;

    void Awake(){
        gm = GameObject.Find("GameManager").GetComponent<GameplayManager>();
    }
    public void GetPlayer(){
        
    }
    private void SetHealth(){

    }

}
