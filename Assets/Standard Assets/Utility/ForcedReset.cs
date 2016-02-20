using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (GUITexture))]
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // if we have forced a reset ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            //... reload the scene CS0618
            #pragma warning disable 0618
            Application.LoadLevelAsync(Application.loadedLevelName);
            #pragma warning restore 0618
        }
    }
}
