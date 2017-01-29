/*
    This script displays a message to the user if a feature has not
    yet been added to the game. 
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System;

public class Disabler : MonoBehaviour {

    public bool         _isDisabled;
    public GameObject   _disabler;
    public Text         _textMessage;

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.LogWarning("WARNING");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.LogError("ERROR ");
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("NORMAL ");
        }
    }

    public void OnObjectPressed()
    {
        _disabler.gameObject.SetActive(true);

        string temp = EventSystem.current.currentSelectedGameObject.gameObject.name;

        switch(temp)
        {
                case "FB":
                    _textMessage.text = "Currently, Facebook has not yet been registered to the game. Please stay tuned for more updates";
                break;


                case "Twitter":
                    _textMessage.text = "No twitter support yet.";
                break;
        }
    }

    public void ExitDisabler(string test)
    {
        _disabler.gameObject.SetActive(false);
    }

    public void dome()
    {

    }
}
