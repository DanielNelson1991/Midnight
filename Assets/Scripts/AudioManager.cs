using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour {

	public AudioMixerGroup audioMixerGroup;
	public AudioClip distantThunder;
	public Light moonLight;

	/*
	 * 
	 * Summary: Start Function
	 * 
	 * */
	void Start () {
		// Invoke a repeating background audio loop
		InvokeRepeating("PlayBackgroundAudio", 0, 25);
	}


	/*
	 * 
	 * Summary: PlayBackgroundAudio() is called to play the background noises. In this case, the thunder.
	 * 
	 * Parameters: None
	 * 
	 * */
	void PlayBackgroundAudio()
	{
        Debug.Log("Message from " + this.GetType().Name + " Lightning Function called");

		// 30% change of playing thunder
		if(Random.value <= 0.3)
		{
			//moonLight.enabled = true;
			GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerGroup;
			GetComponent<AudioSource>().PlayOneShot(distantThunder);
		}  

	}
}
