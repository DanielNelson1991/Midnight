using UnityEngine;
using System.Collections;

public class DescriptiveObject : MonoBehaviour {

	public static string m_objectDescription;
	public string description;
	// Use this for initialization
	void Start () {
		m_objectDescription = description;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static string ReturnObjectDescription()
	{
		return m_objectDescription;	
	}
}
