using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiController : MonoBehaviour
{
    public static GuiController singleton = null;
    // Start is called before the first frame update
    void Awake()
    {
        if(singleton != null )
        {
            singleton = this;
            DontDestroyOnLoad( this.gameObject );
        }
        else if( singleton != this ) 
        {
            Destroy( this.gameObject );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
