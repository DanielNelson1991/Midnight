using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MainEditor : EditorWindow {

	// Create the Main Editor Menu of type audio
	[MenuItem("Midnight Game/Create Horror Event Trigger/Audio")]
	static void CreateAudioTrigger()
	{
		GameObject go;
		Camera editorCamera = Camera.current;
		Debug.Log("Creating Audio Trigger");
		if(GameObject.Find("List of Horror Triggers") == null)
		{
			go = new GameObject();
			go.name = "List of Horror Triggers";
			Debug.Log("Succesfully created Horror Triggers main");

			GameObject go_trigger = new GameObject();
			go_trigger.name = "Horror Trigger";
			go_trigger.transform.parent = GameObject.Find("List of Horror Triggers").transform;
			go_trigger.transform.position = editorCamera.transform.position;
			go_trigger.AddComponent<HorrorTrigger>();
		}
		else {
			GameObject go_trigger = new GameObject();
			go_trigger.name = "Horror Trigger";
			go_trigger.transform.parent = GameObject.Find("List of Horror Triggers").transform;
			go_trigger.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
			go_trigger.transform.position = editorCamera.transform.position;
			go_trigger.AddComponent<HorrorTrigger>();
		}


	}

    [MenuItem("Midnight Game/Open Scene/Demo Night")]
    static void OpenDemoNight()
    {
        OpenScene("demo_night");
    }

    [MenuItem("Midnight Game/Open Scene/Credits Scene")]
    static void OpenCreditsScene()
    {
        OpenScene("CreditsScene");
    }

    [MenuItem("Midnight Game/Open Scene/Main Menu")]
    static void OpenMainMenu()
    {
        OpenScene("MainMenu");
    }

    [MenuItem("Midnight Game/Open Scene/Loadin Scene")]
    static void OpenLoadingScene()
    {
        OpenScene("LoadingScene");
    }

    static void OpenScene(string levelName)
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        if(levelName == "demo_night")
        {
            EditorSceneManager.OpenScene("Assets/Models/DirtyApartments/_scenes/" + levelName + ".unity");
        }
        else
        {
            EditorSceneManager.OpenScene("Assets/Scenes/" + levelName + ".unity");
        }
        
    }


}
