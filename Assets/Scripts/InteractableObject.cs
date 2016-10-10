using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour {

	// Create an enum to determine type of object
	public enum TypeOfObject {
		LightSwitch,
        DragablePuzzlePiece,
        Furniture
	}

	// Display that selection in the inspector
	public TypeOfObject typeOfObject;

	// Properties for light switch
	public Light lightSource; // Light Source Object
	public bool onByDefault;  // Is the light on by default?

	private bool b_lookingAtSwitch; // Is the player looking at the switch?
    private bool b_lookingAtPuzzlePiece;
    private bool b_lookingAtFurniture;

	// Use this for initialization
	void Start () {

		if(onByDefault)
		{
			lightSource.enabled = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
