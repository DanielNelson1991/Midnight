using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour {

	public AudioMixerGroup audioMixerGroup;
	public AudioClip distantThunder;
	public AudioClip owlSound;

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
	 * Summary: Background Audio of the scene
	 * 
	 * Parameters: None
	 * 
	 * */
	void PlayBackgroundAudio()
	{
        Debug.Log("Message from " + this.GetType().Name + " Lightning Function called");

		// 30% change of playing thunder
		if(Random.value <= 3)
		{
			//moonLight.enabled = true;
			GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerGroup;
			GetComponent<AudioSource>().PlayOneShot(distantThunder);
		} else if (Random.value >= 3 && Random.value <= 6)
		{
			GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerGroup;
			GetComponent<AudioSource>().PlayOneShot(owlSound);
		}


	}
}
