using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CharacterSwitchUI : MonoBehaviour
{
    public Button[] activeButtons;
    public Button[] colorButtons;
    public Button[] characterButtons;
    public Button readyButton;
    private int index = 0;
    private PlayerControls controls;
    public PlayerInput input;
    private bool isFirstButtonPressed;
    private int escapeCheck;
    private bool characterButtonsOn = true;
    private bool buttonsOn = true;
    private bool isInputBlocked = false;

    public PlayerSetupMenuController playerSetupMenuController;

    // DoTween Variables
    public float moveDistance = 300f;
    public float fadeTime = 1f;

    // Stores the original positions of the buttons
    private Vector3[] originalPositions;

    public void Initialize(PlayerInput playerInput)
    {
        controls = new PlayerControls();
        controls.devices = playerInput.devices;

        controls.UI.Next.performed += ctx => Next();
        controls.UI.Previous.performed += ctx => Previous();
        controls.UI.Submit.performed += ctx => CheckActiveButtons();
        controls.UI.Back.performed += ctx => Back();

        controls.UI.Enable();
        Debug.Log("CharacterSwitchUI: Initializing for player index " + playerInput.playerIndex);
    }
    private void OnDisable()
    {
        controls.UI.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetCharacterButtons();
    }




    private void GetButtonPositions(){
        originalPositions = new Vector3[activeButtons.Length];

        for (int i = 0; i < activeButtons.Length; i++)
        {
            // Save original positions
            originalPositions[i] = activeButtons[i].transform.localPosition;

            // Set initial opacity for buttons
            activeButtons[i].GetComponent<CanvasGroup>().alpha = i == 0 ? 1 : 0;
        }
        ActivateAndFadeInCurrentButton();
    }



    public void CheckActiveButtons(){
        if(!characterButtonsOn){
            SelectColor();
            
            //Debug.Log("CheckActiveButton");
        }
        else if(characterButtonsOn){
            SelectCharacter();
        }
    }




    public void SetCharacterButtons(){
        for(int i = 0; i < characterButtons.Length; i++){
            activeButtons[i] = characterButtons[i];
            Debug.Log("character is active");
        }
        GetButtonPositions();
    }
    public void SetColorButtons(){
        for(int i = 0; i < colorButtons.Length; i++){
            activeButtons[i] = colorButtons[i];
            Debug.Log("Color is active");
        }
        GetButtonPositions();
    }



    public void Next()
    {
        if (buttonsOn && !isInputBlocked)
        {
            isInputBlocked = true; // Block further input until animation completes
            MoveAndFadeOut(activeButtons[index], () => {
                index = (index + 1) % activeButtons.Length;
                MoveAndFadeIn(activeButtons[index], () => {
                    isInputBlocked = false; // Re-enable input after animation
                });
            });
        }
    }

    public void Previous()
    {
        if (buttonsOn && !isInputBlocked)
        {
            isInputBlocked = true; // Block further input until animation completes
            MoveAndFadeOutLeft(activeButtons[index], () => {
                index = (index - 1 + activeButtons.Length) % activeButtons.Length;
                MoveAndFadeInRight(activeButtons[index], () => {
                    isInputBlocked = false; // Re-enable input after animation
                });
            });
        }
    }
    public void Back(){
        Debug.Log("Clicked back");

        //activeButtons[index].onClick.Invoke();
        //EnableActiveButtons();
        SetCharacterButtons();
        characterButtonsOn = true;
        buttonsOn = true;
        playerSetupMenuController.UnReadyPlayer();
        escapeCheck++;

        if(escapeCheck == 3){
            playerSetupMenuController.LeaveScreen();
        }
    }


    private void SelectColor()
    {
        Debug.Log("Color selected at index: " + index);

        activeButtons[index].onClick.Invoke();
        buttonsOn = false;
        //isFirstButtonPressed = true;
        playerSetupMenuController.ReadyPlayer();
        //DisableActiveButtons();

    }
    private void SelectCharacter()
    {
        //Debug.Log("Character selected at index: " + index);
        activeButtons[index].onClick.Invoke();
        SetColorButtons();
        characterButtonsOn = false;
        escapeCheck--;
        escapeCheck = Mathf.Clamp(escapeCheck, 0, 2);
        
    }


    
    private void ActivateAndFadeInCurrentButton()
    {
        if (index >= 0 && index < activeButtons.Length)
        {
            activeButtons[index].gameObject.SetActive(true);
            CanvasGroup canvasGroup = activeButtons[index].GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = activeButtons[index].gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.DOFade(1, fadeTime);
        }
    }

    private void MoveAndFadeOut(Button button, Action onComplete)
    {
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>() ?? button.gameObject.AddComponent<CanvasGroup>();

        Sequence sequence = DOTween.Sequence().SetAutoKill(false);
        sequence.Append(button.transform.DOLocalMoveX(button.transform.localPosition.x + moveDistance, fadeTime));
        sequence.Join(canvasGroup.DOFade(0, fadeTime));
        sequence.OnComplete(() => {
            button.gameObject.SetActive(false);
            button.transform.localPosition = originalPositions[Array.IndexOf(activeButtons, button)];
            onComplete?.Invoke();  // Call the passed callback function
        });
    }



    private void MoveAndFadeIn(Button button, Action onComplete = null)
    {
        // Ensure the button is active
        button.gameObject.SetActive(true);

        // Get or add a CanvasGroup to manage the button's transparency
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
        }

        // Start the button at an offset position so it can move into its original position
        button.transform.localPosition = new Vector3(button.transform.localPosition.x - moveDistance, button.transform.localPosition.y, button.transform.localPosition.z);

        // Create a DOTween sequence to coordinate the movement and fading
        Sequence sequence = DOTween.Sequence();

        // Append movement to the sequence, moving the button to its original position
        sequence.Append(button.transform.DOLocalMoveX(originalPositions[Array.IndexOf(activeButtons, button)].x, fadeTime));

        // Join fading to the sequence, changing the alpha from 0 to 1 to make the button visible
        sequence.Join(canvasGroup.DOFade(1, fadeTime));

        // Optional: Execute any additional actions once the animation sequence is complete
        if (onComplete != null)
        {
            sequence.OnComplete(() => onComplete());
        }
    }

    private void MoveAndFadeOutLeft(Button button, Action onComplete)
    {
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>() ?? button.gameObject.AddComponent<CanvasGroup>();

        Sequence sequence = DOTween.Sequence().SetAutoKill(false);
        sequence.Append(button.transform.DOLocalMoveX(button.transform.localPosition.x - moveDistance, fadeTime));
        sequence.Join(canvasGroup.DOFade(0, fadeTime));
        sequence.OnComplete(() => {
            button.gameObject.SetActive(false);
            button.transform.localPosition = originalPositions[Array.IndexOf(activeButtons, button)];
            onComplete?.Invoke();  // Call the passed callback function
        });
    }

    private void MoveAndFadeInRight(Button button, Action onComplete = null)
    {
        button.gameObject.SetActive(true);

        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
        }

        button.transform.localPosition = new Vector3(button.transform.localPosition.x + moveDistance, button.transform.localPosition.y, button.transform.localPosition.z);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(button.transform.DOLocalMoveX(originalPositions[Array.IndexOf(activeButtons, button)].x, fadeTime));
        sequence.Join(canvasGroup.DOFade(1, fadeTime));

        if (onComplete != null)
        {
            sequence.OnComplete(() => onComplete());
        }
    }


}
