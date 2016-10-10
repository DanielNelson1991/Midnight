using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
public class HallwayHorrorEvent : MonoBehaviour {

	public Light[] lights;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			StartCoroutine(BeginEvent());
			other.gameObject.GetComponent<FirstPersonController>().enabled = false;
			other.gameObject.GetComponent<CharacterScript>().torchOn = false;
			other.gameObject.GetComponent<CharacterScript>().torchSourceOne.enabled = false;
			other.gameObject.GetComponent<CharacterScript>().torchSourceTwo.enabled = false;
		}
	}

	IEnumerator BeginEvent()
	{
		foreach(Light a in lights)
		{
			a.enabled = false;
			a.GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(1);
		}

		yield return new WaitForSeconds(2);
		GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;

	}
}
