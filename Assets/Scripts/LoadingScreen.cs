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

    AsyncOperation async;
    
    // Use this for initialization
    void Start () {
        StartCoroutine(LoadLevelAsync());
        async.allowSceneActivation = false;
	}

    private IEnumerator LoadLevelAsync()
    {
        async = SceneManager.LoadSceneAsync("demo_night");

        while (!async.isDone)
        {

            yield return null;

        }

        Debug.Log(loadProgress);
    }

    void Update()
    {
        Debug.Log(async.progress);
        loadProgress = "Loading..." + (async.progress * 90f).ToString("F0") + "%";

        if(async.isDone)
        {
            if(Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                async.allowSceneActivation = true;
            }
            
            loadProgress = "Press Enter to Begin...";
        }
    }


    void OnGUI()
    {
        GUI.skin = loadingSkin;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture, ScaleMode.ScaleToFit);
        GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2, 300, 100), loadProgress, loadingSkin.GetStyle("LoadingText"));
    }



}
