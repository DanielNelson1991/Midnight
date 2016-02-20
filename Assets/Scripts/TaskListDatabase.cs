using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TaskListDatabase {
	// Enum types for determining task
	public enum TaskToComplete{
		PickUpObject,
		FindARoom,
		UnlockADoor,
		TurnOnLight, 
		Keypress, 
		InteractWithLockedDoor
	}

	// Main Task Variables
    public string taskName = "New Item";
	public string taskDesc = "";
	public TaskToComplete taskToComplete;
	public bool taskCompleted = false;
	public bool initateOtherTask = false;
	public int iniateOtherTaskNo = 0;
	public bool isTaskEnabled = false;

	// Variables for Picking Up Gameobject
	public GameObject taskCompletedObject;
	public bool pickedUpObject = false;

	// Variables for Unlocking a door
	public GameObject doorObject;
}
