/*		Task Editor v0.1
 * 
 * 			Changes
 * 
 * 			Bugs
 * 
 * - If no task list scriptable object exisits, creating a new one works but will give a null reference 
 * 	 on the if statement on line 114 until the list is manuaully set to the size of 1. Or if this script is changed
 * 	 slightly and saved, then it will work.
 * 
 * */
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class TaskListEditorWindow : EditorWindow {
    
	public TaskSystemList taskSystemList;	// Make reference to TaskSystemList
    private int viewIndex = 1;				// A viewindex needed to show current task number 

    [MenuItem("Window/Task System Editor %#e")]
    static void Init()
    {
		EditorWindow.GetWindow(typeof(TaskListEditorWindow), false, "Task List Editor");
    }

    void OnEnable()
    {
        if(EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            taskSystemList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(TaskSystemList)) as TaskSystemList;
            Debug.Log("Task System List has been found.");
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Task System Editor", EditorStyles.boldLabel);
        if(taskSystemList != null)
        {
            if(GUILayout.Button("Show Item List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = taskSystemList;
            }
        }
        if(GUILayout.Button("Open Task List"))
        {
            OpenItemList();
        }
        if(GUILayout.Button("New Task List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = taskSystemList;
        }
        GUILayout.EndHorizontal();

        if(taskSystemList == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if(GUILayout.Button("Create New Task List", GUILayout.ExpandWidth(false)))
            {
                CreateNewTaskList();
            }
			if(GUILayout.Button("Open Existing Task List", GUILayout.ExpandWidth(false)))
            {
                OpenItemList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if(taskSystemList != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if(GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }

            GUILayout.Space(5);

            if(GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if(viewIndex < taskSystemList.taskList.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if(GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if(GUILayout.Button("Delete", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }


            GUILayout.EndHorizontal();



            if (taskSystemList.taskList.Count > 0)
            {
                //aDebug.Log(taskSystemList.taskList.Count);
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Task No.", viewIndex, GUILayout.ExpandWidth(false)), 1, taskSystemList.taskList.Count);
				EditorGUILayout.LabelField("of " + taskSystemList.taskList.Count.ToString() + " tasks", "", GUILayout.ExpandWidth(false));
                EditorGUILayout.EndHorizontal();

				taskSystemList.taskList[viewIndex - 1].isTaskEnabled = EditorGUILayout.Toggle("Task Enabled", taskSystemList.taskList[viewIndex - 1].isTaskEnabled);
				taskSystemList.taskList[viewIndex - 1].taskToComplete = (TaskListDatabase.TaskToComplete)EditorGUILayout.EnumPopup("Task To Complete:", taskSystemList.taskList[viewIndex - 1].taskToComplete);
				taskSystemList.taskList[viewIndex - 1].taskName = EditorGUILayout.TextField("Task Name", taskSystemList.taskList[viewIndex - 1].taskName as string);
				taskSystemList.taskList[viewIndex - 1].taskDesc = EditorGUILayout.TextField("Task Desc", taskSystemList.taskList[viewIndex - 1].taskDesc);

				if(taskSystemList.taskList[viewIndex - 1].taskToComplete == TaskListDatabase.TaskToComplete.UnlockADoor)
				{
					taskSystemList.taskList[viewIndex - 1].doorObject = (GameObject)EditorGUILayout.ObjectField("Completed Object", taskSystemList.taskList[viewIndex - 1].doorObject, typeof(GameObject), true, GUILayout.ExpandWidth(true)) as GameObject;
				}
				taskSystemList.taskList[viewIndex - 1].taskCompleted = EditorGUILayout.Toggle("Task Completed", taskSystemList.taskList[viewIndex - 1].taskCompleted);
				taskSystemList.taskList[viewIndex - 1].initateOtherTask = EditorGUILayout.Toggle("Initiate Other task?", taskSystemList.taskList[viewIndex - 1].initateOtherTask);
				if(taskSystemList.taskList[viewIndex - 1].initateOtherTask)
				{
					taskSystemList.taskList[viewIndex - 1].iniateOtherTaskNo = EditorGUILayout.IntField("Other Task No.", taskSystemList.taskList[viewIndex - 1].iniateOtherTaskNo);
				}

				// Conditional 
				if(taskSystemList.taskList[viewIndex - 1].taskToComplete == TaskListDatabase.TaskToComplete.Keypress)
				{
					EditorGUILayout.HelpBox("Message: For the time being, keypresses need to be hard coded into the game in order to work. Message" +
						"a developer for more information before attempting to add this yourself.", MessageType.Info);
				}
				if(taskSystemList.taskList[viewIndex - 1].taskToComplete == TaskListDatabase.TaskToComplete.PickUpObject)
				{
					taskSystemList.taskList[viewIndex - 1].taskCompletedObject = (GameObject)EditorGUILayout.ObjectField("Completed Object", taskSystemList.taskList[viewIndex - 1].taskCompletedObject, typeof(GameObject), false, GUILayout.ExpandWidth(true)) as GameObject;
					EditorGUILayout.HelpBox("Use only Prefab Objects. Doing so, will prevent null reference error", MessageType.Warning);
				}
            } else {
					GUILayout.Label("This Task List is Empty");
			}

            if (GUI.changed)
            {
                EditorUtility.SetDirty(taskSystemList);
            }  
        }
    }

    void CreateNewTaskList()
    {
        viewIndex = 1;
        taskSystemList = TaskListStruct.Create();
        if (taskSystemList)
        {
            string relPath = AssetDatabase.GetAssetPath(taskSystemList);
            EditorPrefs.SetString("ObjectPath", relPath);

        }
    }

    void OpenItemList()
    {
		string absPath = EditorUtility.OpenFilePanel("Select Task List", "", "");
		if(absPath.StartsWith(Application.dataPath))
		{
			string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
			taskSystemList = AssetDatabase.LoadAssetAtPath(relPath, typeof(TaskSystemList)) as TaskSystemList;
			if(taskSystemList)
			{
				EditorPrefs.SetString("ObjectPath", relPath);
			}
		}
    }



    void AddItem()
    {
        Debug.Log("Add Item called");
        TaskListDatabase newItem = new TaskListDatabase();
        newItem.taskName = "New Item";
        taskSystemList.taskList.Add(newItem);
        viewIndex = taskSystemList.taskList.Count;
    }

	void DeleteItem(int index)
	{
		taskSystemList.taskList.RemoveAt(index);
	}
}
