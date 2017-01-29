/* * * Custom Test Editor v0.8 * * * 

- Version comments updated. Rversion history continuing from v0.8
- Added comments to the script for easier clarification 

*/
using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.Audio;

[CustomEditor(typeof(HorrorTrigger))]
public class TestEditor : Editor
{

    // Foldout Boolean 
	public bool b_moreTriggers;

	public bool b_requireOpeningDoor;

    public bool b_activated;

    // Force override in the inspector when Horror Trigger is attached
	public override void OnInspectorGUI ()
	{
        EditorGUILayout.Space();
        // Create a new style. Used mainly for headings
        GUIStyle style = new GUIStyle();
        style.font = EditorStyles.boldFont;
        style.fontSize = 12;

        // Create a reference to HorrorScript.cs
        HorrorTrigger myTarget = (HorrorTrigger)target;

        // Begin Horizontal
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Type of horror event: ");
        // Display the required Audio Clip Object Field
        myTarget.m_horrorType = (HorrorTrigger.horrorTypes)EditorGUILayout.EnumPopup(myTarget.m_horrorType, GUILayout.MaxWidth(50), GUILayout.MinWidth(125));
        // End Horizontal
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        // If the Horror Type is Audio, perform the following...
        if (myTarget.m_horrorType == HorrorTrigger.horrorTypes.Audio)
		{
			
			// Begin Horizontal
			EditorGUILayout.BeginHorizontal();
				// Display Audio Prefix Label
				EditorGUILayout.PrefixLabel("Audio Clip:");
				// Display the required Audio Clip Object Field
				myTarget.m_audioClip = (AudioClip)EditorGUILayout.ObjectField(myTarget.m_audioClip, typeof(AudioClip), false);
			// End Horizontal
			EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);

			// Begin Horizontal
			EditorGUILayout.BeginHorizontal();
				// Display Boolean Prefix Label
				EditorGUILayout.PrefixLabel("Should Audio Repeat?");
				// Display Boolean 
			myTarget.m_requireAudioLoop = EditorGUILayout.Toggle(myTarget.m_requireAudioLoop);
			// End Horizontal
			EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Delay Amount: ");
            myTarget.m_playMusicDelay = EditorGUILayout.FloatField(myTarget.m_playMusicDelay, GUILayout.MaxWidth(125), GUILayout.MinWidth(125));
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);

            // Conditional Statement. If we require a looped audio, display the following...
            if (myTarget.m_requireAudioLoop)
			{
				// Begin Horizontal
				EditorGUILayout.BeginHorizontal();
					// Display Repeat Amount Prefix Label
					EditorGUILayout.PrefixLabel("Repeat Amount:");
					// Display Repeat Amount Field
				myTarget.m_repeatAmount = EditorGUILayout.IntField(myTarget.m_repeatAmount, GUILayout.MaxWidth(125), GUILayout.MinWidth(125));
				// End Horizontal
				EditorGUILayout.EndHorizontal();

				// Conditional Statement. This if statement prevents the editor from entering an inapropiate amount of repeats
				if(myTarget.m_repeatAmount <= 0 || myTarget.m_repeatAmount > 5)
				{
					// Revert value back to 1
					myTarget.m_repeatAmount = 1;
				}

				// Conditional Statement. If audio is null, show error.
				if(myTarget.m_audioClip == null)
				{
					// If no audio clip is supplied, show log error.
					EditorGUILayout.HelpBox("You have not provided an Audio Clip yet. Please ensure that you add one", MessageType.Error);
				}
			}

        }


        if (myTarget.m_horrorType == HorrorTrigger.horrorTypes.DoorOpen)
        {
            

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Vector3Field("Door Position: ", myTarget.m_initialDoorTransform);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Rotation Amount: ");
            myTarget.m_maxRotation = EditorGUILayout.FloatField(myTarget.m_maxRotation, GUILayout.MaxWidth(125), GUILayout.MinWidth(125));
            EditorGUILayout.EndHorizontal();

            // Begin Horizontal
            EditorGUILayout.BeginHorizontal();
            // Display Audio Prefix Label
            EditorGUILayout.PrefixLabel("Door Audio Clip:");
            // Display the required Audio Clip Object Field
            myTarget.m_doorCreakAudio = (AudioClip)EditorGUILayout.ObjectField(myTarget.m_doorCreakAudio, typeof(AudioClip), false, GUILayout.MaxWidth(125), GUILayout.MinWidth(125));
            // End Horizontal
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Delay Amount: ");
            myTarget.m_doorOpenDelay = EditorGUILayout.FloatField(myTarget.m_doorOpenDelay, GUILayout.MaxWidth(125), GUILayout.MinWidth(125));
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);


        }

		// If horror type is Push Object
		if(myTarget.m_horrorType == HorrorTrigger.horrorTypes.PushObject)
		{
			myTarget.pushedObjects = EditorGUILayout.ObjectField(myTarget.pushedObjects, typeof(GameObject), true) as GameObject;
		}


    }
}

