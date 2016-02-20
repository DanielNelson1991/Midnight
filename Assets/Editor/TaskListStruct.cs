using UnityEngine;
using System.Collections;
using UnityEditor;

public class TaskListStruct {
    [MenuItem("Assets/Create/Task List")]
	public static TaskSystemList	Create()
    {
        TaskSystemList asset = ScriptableObject.CreateInstance<TaskSystemList>();

		AssetDatabase.CreateAsset(asset, "Assets/TaskSystemList.asset");
		AssetDatabase.SaveAssets();
        return asset;
    }
}
