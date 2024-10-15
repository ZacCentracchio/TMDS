using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneManager : MonoBehaviour
{
    public GameObject meleeCane, marchCane;
    public ParticleSystem meleeEffect;

    public void meleeCaneOn(){
        meleeCane.SetActive(true);
        marchCane.SetActive(false);
        Debug.Log("Melee Effect Active");
        meleeEffect.gameObject.SetActive(true);
        meleeEffect.Play();
    }
    public void marchCaneOn(){
        marchCane.SetActive(true);
        meleeCane.SetActive(false);
        meleeEffect.Stop();
    }
}
