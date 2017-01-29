using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class TaskManager : MonoBehaviour {
    
    [System.Serializable]
    public class Tasks
    {
        public string taskName;
        public string taskDesc;
        public bool taskCompleted;
    }

    public Sprite _background;
    
    public List<Tasks> taskList = new List<Tasks>();

    public void SetCompletedTask(int taskID)
    {
        Debug.Log("Message from " + this.GetType().Name + ": SetTaskCompleted(taskID = "+taskID+")");
        for(int i = 0; i < taskList.Count; i++)
        {
            taskList[taskID].taskCompleted = true;
            taskList.RemoveAt(taskID);

            // Hopefully prevent null ref
            if (taskList.Count <= 0)
            {
                taskList.Clear();
            }
        }
    }

}
