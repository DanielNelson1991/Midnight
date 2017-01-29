using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LoadingSceneManager;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TalkbackHelper;

public class CharacterScript : MonoBehaviour {
		
	bool taskListOpen;											// To determine if the class list is open
	bool lookingAtObject;										// Is the player looking at an interactive object? Used for GUI

	#pragma warning disable 0414
	AudioSource audioSource;
    ControlsAssist controlsassist;

    public GUISkin myskin;

    [HideInInspector]
    public List<GameObject> inventoryItems = new List<GameObject>();

    /*
	 * 
	 * Summary: Awake Function
	 * 
	 * */
    void Awake()
	{
		audioSource = GetComponentInChildren<AudioSource>();
    }


	/*
	 * 
	 * Summary: StartFunction
	 * 
	 * */
	void Start () {


        controlsassist = new ControlsAssist();

        StartCoroutine(controlsassist.GameCharacterScript(0));

        Cursor.lockState = CursorLockMode.Locked;
    }

	
	/*
	 * 
	 * Summary: Update Function
	 * 
	 * */
	void Update () {

		// Debug Purpose
		if(Input.GetKeyDown(KeyCode.O))
		{

		}

        if (Input.GetKeyDown(KeyCode.B))
		{
			Debug.Break();
		}

        if (Input.GetKeyDown(KeyCode.R))
        {
            controlsassist.ObjectiveReminder();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            controlsassist.CheckInventoryItems();
        }


    }
}
