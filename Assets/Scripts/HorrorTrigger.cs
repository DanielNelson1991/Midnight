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
        DoorOpen, 
        PushObject, 
		Vision
	}


	/*
	 * 
	 * Summary: The different reactions types are as follows:
	 * 
	 * Rotate Towards: The player will rotate to a specific Game Object
	 * 
	 * */
	public enum triggerMore {
		PlayRadio
	}

		
	public horrorTypes m_horrorType;				// Make the Horror Type list show in inspector

	public AudioClip m_audioClip;					// The required audio clip, if horror type Audio	
	public AudioClip m_doorCreakAudio;
	public AudioMixerGroup m_doorAudioMixerGroup;
	public bool m_requireAudioLoop; 				// Used if we want the audio to play more than once. 

	public int m_repeatAmount;						// How many times should the audio repeat?
    public bool m_turnPlayerTorchOff;               // Turn the players torch off?

    public float m_turnTorchOffDelay;               // (Optional Delay)
    public float m_playMusicDelay;                  // (Optional Delay)
    public float m_pushObjectDelay;
    public float m_doorOpenDelay;
    public bool b_activated;


	public GameObject m_horrorObject;

	public triggerMore triggerOtherEvent;		    // Reaction types that the player can perform
    public GameObject triggerOtherEvent_Object;

	public bool reactionPlayerActive;				// Is the player currently reacting to a horror event. E.g rotating towards a noise

	private int tmp;								// Create a temporary variable to complate the repeat amount with

	public bool m_shouldInitDoorRot;				// Should a door rotation be involved?
	public Vector3 m_initialDoorTransform;			// The initial door transform rotation. 
	public float m_maxRotation;						// The max rotation of the door
	private bool beginDoorRotation;

	#pragma warning disable 0414
	GameObject player;								// Store a the Player Game Object in this variable
	FirstPersonController firstPersonController;	// Store the first person controller in this variable

	// Arrays
	public GameObject[] pushedObjects;				// Array to hold the game objects that will be pushed
	 

	Quaternion tmpa; 
	AudioSource audioSource;

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

        if (transform.localEulerAngles.y < m_maxRotation && beginDoorRotation)
        {
            transform.localPosition = m_initialDoorTransform;
            GetComponent<Rigidbody>().AddRelativeTorque(transform.up * -2, ForceMode.Impulse);
        }
        if (transform.localEulerAngles.y > m_maxRotation)
        {
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
	public IEnumerator CallAudioHorrorEvent()
	{
        yield return new WaitForSeconds(m_playMusicDelay);
        // If this horror event requires a loop, Invoke the method.
        if (m_requireAudioLoop) {
				
               // Call an invoke repeating, and only repeat when the clip has finished playing
		       InvokeRepeating("RequiredLoop", 0, m_audioClip.length);
			   Debug.Log("Message from HorrorTrigger on Gameobject " + this.gameObject.name + ": Repeating audio called");
						
			} else {

			    // However, if we do not need a looped audio clip, simply play the audio once
			    audioSource.PlayOneShot(m_audioClip);
			    // Call the method to destroy the audio clip after it has finished playing
			    StartCoroutine(DestroyAudioClip(m_audioClip.length));
		}
	}

    public IEnumerator CreekOpenDoor()
    {

        yield return new WaitForSeconds(m_doorOpenDelay);
        Debug.Log("Calling CreakDoor Open");
        beginDoorRotation = true;
        audioSource.clip = m_doorCreakAudio;
        audioSource.Play();
        Debug.Log(m_doorCreakAudio.loadState);
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
        //Destroy(this.gameObject.GetComponent<AudioClip>());
	}

	/// <summary>
	/// This function pushes an object off the shelf.
	/// </summary>
	/// <param name="seconds">Seconds.</param>
	public void PushObject(float seconds)
	{
		
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
            audioSource.clip = m_audioClip;
			audioSource.Play();
			tmp++;
		}
		else {
			CancelInvoke("RequiredLoop");
            DestroyAudioClip(.2f);
            DestroyComponent();

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
		yield return new WaitForSeconds(10);

        triggerOtherEvent_Object.GetComponent<AudioSource>().Play();
	}

    /// <summary>
    /// This function destroys the instnace of this script. This helps avoid collisions between different horror type scripts 
    /// </summary>
    void DestroyComponent()
    {
        Destroy(this);
    }


}
