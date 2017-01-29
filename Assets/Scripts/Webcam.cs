using UnityEngine;
using System.Collections;

public class Webcam : MonoBehaviour {

    private int enableWebcam;

	// Use this for initialization
	void Start () {
	    if(PlayerPrefs.HasKey("EnableWebcam"))
        {
            enableWebcam = PlayerPrefs.GetInt("EnableWebcam");
        }
	}
}
