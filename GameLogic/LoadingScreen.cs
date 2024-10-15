using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    public GameObject Background;
    public Animator anim;
    public GameRulesSO gr;
    public string toSceneName;
    public void Start(){
        toSceneName = gr.MapChosen;
        LoadScene(toSceneName);
    }
    public void LoadScene(string sceneID){
        StartCoroutine(LoadSceneAsync(sceneID));
    }


    IEnumerator LoadSceneAsync(string sceneID){

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        //LoadingScreenUI.SetActive(true);
        float loadTime = Time.deltaTime;
        while(!operation.isDone){

            //anim.SetBool("isLoaded", false);
            //float progressValue = Mathf.Clamp01(operation.progress / 0.09f);
            Debug.Log("Waiting");

            //LoadingBarFill.fillAmount = progressValue;
            if(loadTime < 3){
                yield return new WaitForSeconds(2f);
                
            }
            yield return null;
        }
        anim.SetBool("isLoaded", true);
        
        
    }

}
