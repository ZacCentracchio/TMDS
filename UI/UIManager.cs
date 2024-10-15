using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class UIManager : MonoBehaviour
{
    public GameObject perkScreen, WinScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void PerkScreen()
    {
        if(perkScreen != null)
        {
            perkScreen.SetActive(true);
            //StartCoroutine(perkScreenEnumerator());
        }
        else perkScreen.SetActive(false);

    }*/
    public void EndScreen()
    {
        
            WinScreen.SetActive(true);
        

    }
    IEnumerator perkScreenEnumerator()
    {

        yield return new WaitForSeconds(3);
        Debug.Log("Hello World");
        //PerkScreen();
    }
}
