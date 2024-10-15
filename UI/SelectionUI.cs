using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FMODUnity;
 
public class SelectionUI : MonoBehaviour
{
    public EventSystem EventSystem;
    public List<Selectable> Selectables = new List<Selectable>();
    public GameObject backButton;
    private GameObject lastSelected;

 
    private void Start()
    {
        EventSystem = FindObjectOfType<EventSystem>(true);
        GetChildSelectables();
        lastSelected = EventSystem.currentSelectedGameObject;
    }
    private void Update()
    {
        if (EventSystem.currentSelectedGameObject != null && EventSystem.currentSelectedGameObject.activeInHierarchy)
        {
            if (EventSystem.currentSelectedGameObject != lastSelected)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Navigate, this.transform.position);
                lastSelected = EventSystem.currentSelectedGameObject;
            }
        }
        else
        {
            foreach (var item in Selectables)
            {
                if (item.isActiveAndEnabled)
                {
                    EventSystem.SetSelectedGameObject(item.gameObject);
                    Debug.Log("Selected: " + item.name);
                    PlaySound(FMODEvents.instance.Select);
                    return;
                }
            }
        }
    }

    public void OnConfirmSelection()
    {
        PlaySound(FMODEvents.instance.Select); // Play select sound when a selection is confirmed
    }

    private void PlaySound(EventReference soundEvent)
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.Select, this.transform.position);
    }

    [ContextMenu("GetChildSelectables")]
    public void GetChildSelectables()
    {
        Selectables.Clear();
        Selectables.AddRange(gameObject.GetComponentsInChildren<Selectable>(true));
    }
    public void SetLocalPlayAsActive(){
        EventSystem.SetSelectedGameObject(Selectables[1].gameObject);
    }
    public void SetBackButtonAsActive(){
        Debug.Log("backButton");
        EventSystem.SetSelectedGameObject(backButton);
    }
}