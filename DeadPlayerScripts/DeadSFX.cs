using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSFX : MonoBehaviour
{
    public AudioSource clapSFX, booSFX; 
    
    public void PlayClapSFX(){
        //clapSFX.Play();
        Debug.Log("Clap");
    }
    public void PlayBooSFX(){
        //booSFX.Play();
        Debug.Log("Booooooooo");
    }


}
