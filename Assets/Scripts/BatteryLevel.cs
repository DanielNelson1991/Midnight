using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BatteryLevel : MonoBehaviour {

	public static float _batteryLevel;
	public Light _torchOne;
	public Light _torchTwo;
	public static bool _torchsOn;

	[Tooltip("Battery level at 100%")]
	public Sprite _oneHundredPercentIcon;

	[Tooltip("Battery level Icon at 75%")]
	public Sprite _seventyFivePercentIcon;

	[Tooltip("Batter level Icon at 50%")]
	public Sprite _fiftyPercentIcon;

	[Tooltip("Battery level Icon at 25%")]
	public Sprite _twentyFivePercentIcon;

	// Use this for initialization
	void Start () {
		_batteryLevel = 100;
	}
	
	// Update is called once per frame
	void Update () {

		// Only if the torch is on, deplete battery
		if(_torchsOn) {
			_batteryLevel = _batteryLevel - 1f * 2 * Time.deltaTime;
		}

		// Block of If's to determine battery level and switch sprite accordingly
		if(_batteryLevel <= 100) {
			GetComponent<Image>().sprite = _oneHundredPercentIcon;
		} if(_batteryLevel <= 75) {
			GetComponent<Image>().sprite = _seventyFivePercentIcon;
		} if (_batteryLevel <= 50) {
			GetComponent<Image>().sprite = _fiftyPercentIcon;
		} if(_batteryLevel <= 25) {
			GetComponent<Image>().sprite = _twentyFivePercentIcon;
		} if(_batteryLevel <= 0) {
			GetComponent<Image>().enabled = false;
			_batteryLevel = 0;
			_torchOne.enabled = false;
			_torchTwo.enabled = false;
		}

	}
}
