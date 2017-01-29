using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TaskCompleter : MonoBehaviour {
    
    public enum TaskType
    {
        Door,
        Puzzle,
        Memory
    }

    public TaskType taskType;

    public GameObject ObjectThatCompletesTask;

    /// <summary>
    /// This function is called to check whether or not the player has the item
    /// to complete this task. If not, return false.
    /// </summary>
    public void CheckInventory()
    {
        Debug.Log("Does contain item");

        for (int i = 0; i < Inventory.InventoryItems.Count; i++)
        {
            if(Inventory.InventoryItems[i].ItemName.Contains(ObjectThatCompletesTask.name))
            {
                switch (taskType)
                {
                    case TaskType.Door:
                        GetComponent<DoorScript>().UnlockDoor();
                    break;
                }
            } else
            {
                switch(taskType)
                {
                    case TaskType.Door:
                        GetComponent<DoorScript>().DoorStillLocked();
                    break;
                }
            }
            Debug.Log("Items In Inventory: " + Inventory.InventoryItems[i].ItemName);
        }
    }

    public void Test() {

    }
}
