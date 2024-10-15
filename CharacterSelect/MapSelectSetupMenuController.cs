using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class MapSelectSetupMenuController : MonoBehaviour
{
    private int playerIndex;
    private float ignoreInputTime = 1.0f;
    private bool inputEnabled;
    void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }
    public void setPlayerIndex(int pi)
    {
        playerIndex = pi;
        Debug.Log("MapSelectSetupMenuController: Setting player index " + pi);
    }

    public void SelectMap()
    {
        if (!inputEnabled) { return; }
        PlayerConfigurationManager.Instance.SelectMap(playerIndex);
    }
}
