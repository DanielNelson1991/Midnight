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
	public bool additionalProperties;
	public bool yes;

    Texture2D text;

    // Foldout Boolean 
	public bool b_moreTriggers;

	public bool b_requireOpeningDoor;

    public class boolList
    {

        string newS;
        bool newB;

    }

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

        // Enum variable to select the type of horror event
		myTarget.m_horrorType = (HorrorTrigger.horrorTypes)EditorGUILayout.EnumPopup("Type of Horror Event: ", myTarget.m_horrorType);

		// If the Horror Type is Audio, perform the following...
		if(myTarget.m_horrorType == HorrorTrigger.horrorTypes.Audio)
		{
			
			// Begin Horizontal
			EditorGUILayout.BeginHorizontal();
				// Display Audio Prefix Label
				EditorGUILayout.PrefixLabel("Audio Clip:");
				// Display the required Audio Clip Object Field
				myTarget.m_audioClip = (AudioClip)EditorGUILayout.ObjectField(myTarget.m_audioClip, typeof(AudioClip), false);
			// End Horizontal
			EditorGUILayout.EndHorizontal();





			// Begin Horizontal
			EditorGUILayout.BeginHorizontal();
				// Display Boolean Prefix Label
				EditorGUILayout.PrefixLabel("Should Audio Repeat?");
				// Display Boolean 
			myTarget.m_requireAudioLoop = EditorGUILayout.Toggle(myTarget.m_requireAudioLoop);
			// End Horizontal
			EditorGUILayout.EndHorizontal();

			// Conditional Statement. If we require a looped audio, display the following...
			if(myTarget.m_requireAudioLoop)
			{
				// Begin Horizontal
				EditorGUILayout.BeginHorizontal();
					// Display Repeat Amount Prefix Label
					EditorGUILayout.PrefixLabel("Repeat Amount:");
					// Display Repeat Amount Field
				myTarget.m_repeatAmount = EditorGUILayout.IntField(myTarget.m_repeatAmount);
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

			// Begin Horizontal
			EditorGUILayout.BeginHorizontal();
			// Display Boolean Prefix Label
			EditorGUILayout.PrefixLabel("Do you require a door to open?");
			// Display Boolean 
			myTarget.m_shouldInitDoorRot = EditorGUILayout.Toggle(myTarget.m_shouldInitDoorRot);
			// End Horizontal
			EditorGUILayout.EndHorizontal();

			if(myTarget.m_shouldInitDoorRot)
			{

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Vector3Field("Door Position:", myTarget.m_initialDoorTransform);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				myTarget.m_maxRotation = EditorGUILayout.FloatField("Rotation amount:", myTarget.m_maxRotation);
				EditorGUILayout.EndHorizontal();

				// Begin Horizontal
				EditorGUILayout.BeginHorizontal();
				// Display Audio Prefix Label
				EditorGUILayout.PrefixLabel("Door Audio Clip:");
				// Display the required Audio Clip Object Field
				myTarget.m_doorCreakAudio = (AudioClip)EditorGUILayout.ObjectField(myTarget.m_doorCreakAudio, typeof(AudioClip), false);
				// End Horizontal
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				// Display Audio Prefix Label
				EditorGUILayout.PrefixLabel("Door Audio Mixer Group:");
				// Display the required Audio Clip Object Field
				myTarget.m_doorAudioMixerGroup = (AudioMixerGroup)EditorGUILayout.ObjectField(myTarget.m_doorAudioMixerGroup, typeof(AudioMixerGroup), false);
				// End Horizontal
				EditorGUILayout.EndHorizontal();
			}

			// Find and serialize the property "Addtional Text"
			SerializedProperty tps = serializedObject.FindProperty("addtionalText");
			// If the element size has increased, begin range check to update Editor GUI
			EditorGUI.BeginChangeCheck();
			// Display the property field 
			EditorGUILayout.PropertyField(tps, true);
			// If the range check has finished
			if(EditorGUI.EndChangeCheck())
			{
				// On the property, apply the modifications
				serializedObject.ApplyModifiedProperties();
                // Deprecated. Remove at some point.
                #pragma warning disable 0618
                EditorGUIUtility.LookLikeControls();
                #pragma warning restore 0618
            }

		}
	

    }
}

