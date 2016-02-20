/* LoadingScreen.cs - Version 0.1
        
            ** Change Log ** 

- First creating of script 20/12/2015

              ** Bugs **

- Loading text jumps straight from 0% to 90% [Fixed in Version 0.1. NOTE: Still stops at 90%]

*/ 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using LoadingSceneManager;

public class LoadingScreen : MonoBehaviour {

    

    public Texture backgroundTexture;
    public GUISkin loadingSkin;

    private string loadProgress = "Loading...";
#pragma warning disable 0414
    private float progress;
#pragma warning restore 0414
    private string newSceneName;
    
    // Use this for initialization
    void Start () {
        StartCoroutine(LoadLevelAsync());
	}

    private string lastLoadProgress = null;

    private IEnumerator LoadLevelAsync()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(LoadScene.ChangeSceneName(LoadScene.loadSceneName));
        async.allowSceneActivation = false;
        while(!async.isDone)
        {
            if(async.progress < 0.9f)
            {
                loadProgress = "Loading..." + (async.progress * 100f).ToString("F0") + "%";
            }
            else
            {
                if(Input.anyKeyDown)
                {
                    async.allowSceneActivation = true;
                }
                loadProgress = "Press [ENTER] to begin.";
            }
            if(lastLoadProgress != loadProgress)
            {
                lastLoadProgress = loadProgress;
                Debug.Log(loadProgress);
            }
            progress = async.progress * 100;
            yield return null;

        }
        loadProgress = "Load Complete";
        Debug.Log(loadProgress);
    }


    void OnGUI()
    {
        GUI.skin = loadingSkin;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture, ScaleMode.ScaleToFit);
        GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2, 300, 100), loadProgress, loadingSkin.GetStyle("LoadingText"));
    }



}
