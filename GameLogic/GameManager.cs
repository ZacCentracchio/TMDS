using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator splashScreenAnim;

    public void OpenGame(){
        //Debug.Log("started");
        splashScreenAnim.SetBool("Start",true);
        
    }


}