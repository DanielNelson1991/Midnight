/*  Loading Scene Manager Namespace Class
    
Description: This Namespace is used for simplicity when handling scene states. 
This Namespace does not require any inhertience from any base class, and does not need to be attatched 
to a GameObject within the scene. Its main purpose is to handle the functionality between scenes. 

This also cleans up code as writing a parent class with Get and Set methods would still require a reference to that script. 
This method of management allows for 'LoadingSceneManager' to be put at the top of any file, and a simple function call.

*/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace LoadingSceneManager
{
    public class LoadScene
    {

        public static string loadSceneName;

        public static string ChangeSceneName(string sceneName)
        {
            return sceneName;
        }

    }
}
