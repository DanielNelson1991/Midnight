/*
    Disabler Script v.0.1
    ---------------------

    Description
    -----------
    This script is used to disable in-game features that are not yet ready
    for the game release. 

    Bugs
    ----
    - None as of yet

    Changelog
    ---------
    v0.01   - Removed previous code
            - Added basic message to display to end user

*/
using UnityEngine;
using UnityEngine.UI;

public class Disabler : MonoBehaviour {

    public bool     _Disabled;
    public Image    _UI;
    public Text     _DisabledMessage;
    
    void Update()
    {
        if(_Disabled)
        {
            DisabledUI();
        }
    }

    /// <summary>
    /// The function to tell the user this feature is disabled
    /// </summary>
    public void DisabledUI()
    {
        _DisabledMessage.text = "Sorry, but this feature is not yet ready. Check back soon for more updates!";
    }

    public void ExitDisabler()
    {
        _UI.gameObject.SetActive(false);
    }
}
