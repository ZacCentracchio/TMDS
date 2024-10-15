using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameUIManager : MonoBehaviour
{
    private int mashCount;

    public void OnMash(){
        mashCount++;
        Debug.Log(mashCount);
    }
}
