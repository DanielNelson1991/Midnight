using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(InteractableObject))]
public class InteractableEditor : Editor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnInspectorGUI()
	{
		// Make a reference to interactable object script
		InteractableObject myTarget = (InteractableObject)target;

		// If the type of object is a light, perform the following..
		if(myTarget.typeOfObject == InteractableObject.TypeOfObject.LightSwitch)
		{
			// Begin Horizontal
			EditorGUILayout.BeginHorizontal();
				// Prefix label for Light Source
				EditorGUILayout.PrefixLabel("Light Source:");
				// Object field for light source
				myTarget.lightSource = (Light)EditorGUILayout.ObjectField(myTarget.lightSource, typeof(Light), true);
			// End Horizontal
			EditorGUILayout.EndHorizontal();

			// Begin Horizontal
			EditorGUILayout.BeginHorizontal();
				// Prefix Label for light source
				EditorGUILayout.PrefixLabel("Light On by Default");
				// Boolean field for onByDefault
				myTarget.onByDefault = EditorGUILayout.Toggle(myTarget.onByDefault);
			// End Horizontal
			EditorGUILayout.EndHorizontal();
		}
	}
}
