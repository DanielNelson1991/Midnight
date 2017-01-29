using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TalkbackHelper;
public class RaycastHandler : MonoBehaviour {

	#pragma warning disable 0414
	Camera cam;								// Make a reference to the camera object
	CharacterScript player;					// Make a reference to the player

	public GUISkin skin;					// Make a public GUISkin

	public Texture2D normalCusor;			// Variable for the Normal Cursor (DOT)
	public Texture2D doorsCursor;			// Variable for the Door Cursor (HAND)
	public Texture2D interactiveCursor;		// Variable for the Interactive Object (QUESTION MARK)
    public Texture2D lookingAtObject; 

	public Text descriptiveTextCanvas;

	public CursorMode cursorMode;			// Make a reference the current cursor mode

	#pragma warning disable 0414
	bool lookingAtPickupObject;					// Is the player looking at an interactive object? Used for GUI
	bool lookingAtInteractiveObject;
	string objectName;						// What is the object name?
	string objectDesc;
	[Tooltip("What layers should the raycast ignore?")]
	public LayerMask raycastLayerMask;		// What layers should the raycast ignore?

	HorrorTrigger horror;					// Make a reference to the horror trigger script

	ControlsAssist conntrolsAssist;
    TaskManager taskObjective;
    CharacterScript character;

    bool isPickupMove = false;

    bool b_oneTimeCursorCall = false;

    public string[] something;

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

		// Setup the normal cursor at the beginning of the game
		ChangeCursorMode("Normal", new Vector2(0, 0));
        //Cursor.lockState = CursorLockMode.Locked;


    }
	
	
	/*
	 * 
	 * Summary: Update Function()
	 * 
	 * Parameters: None
	 * 
	 * */
	void Update () {

        Cursor.visible = true;

        // Create a reference to the raycast hit class
        RaycastHit hit;

        lookingAtPickupObject = false;

		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3, raycastLayerMask))
		{
			// Switch case to determine the object we are currently looking at
			switch(hit.transform.gameObject.tag)
			{
				case "PickupObject":
					lookingAtPickupObject = true;
					//objectName = hit.transform.gameObject.name;
                    if (!b_oneTimeCursorCall)
                    {
                        //ChangeCursorMode("QuestionMark", new Vector2(normalCusor.width / 2, normalCusor.height / 2));
                        b_oneTimeCursorCall = true;
                    }
				break;

				case "InteractiveObject":
                    descriptiveTextCanvas.enabled = true;
                    if (hit.transform.gameObject.GetComponent<DescriptiveObject>() == null)
				    {
					    descriptiveTextCanvas.text = "";
				    } else
                    {
                        descriptiveTextCanvas.text = DescriptiveObject.ReturnObjectDescription();
                    }					
				break;

				case "Door":
                    if(!b_oneTimeCursorCall)
                    {
                        //ChangeCursorMode("Hand", new Vector2(normalCusor.width / 2, normalCusor.height / 2));
                        b_oneTimeCursorCall = true;
                    }   
                break;

				case "HorrorDoor":
					hit.transform.gameObject.tag = "Untagged";
				break;

                case "Untagged":
                    if (!b_oneTimeCursorCall)
                    {
                        //ChangeCursorMode("Normal", new Vector2(normalCusor.width / 2, normalCusor.height / 2));
                        b_oneTimeCursorCall = true;
                    }
                break;

				default:
                    
                break;
			}



            if(hit.transform.gameObject.GetComponent<HorrorTrigger>() == true)
            {

                HorrorTrigger[] ht = hit.transform.gameObject.GetComponents<HorrorTrigger>();

                for(int i = 0; i < ht.Length; i++)
                {
                    if (ht[i].b_activated == false)
                    {

                        if (ht[i].m_horrorType == HorrorTrigger.horrorTypes.DoorOpen)
                        {
                            StartCoroutine(ht[i].CreekOpenDoor());
                            ht[i].b_activated = true;
                        }

                        if (ht[i].m_horrorType == HorrorTrigger.horrorTypes.Audio)
                        {
                            StartCoroutine(ht[i].CallAudioHorrorEvent());
                            ht[i].b_activated = true;
                        }

						if(ht[i].m_horrorType == HorrorTrigger.horrorTypes.PushObject)
						{
							ht[i].PushObject();
						}

                    }

                    ht[i].b_activated = true;
                    Debug.Log(ht[i].gameObject.name + " : " + ht[i].b_activated);
                }
            }

			//Determine what happens if the player clicks
			if(Input.GetMouseButtonDown(0))
			{

                // Animation cases
                if(hit.transform.gameObject.tag == "AnimatedDraw")
                {
                    hit.transform.gameObject.GetComponentInParent<Animator>().enabled = true;
                    switch (hit.transform.parent.tag)
                    {
                        case "AnimatedObject":
                            InteractWithAnimatedObject("DrawOpen", hit.transform.gameObject.GetComponentInParent<Animator>());
                            break;
                    }
                }

                // Pickup object cases
                if (hit.transform.gameObject.tag == "PickupObject")
				{
					Debug.Log("Debug Message: PickupObject Tag from " + this.GetType().Name + " was called.");

                    conntrolsAssist = new ControlsAssist();
                    
					taskObjective = GetComponentInParent<TaskManager>();
                    
					conntrolsAssist.ItemLookAtMessage(hit.transform.gameObject.name);
                                        
					hit.transform.gameObject.SetActive(false);

                    Inventory inv = new Inventory();
                    inv.AddItem(hit.transform.gameObject, hit.transform.gameObject.name);

                }

                if(hit.transform.gameObject.tag == "TaskObjective")
                {
                    hit.transform.gameObject.GetComponent<TaskCompleter>().CheckInventory();
                }
			}
        }
	}

	/// <summary>
	/// Changes the cursor mode.
	/// </summary>
	/// <param name="name">Name.</param>
	void ChangeCursorMode(string name, Vector2 hotSpot)
	{
        Debug.Log("Message: Change cursor mode active. - FUNCTION CALL");
		switch (name)
		{
			case "Normal":
				Cursor.SetCursor(normalCusor, hotSpot, CursorMode.ForceSoftware);
			break;

			case "Hand":
				Cursor.SetCursor(doorsCursor, hotSpot, CursorMode.ForceSoftware);
			break;

			case "QuestionMark":
				Cursor.SetCursor(interactiveCursor, hotSpot, CursorMode.ForceSoftware);
			break;

			default:
				Cursor.SetCursor(normalCusor, hotSpot, CursorMode.ForceSoftware);
			break;
		}
        b_oneTimeCursorCall = false;
	}

   
    /// <summary>
    /// Woohoo! Shorter code! Instead of using an if after if for each object. In this function, 
    /// we will react to each object that has a tag of "AnimatedObject" and we also grab the current
    /// animator attatched to the object we are looking at. We set up a temporary bool that handles the on and off state. 
    /// </summary>
    /// <param name="objectBoolsName"></param>
    /// <returns>The object bool name</returns>
    /// <param name="objectAnimator"></param>
    void InteractWithAnimatedObject(string objectBoolsName, Animator objectAnimator)
    {
        bool objectBoolValue;

            objectBoolValue = objectAnimator.GetBool(objectBoolsName);
            if (objectAnimator == null)
            {
                Debug.Log("Error: " + objectAnimator.transform.gameObject.name + " is missing an animator component");
            }
            else
            {

                objectBoolValue = !objectBoolValue;
                objectAnimator.SetBool(objectBoolsName, objectBoolValue);
            }
    }
}