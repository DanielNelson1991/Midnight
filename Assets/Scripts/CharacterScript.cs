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

	public bool interactingDoor;

    // Audio Clips
    public AudioClip audioClip;

    // Animators
    public Animator objectAnimator;

	public bool torchOn;

	public Light torchSourceOne;
	public Light torchSourceTwo;
	public float batteryLevel;

    public GameObject phone;

	//TaskSystemList taskListData;

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

        //taskListData = AssetDatabase.LoadAssetAtPath(EditorPrefs.GetString("ObjectPath"), typeof(TaskSystemList)) as TaskSystemList;

        //audioSource.Play();
    }

	
	/*
	 * 
	 * Summary: Update Function
	 * 
	 * */
	void Update () {

		if(Input.GetKeyDown(KeyCode.F))
		{
            Animator phoneAnim = phone.GetComponent<Animator>();

            if(phoneAnim.GetBool("Turned") == false)
            {
                phoneAnim.SetBool("Turned", true);
            } else
            {
                phoneAnim.SetBool("Turned", false);
            }
		}

		// Debug Purpose
		if(Input.GetKeyDown(KeyCode.O))
		{
			BatteryLevel._batteryLevel = BatteryLevel._batteryLevel + (100 - BatteryLevel._batteryLevel);
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

	///	<summary>
	///	InteractWithObject is called from the RaycastHandler script. 
	///	RaycastHandler determines if the object we are looking at is an interactive object. 
	///	If it is, pass the gameobject name to this function. If this object name exists within the switch
	///	statement, add the item to the inventory.
	///	</summary>
	///	<param name="itemName" param desc = "hello">Item name.</param>
	public void InteractWithObject(string itemName)
	{  
			switch(itemName)
			{
				case "Main Door Key":
					//inventory.AddItem(1);
				break;

                case "Battery":
                    //inventory.AddItem(2);
                break;

	        }
			
		//CheckTaskSystem();
    }

	/// <summary>
	/// Animate a door Gameobject by passing in the <code>DoorScript</code> component
	/// </summary>
	/// <param name="component">Component.</param>
	public void InteractWithDoor(DoorScript component)
	{
		// Make reference to the door script
		DoorScript door;
		// Give the DoorScript the component from the Raycast
		door = component;
		// Loop through inventory items and see if the player has the correct key for this door
		// NOTE: There we a problem with the audio clip for "Locked door" be played. Apparently, if the list is empty,
		// the for loop below is not called. Not sure why. Fixed it by adding at least one item to inventory on start
		door.CheckDoor();		
	}

	void OnGUI()
	{
		GUI.skin = myskin;
		//if(taskListOpen)
		//{
		//	GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500), myskin.GetStyle("TaskSystem"));
		//	GUILayout.BeginVertical();
		//	GUILayout.Label("My Diary");
		//	for(int i = 0; i < taskListData.taskList.Count; i++)
		//	{
		//		if(taskListData.taskList[i].isTaskEnabled)
		//		{
		//			if(!taskListData.taskList[i].taskCompleted)
		//			{
		//				GUILayout.Label(taskListData.taskList[i].taskDesc);
		//			} else {
		//				GUILayout.Label("<color=green>" + taskListData.taskList[i].taskName + "</color>" + "\n" + taskListData.taskList[i].taskDesc + " - DONE");
		//			}
		//		}
		//	}
		//	GUILayout.EndVertical();
		//	GUILayout.EndArea();
		//}
	}

	/// <summary>
	/// Check the task system to see if a task has been completed.
	/// </summary>
	//public void CheckTaskSystem()
	//{
	//	for(int i = 0; i < taskListData.taskList.Count; i++)
	//	{
	//		if(taskListData.taskList[i].taskToComplete == TaskListDatabase.TaskToComplete.PickUpObject)
	//		{
	//			// Error Prevention. If task is not null, continue...
	//			if(taskListData.taskList[i].taskCompletedObject != null)
	//			{
	//				//if(inventory.InventoryContainString(taskListData.taskList[i].taskCompletedObject.name))
	//				//{
	//				//	taskListData.taskList[i].taskCompleted = true;
	//				//	if(taskListData.taskList[i].initateOtherTask)
	//				//	{
	//				//		taskListData.taskList[taskListData.taskList[i].iniateOtherTaskNo].isTaskEnabled = true;
	//				//	}
	//				//}
	//			} else {
	//				Debug.LogError("You have not assigned a completed object in one of the 'Pickup Tasks'");
	//			}
	//		}

	//		if(taskListData.taskList[i].taskToComplete == TaskListDatabase.TaskToComplete.UnlockADoor)
	//		{
	//			if(taskListData.taskList[i].doorObject != null)
	//			{
	//				if(!taskListData.taskList[i].doorObject.GetComponent<DoorScript>().isDoorLocked)
	//				{
	//					taskListData.taskList[i].taskCompleted = true;
	//				}
	//			} else {
	//				Debug.LogError("Door Not Valid");
	//			}
	//		}
	//	}
	//}

}
