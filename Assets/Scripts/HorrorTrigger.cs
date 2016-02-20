using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityStandardAssets.Characters.FirstPerson;

public class HorrorTrigger : MonoBehaviour {

	/* 
	 * 
	 * Summary: The different types of Horror Events that are available. 
	 * 
	 * Audio - The player will react to a noise with additional properties being made available
	 * Vision - The player will react to a vision. Anything from a 'shadow' to a moving object
	 * 
	 * */
	public enum horrorTypes {
		Audio, 
		Vision
	}


	/*
	 * 
	 * Summary: The different reactions types are as follows:
	 * 
	 * Rotate Towards: The player will rotate to a specific Game Object
	 * 
	 * */
	public enum reactTypes {
		RotateTowards
	}

		
	public horrorTypes m_horrorType;				// Make the Horror Type list show in inspector

	public AudioClip m_audioClip;					// The required audio clip, if horror type Audio	
	public AudioClip m_doorCreakAudio;
	public AudioMixerGroup m_doorAudioMixerGroup;
	public bool m_requireAudioLoop; 				// Used if we want the audio to play more than once. 

	public int m_repeatAmount;						// How many times should the audio repeat?

	public GameObject m_horrorObject;

	public reactTypes reactionTypes;				// Reaction types that the player can perform

	public bool reactionPlayerActive;				// Is the player currently reacting to a horror event. E.g rotating towards a noise

	private int tmp;								// Create a temporary variable to complate the repeat amount with

	public bool m_shouldInitDoorRot;				// Should a door rotation be involved?
	public Vector3 m_initialDoorTransform;			// The initial door transform rotation. 
	public float m_maxRotation;						// The max rotation of the door
	private bool beginDoorRotation;

	GameObject player;								// Store a the Player Game Object in this variable
	FirstPersonController firstPersonController;	// Store the first person controller in this variable
	 

	Quaternion tmpa; 
	AudioSource audioSource;

	public string[] addtionalText;					// Remove

	/*
	 * 
	 * Summary: Awake Function
	 * 
	 * */
	void Awake()
	{
		
		m_initialDoorTransform = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
		// Create a reference to the Player Game object
		player = GameObject.FindGameObjectWithTag("Player");
		// Get the FirstPersonController Component
		firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
		/*
		 * 
		 * Summary: This prevents the game from loading in the editor. This is to stop and tell the developer that they 
		 * are missing an audio clip on one of the Horror Event Trigger objects
		 * 
		 * Parameters: audioClip - The audio clip that is meant to be supplied.
		 * 
		 * */
		if(m_audioClip == null)
		{
			// If the clip is missing, tell the developer what game object it is missing from
			Debug.LogError("Missing Audio Clip! Please add an Audio Clip to Gameobject " + this.gameObject.name);
			// Pause the editor
			Debug.Break();
		}

		audioSource = GetComponent<AudioSource>();
	}


	/* 
	 * 
	 * Summary: Start Function
	 * 
	 * */
	void Start () {
		// Check for any errors relating to the Horror Triggers
		if(m_horrorType == horrorTypes.Audio)
		{
			if(this.gameObject.GetComponent<AudioSource>() == null)
			{
				Debug.LogError("You are missing an Audio Source on Game Object: " + this.gameObject.name);
				Debug.Break();
			}
		}

		firstPersonController.m_MouseLook.Init(player.transform, firstPersonController.m_Camera.transform);
	}


	/*
	 * 
	 * Summary: Update Function.
	 * 
	 * */
	void Update () {
		// If player reaction is active..
		if(reactionPlayerActive)
		{
			// Rotate the player smoothly to face the object we set as the 'objectToRotateTowards'
			//player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(objectToRotateTowards.transform.position -  player.transform.position), Time.deltaTime * 7f);

		} 

		if(transform.localEulerAngles.y < m_maxRotation && beginDoorRotation)
		{
			transform.localPosition = m_initialDoorTransform;
			GetComponent<Rigidbody>().AddRelativeTorque(transform.up * -2, ForceMode.Impulse);
		} 
		if(transform.localEulerAngles.y > m_maxRotation) {
			beginDoorRotation = false;
			audioSource.Stop();
		}

	}


	/*
	 * 
	 * Summary: OnTriggerEnter is called to determine if the player has entered a Box Collider that is setup within the scene. 
	 * This handles some of, if not the majority of the 'Jump Scare' events. Once the player has entered the OnTriggerEnter()
	 * it is imperitive that is destroyed afterwards to prevent the same event happening twice.
	 * 
	 * Parameters: Collider other - The object that the horror trigger collides with to perform a certain action
	 * 
	 * */
	public void CallHorrorEvent()
	{
			// Switch between different Horror Types
			switch (m_horrorType)
			{
				case horrorTypes.Audio:
					// If this horror event requires a loop, Invoke the method.
					if(m_requireAudioLoop) {
						// Call an invoke repeating, and only repeat when the clip has finished playing
						InvokeRepeating("RequiredLoop", 0, m_audioClip.length);
						Debug.Log("Message from HorrorTrigger on Gameobject " + this.gameObject.name + ": Repeating audio called");
						
					} else {
						// However, if we do not need a looped audio clip, simply play the audio once
						audioSource.PlayOneShot(m_audioClip);
						// Call the method to destroy the audio clip after it has finished playing
						StartCoroutine(DestroyAudioClip(m_audioClip.length));
					}
					if(m_shouldInitDoorRot)
					{
						StartCoroutine(PlayerReaction());
					}
				break;
			}
	}


	/*
	 * 
	 * Summary: DestroyAudioClip() of type IEnumerator is used to destroy the audio clip after a certain amount of seconds had passed.
	 * In this case, we do not want to destroy the audio clip until it has finished playing. This method is only called if the 'requiredAudioLoop'
	 * is set to false.
	 * 
	 * Parameters: float seconds - The seconds passed before the audio clip is destroyed.
	 * 
	 * */
	IEnumerator DestroyAudioClip(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}


	/*
	 * 
	 * Summary: RequiredAudioLoop() method is called when the boolean 'requiredAudioLoop' is set to true. 
	 * This determines the required repeat amount of the audio before it stops playing. After the required amount of repeats
	 * has been played, we cancel the InvokeRepeating.
	 * 
	 * Paramaters: None
	 * 
	 * */
	void RequiredLoop()
	{
		if(tmp < m_repeatAmount)
		{
			audioSource.PlayOneShot(m_audioClip);
			tmp++;
		}
		else {
			CancelInvoke("RequiredLoop");
			StartCoroutine(CreekOpenDoor());
		}

	}


	/*
	 * 
	 * Summary: PlayerReaction() is called only when the boolean 'shouldPlayerReact' is set to true.
	 * otherwise, the function will not be called. IEnumerator method was chosen to turn on/off scripts after a certain amount 
	 * of time has passed. 
	 * 
	 * Parameters: None
	 * 
	 * */
	IEnumerator PlayerReaction()
	{
		firstPersonController.enabled = false;
		yield return new WaitForSeconds(10);
		firstPersonController.enabled = true;
		StartCoroutine(RadioScript.RadioPlay());
	}

	IEnumerator CreekOpenDoor()
	{
		if(m_shouldInitDoorRot)
		{
			beginDoorRotation = true;
			audioSource.outputAudioMixerGroup = m_doorAudioMixerGroup;
			audioSource.PlayOneShot(m_doorCreakAudio);

			yield return true;
		} else {
			yield return false;
		}
	}
}
