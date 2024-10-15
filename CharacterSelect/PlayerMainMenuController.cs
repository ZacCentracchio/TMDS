using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMainMenuController : MonoBehaviour
{
    private int playerIndex;
    private float ignoreInputTime = 1.0f;
    private bool inputEnabled;
     [SerializeField]
    private Button localPlayButton;
    public void setPlayerIndex(int pi)
    {
        playerIndex = pi;
        //titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
        Debug.Log("PlayerMainMenuController: Setting player index " + pi);
    }

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }
    public void ToGameSettings(){
        if (!inputEnabled) { return; }
         PlayerConfigurationManager.Instance.ToGameSettings();
         localPlayButton.Select();
    }
    public void ToMapSelect(){
        if (!inputEnabled) { return; }
         PlayerConfigurationManager.Instance.ToMapSelect();
    }
    public void ToMainMenu(){
        if (!inputEnabled) { return; }
         PlayerConfigurationManager.Instance.ToMainMenu();
    }
    public void ToCharacterSelect(){
        if (!inputEnabled) { return; }
         PlayerConfigurationManager.Instance.ToCharacterSelect();
         
    }
    public void ToSystemSettings(){
        if (!inputEnabled) { return; }
         PlayerConfigurationManager.Instance.ToSystemSettings();
    }
    public void ToQuit(){
        if (!inputEnabled) { return; }
         PlayerConfigurationManager.Instance.ToQuit();
    }
    public void ToCredits(){
        if (!inputEnabled) { return; }
         PlayerConfigurationManager.Instance.ToCredits();
    }
    
}
