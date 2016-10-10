using UnityEngine;
using System.Collections;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FacebookIntegration : MonoBehaviour {

	public List<string> fb_params = new List<string>(){"public_profile", "email", "user_friends"};

	void Awake()
	{
		Scene s = SceneManager.GetActiveScene();
		if(s.name == "MainMenu")
		{
			if(!FB.IsInitialized) {
				FB.Init(InitCallback, OnHideUnity);
			} else {
				FB.ActivateApp();
			}
		}
	}

	private void InitCallback()
	{
		if(FB.IsInitialized) {
			FB.ActivateApp();
		} else {
			Debug.Log("Failed to initialize facebook");
		}
	}

	private void OnHideUnity(bool isGameShown)
	{
		if(!isGameShown)
		{
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	private void AuthCallback(LoginResult result) {
		if(FB.IsLoggedIn) {
			AccessToken aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

			Debug.Log(aToken.UserId);

			foreach(string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
		} else {
			Debug.Log("User cancelled");
		}
	}

}
