using UnityEngine;
using System.Collections;
using Twitter;
using UnityEngine.UI;
public class SocialMediaHandler : MonoBehaviour {

    public int _twitterWordCount = 140;           // The word count
    public Text _twitterWordCountText;      // Reference the text object
    public Text _a;
	// Use this for initialization
	void Start () {
        /*_twitterWordCountText = GameObject.Find("a").GetComponent<Text>();
        _a = GameObject.Find("s").GetComponent<Text>();*/
    }

    public void LoadTwitterUI()
    {
        // Enable it here.
        
    }

    public void d()
    {
        // Count the letters in text string
        int tmp = 140 - _twitterWordCountText.text.Length;
        _a.text = tmp.ToString();
        Debug.Log(tmp);
    }
}
