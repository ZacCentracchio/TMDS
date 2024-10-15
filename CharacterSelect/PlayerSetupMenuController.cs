using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject colorPanel;
    [SerializeField]
    private GameObject characterPanel;
    [SerializeField]
    private Button readyButton;
    [SerializeField]
    private Button colorButton;
    


    private float ignoreInputTime = 1.0f;
    private bool inputEnabled;
    public void setPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
        Debug.Log("PlayerSetupMenuController: Setting player index " + pi);
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

    public void SelectColor(Material mat)
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.SetPlayerColour(playerIndex, mat);
        readyPanel.SetActive(true);
        readyButton.interactable = true;
        //colorPanel.SetActive(true);
        //colorPanel.interactable = false;
        readyButton.Select();
        Debug.Log("Color is set");

    }
    public void SelectCharacter(int characterIndex)
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.SetPlayerCharacter(playerIndex, characterIndex);
        colorPanel.SetActive(true);
        colorButton.Select();
        //characterPanel.SetActive(false);
        Debug.Log("Character is set");
        //colorPanel.interactable = true;
        

    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }
    public void UnReadyPlayer(){
        if (!inputEnabled) { return; }
        
        PlayerConfigurationManager.Instance.UnReadyPlayer(playerIndex);
        characterPanel.gameObject.SetActive(true);
        readyPanel.SetActive(false);
        colorPanel.SetActive(true);
        //characterPanel.interactable = true;
        
    }  
    public void LeaveScreen(){
        PlayerConfigurationManager.Instance.ToMapSelect();
    } 
    
    
}
