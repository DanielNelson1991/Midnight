using UnityEngine;
using System.Collections;

public class RadioScript : MonoBehaviour {

	public static AudioSource audioSourceOne;
	public static AudioSource audioSourceTwo;
	public static bool playAudioSources;

	// Use this for initialization
	void Start () {
		AudioSource[] allAudioSource = GetComponents<AudioSource>();
		audioSourceOne = allAudioSource[0];
		//audioSourceTwo = allAudioSource[1];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static IEnumerator RadioPlay()
	{
		audioSourceOne.Play();

		yield return new WaitForSeconds(1);

		audioSourceTwo.Play();
	}
}
