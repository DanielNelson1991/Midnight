using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class FlickeringLight : MonoBehaviour {

	public float min;
	public float max;
	public bool triggerBlowingLightbulb;
	public AudioClip bulbExplodeAudioClip;
	public AudioMixerGroup audioMixerGroup;
	private Light light = new Light();

	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		InvokeRepeating("FlickerLight", 0, 0.5f);
        light = gameObject.GetComponent<Light>();
		if(triggerBlowingLightbulb)
		{
			GetComponent<BoxCollider>().enabled = true;
			audioSource = this.gameObject.AddComponent<AudioSource>();
			audioSource.clip = bulbExplodeAudioClip;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FlickerLight()
	{
		float rand = Random.Range(min, max);
		if(rand < 5)
		{
			light.enabled = true;
		} else 
		{
			light.enabled = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			audioSource.outputAudioMixerGroup = audioMixerGroup;
			audioSource.PlayOneShot(bulbExplodeAudioClip);
			audioSource.loop = false;
			DestroyObject(bulbExplodeAudioClip.length);
		}
	}

	IEnumerator DestroyObject(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
