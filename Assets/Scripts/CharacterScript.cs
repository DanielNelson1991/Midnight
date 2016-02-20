using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LoadingSceneManager;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
public class CharacterScript : MonoBehaviour {
		
	bool taskListOpen;											// To determine if the class list is open
	bool lookingAtObject;										// Is the player looking at an interactive object? Used for GUI

	Inventory inventory;

	public bool interactingDoor;

    // Audio Clips
    public AudioClip audioClip;

    // Animators
    public Animator objectAnimator;

	public bool torchOn;

	public Light torchSourceOne;
	public Light torchSourceTwo;
	public float batteryLevel;

	TaskSystemList taskListData;

	AudioSource audioSource;

	public GUISkin myskin;

	/*
	 * 
	 * Summary: Awake Function
	 * 
	 * */
    void Awake()
	{
        inventory = GetComponent<Inventory>();
		audioSource = GetComponentInChildren<AudioSource>();
    }


	/*
	 * 
	 * Summary: StartFunction
	 * 
	 * */
	void Start () {

		taskListData = AssetDatabase.LoadAssetAtPath(EditorPrefs.GetString("ObjectPath"), typeof(TaskSystemList)) as TaskSystemList;

		//audioSource.Play();

		if(taskListData == null)
		{
			Debug.Break();
		}
    }

	
	/*
	 * 
	 * Summary: Update Function
	 * 
	 * */
	void Update () {

		if(Input.GetKeyDown(KeyCode.K))
		{
			BatteryLevel._torchsOn = !BatteryLevel._torchsOn;
			torchSourceOne.enabled = BatteryLevel._torchsOn;
			torchSourceTwo.enabled = BatteryLevel._torchsOn;
		}

		// Debug Purpose
		if(Input.GetKeyDown(KeyCode.R))
		{
			BatteryLevel._batteryLevel = BatteryLevel._batteryLevel + (100 - BatteryLevel._batteryLevel);
		}


		if(Input.GetKeyDown(KeyCode.B))
		{
			Debug.Break();
		}

		if(Input.GetKeyDown(KeyCode.T))
		{
			taskListOpen = !taskListOpen;
		}
		if(Input.GetKeyDown(KeyCode.I))
		{
			taskListData.taskList[0].taskCompleted = true;
		}
	}

	///	<summary>
	///	InteractWithObject is called from the RaycastHandler script. 
	///	RaycastHandler determines if the object we are looking at is an interactive object. 
	///	If it is, pass the gameobject name to this function. If this object name exists within the switch
	///	statement, add the item to the inventory.
	///	</summary>
	///	<param name="itemName">Item name.</param>
	public void InteractWithObject(string itemName)
	{  
			switch(itemName)
			{
				case "Main Door Key":
					inventory.AddItem(1);
				break;

	            case "GKey":
	                inventory.AddItem(2);

	            break;

	            case "TKey":
	                inventory.AddItem(3);
	            break;
	        }
			
		CheckTaskSystem();
    }


	/// <summary>
	/// InteractWithAnimatedObject, also called from RaycastHandler, determines if the object we are 
	/// looking at is animated and interactable. If it is, RaycastHandler will pass the gameobject name
	///	and the animator component. <c>public string something</c>
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="objectAnimator">Object animator.</param>
	public void InteractWithAnimatedObject(string name, Animator objectAnimator)
	{
		// Make reference to the animator
		Animator anim;
		// Give the animator the component from Raycast
		anim = objectAnimator;
		// Safety measure to prevent errors
		if(anim == null)
		{
			Debug.LogWarning("Warning: This object does not contain an animator. Releaseing control back to game...");
		}
		// If safety measure is satisfied, proceed to the switch
		if(anim != null)
		{
			// Switch the name of the component
			switch(name)
			{
			case "fridge_door":
				if(anim.GetBool("FridgeOpen") == false)
				{
					anim.SetBool("FridgeOpen", true);
					Debug.Log("This fridge door contains an animator. Proceeding...");
				}
				else if(anim.GetBool("FridgeOpen") == true)
				{
					anim.SetBool("FridgeOpen", false);
				}
				break;
				
				default:
				
				break;
			}
		}
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
		if(taskListOpen)
		{
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500), myskin.GetStyle("TaskSystem"));
			GUILayout.BeginVertical();
			GUILayout.Label("My Diary");
			for(int i = 0; i < taskListData.taskList.Count; i++)
			{
				if(taskListData.taskList[i].isTaskEnabled)
				{
					if(!taskListData.taskList[i].taskCompleted)
					{
						GUILayout.Label(taskListData.taskList[i].taskDesc);
					} else {
						GUILayout.Label("<color=green>" + taskListData.taskList[i].taskName + "</color>" + "\n" + taskListData.taskList[i].taskDesc + " - DONE");
					}
				}
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}

	/// <summary>
	/// Check the task system to see if a task has been completed.
	/// </summary>
	public void CheckTaskSystem()
	{
		for(int i = 0; i < taskListData.taskList.Count; i++)
		{
			if(taskListData.taskList[i].taskToComplete == TaskListDatabase.TaskToComplete.PickUpObject)
			{
				// Error Prevention. If task is not null, continue...
				if(taskListData.taskList[i].taskCompletedObject != null)
				{
					if(inventory.InventoryContainString(taskListData.taskList[i].taskCompletedObject.name))
					{
						taskListData.taskList[i].taskCompleted = true;
						if(taskListData.taskList[i].initateOtherTask)
						{
							taskListData.taskList[taskListData.taskList[i].iniateOtherTaskNo].isTaskEnabled = true;
						}
					}
				} else {
					Debug.LogError("You have not assigned a completed object in one of the 'Pickup Tasks'");
				}
			}

			if(taskListData.taskList[i].taskToComplete == TaskListDatabase.TaskToComplete.UnlockADoor)
			{
				if(taskListData.taskList[i].doorObject != null)
				{
					if(!taskListData.taskList[i].doorObject.GetComponent<DoorScript>().isDoorLocked)
					{
						taskListData.taskList[i].taskCompleted = true;
					}
				} else {
					Debug.LogError("Door Not Valid");
				}
			}
		}
	}

}
