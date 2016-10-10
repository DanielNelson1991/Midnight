using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

    public AudioClip doorLocked;        // Variable for audio used for the door locked sound
    public AudioClip doorUnlocked;      // Variable for audio used for the door unlocked sound
    public AudioClip doorOpening;
    public bool isDoorLocked;           // Create a public boolean variable so the developer can determine if the door is locked
    public GameObject keyForThisDoor;   // What is the Key ID for this door. ID is defined withing the Item Database

    AudioSource audioSource;			// Make a reference to an audio source component 
#pragma warning disable 0414			 
    Rigidbody rigid;                    // Make a reference to a Rigidbody component
#pragma warning restore 0414 
    HingeJoint hinge;                   // Make a reference to the Hingejoint component in order to swing the door open
    BoxCollider boxCollider;            // Make a reference to a box collider

    private bool doesHaveKey;           // Determine if the door does have a key or not

    public float HingeMaxLimit = 100;	// Make limit the door is allowed to swing

    CharacterScript character;

    void Awake()
    {
        // Grab the audio source from the door object
        audioSource = GetComponent<AudioSource>();
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
    }

    /*
	 * 
	 * Summary: Start Function()
	 * 
	 * Parameters: None
	 * 
	 * */
    void Start()
    {
        // Grab the inventory component from the player


    }


    /*
	 * 
	 * Summary: CheckDoor() is called when the player interacts with the door they are 
	 * currently interacting with. This function is called from the Raycast Handler Script 
	 * 
	 * Parameters: None
	 * 
	 * */
    public void CheckDoor()
    {
        //// If the door is locked
        if (isDoorLocked)
        {
            // If the player contains the key for this door
            if (character.inventoryItems.Contains(keyForThisDoor))
            {
                // Play door unlocked audio
                audioSource.PlayOneShot(doorUnlocked);
                // Remove the key from the inventory
                //inventory.RemoveItem(keyID);
                // Change the door locked boolean to false
                isDoorLocked = false;
                // Add the rigidbody to this door
                rigid = this.gameObject.AddComponent<Rigidbody>();
                // Add the HingeJoint component to this door
                hinge = this.gameObject.AddComponent<HingeJoint>();
                JointLimits limits = hinge.limits;
                // Define the anchor point of the hingejoint
                hinge.anchor = new Vector3(0, 2.686714f, 0);
                // Define the axis on which to rotate on
                hinge.axis = new Vector3(0, 100, 0);
                // Define the maximum rotation amount on the hingejoint
                limits.max = 100;
                hinge.limits = limits;
                hinge.useLimits = true;
            }
            else {
                audioSource.PlayOneShot(doorLocked);
            }
        }
    }
}