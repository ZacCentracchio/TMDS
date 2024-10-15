using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    //[SerializeField] StatPerk _data;
    public List<string> destroyableObjects = new List<string>();
    public StatsManager statManager;
    public GameObject thisPlayer;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("MeleeHit");
        if(destroyableObjects.Contains(collision.tag = "Player")){
            collision.gameObject.GetComponent<PlayerHealth>().SendMessage("TakeDamage", statManager.meleeDamage);
            Debug.Log("DamageGiven");
        }
               
    }
}
