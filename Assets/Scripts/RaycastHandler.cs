using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class RaycastHandler : MonoBehaviour {
					
	Camera cam;								// Make a reference to the camera object
	CharacterScript player;					// Make a reference to the player

	public GUISkin skin;					// Make a public GUISkin

	public Texture2D normalCusor;			// Variable for the Normal Cursor (DOT)
	public Texture2D doorsCursor;			// Variable for the Door Cursor (HAND)
	public Texture2D interactiveCursor;		// Variable for the Interactive Object (QUESTION MARK)

	public Text descriptiveTextCanvas;

	public CursorMode cursorMode;			// Make a reference the current cursor mode

	bool lookingAtPickupObject;					// Is the player looking at an interactive object? Used for GUI
	bool lookingAtInteractiveObject;
	string objectName;						// What is the object name?
	string objectDesc;
	[Tooltip("What layers should the raycast ignore?")]
	public LayerMask raycastLayerMask;		// What layers should the raycast ignore?

	private Vector2 hotSpot = Vector2.zero;	// Define the hotspot of the cursor. 

	Inventory inventory;					// Make a reference to Inventory
	HorrorTrigger horror;					// Make a reference to the horror trigger script


	/*
	 * 
	 * Summary: Awake Function
	 * 
	 * */
    void Awake()
    {
        // Grab the Camera Component 
        cam = GetComponent<Camera>();

        // Grab the Character Script component from the Parent Game Object
        player = GetComponentInParent<CharacterScript>();
    }

	
	/*
	 * 
	 * Summary: StartFunction() 
	 * 
	 * Parameters: None
	 * 
	 * */
	void Start () {


		// Define the X and Y co-ordinates of the cursor HotSpot
		hotSpot.x = 0f;
		hotSpot.y = 0f;

		// Setup the normal cursor at the beginning of the game
		Cursor.SetCursor(normalCusor, hotSpot, CursorMode.ForceSoftware);
	}
	
	
	/*
	 * 
	 * Summary: Update Function()
	 * 
	 * Parameters: None
	 * 
	 * */
	void Update () {

		// Lock the cursor in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;

		// Keep the cursor visible
		Cursor.visible = true;

		// Create a reference to the raycast hit class
		RaycastHit hit;

		// Define a Vector3 transform position for the direction of raycast
		Vector3 fwd = transform.TransformDirection(Vector3.forward);

		// (Testing purposes) Show the raycast within scene view
		Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 3, Color.green);

        // Tman code
		lookingAtPickupObject = false;
		descriptiveTextCanvas.enabled = false;
        objectName = "";
        Cursor.SetCursor(normalCusor, hotSpot, cursorMode);

		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3, raycastLayerMask))
		{
			Debug.Log(hit.transform.gameObject.name);

			// Switch case to determine the object we are currently looking at
			switch(hit.transform.gameObject.tag)
			{
				case "PickupObject":
					lookingAtPickupObject = true;
					objectName = hit.transform.gameObject.name;
					
					if(hit.transform.gameObject.tag == "PickupObject" && objectName == "Battery")
					{
						BatteryLevel._batteryLevel += 50;
					}
				Cursor.SetCursor(interactiveCursor, hotSpot, cursorMode);
				break;


				case "InteractiveObject":
					descriptiveTextCanvas.enabled = true;
					descriptiveTextCanvas.text = DescriptiveObject.ReturnObjectDescription();
				break;

				case "Door":
					Cursor.SetCursor(doorsCursor, hotSpot, cursorMode);
				break;

				case "HorrorDoor":
					hit.transform.gameObject.GetComponent<HorrorTrigger>().CallHorrorEvent();
					hit.transform.gameObject.tag = "Untagged";
				break;

				default:
					// Do nothing (May be used later)
				break;
			}

			//Determine what happens if the player clicks
			if(Input.GetMouseButtonDown(0))
			{
				if(hit.transform.gameObject.tag == "PickupObject")
				{
					player.InteractWithObject(hit.transform.gameObject.name);
					Destroy(hit.transform.gameObject);
                }

				if(hit.transform.gameObject.tag == "AnimatedDraw")
				{
					Animator anim;
					anim = hit.transform.gameObject.GetComponentInParent<Animator>();
					anim.enabled = true;
					if(anim.GetBool("DrawOpen") == true)
					{
						anim.SetBool("DrawOpen", false);
					} else if(anim.GetBool("DrawOpen") == false)
					{
						anim.SetBool("DrawOpen", true);
					}
					
				}

				if(hit.transform.gameObject.tag == "Door")
				{
					player.InteractWithDoor(hit.transform.gameObject.GetComponent<DoorScript>());
				}

				if(hit.transform.gameObject.tag == "AnimatedObject")
				{
					player.InteractWithAnimatedObject(hit.transform.gameObject.name, hit.transform.gameObject.GetComponent<Animator>());
				}

				if(hit.transform.gameObject.tag == "Light")
				{
					Debug.Log("Looking at Light Switch");
					hit.transform.gameObject.GetComponent<InteractableObject>().lightSource.enabled = true;
				}
			}
		}
	}


	/*
	 * 
	 * Summary: OnGUI
	 * 
	 * */
	void OnGUI()
	{
		GUI.skin = skin;
			if(lookingAtPickupObject)
			{
				GUI.Label(new Rect(Screen.width / 2 - 95, Screen.height / 2 + 42.5f, 0, 0), objectName, GUI.skin.GetStyle("lookAt"));
			}
			if(lookingAtInteractiveObject)
			{
			//GUI.Label(new Rect(Screen.width / 2 - DescriptiveObject.ReturnObjectDescription().Length / 2, Screen.height + 100, 400, 400), DescriptiveObject.ReturnObjectDescription());
			}
	}

}
