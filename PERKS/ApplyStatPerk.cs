using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class ApplyStatPerk : MonoBehaviour
{
    [SerializeField] StatPerk _data;
    private GameObject player;
    //public static GuiController singleton = null;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(_data != null)
        {
            //HandleEffect();
        }
    }
    public void SelectPerk() 
    {
        var effectable = player.GetComponent<IEffectable>();
        effectable.ApplyPerk(_data);
        Destroy(this.gameObject);
    }

}
