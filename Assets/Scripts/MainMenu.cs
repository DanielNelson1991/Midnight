using UnityEngine;
using UnityEngine.SceneManagement;
using LoadingSceneManager;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    AsyncOperation async;
    public Texture backgroundTexture;
    public Texture backgroundTextureOptions;
    private string progress;
    bool optionsActive;
    public Dropdown dropdown;
    GameObject optionsMenu;
    GameObject mainMenu;
    void Awake()
    {
        mainMenu = GameObject.Find("Panel");
        optionsMenu = GameObject.Find("OptionsPanel");
    }

    // Use this for initialization
    void Start () {
        Debug.Log(QualitySettings.GetQualityLevel());

        optionsMenu.SetActive(false);
    }

    void Update()
    {
       
    }

    public void SceneChange(string buttonName)
    {
        switch(buttonName)
        {
            case "Play":
                LoadScene.loadSceneName = "demo_night";
                SceneManager.LoadScene("LoadingScene");
            break;

            case "Options":
                mainMenu.SetActive(false);
                optionsMenu.SetActive(true);
            break;

            case "Credits":
                SceneManager.LoadScene("CreditsScene");
            break;
        }
    }

    public void ChangeQuality()
    {
        switch(dropdown.value)
        {
            case 0:
                QualitySettings.SetQualityLevel(0);
                break;

            case 1:
                QualitySettings.SetQualityLevel(1);
                break;

            case 2:
                QualitySettings.SetQualityLevel(2);
                break;

            case 3:
                QualitySettings.SetQualityLevel(3);
                break;

            case 4:
                QualitySettings.SetQualityLevel(4);
                break;

            case 5:
                QualitySettings.SetQualityLevel(5);
                break;
        }
        Debug.Log(dropdown.value);
    }

}
