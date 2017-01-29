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

    public Button PlayButton;
    public Button OptionsButton;
    public Button ExitButton;

    // Use this for initialization
    void Start () {

    }

    public void ChangeButtonOnHover(Button btn)
    {
        btn.GetComponent<Graphic>().CrossFadeColor(new Color(255, 0, 255, 255), 0.9f, false, false);
    }

    public void ChangeButtonColourNormal(Button btn)
    {
        btn.GetComponent<Graphic>().CrossFadeColor(new Color(255, 255, 255, 255), 0.9f, false, false);
    }

    /// <summary>
    /// Switch game scene based on button pressed.
    /// </summary>
    /// <param name="buttonName"></param>
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

}
