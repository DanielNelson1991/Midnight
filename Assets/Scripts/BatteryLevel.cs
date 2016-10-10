using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BatteryLevel : MonoBehaviour {

	public static float _batteryLevel;
	public Light _torchOne;
	public Light _torchTwo;
	public static bool _torchsOn;

    public Sprite[] _flashLightSprite;

    [Tooltip("The speed of which the flashlight decreases")]
    public float _flashLightSpeedDecrease = 0.0f;

    [Tooltip("The amount decreased per frame")]
    public float _flashLightDecreaseAmount = 0.0f;

	// Use this for initialization
	void Start () {
		_batteryLevel = 100;
	}
	
	// Update is called once per frame
	void Update () {

		// Only if the torch is on, deplete battery
		if(_torchsOn) {
			_batteryLevel = _batteryLevel - _flashLightDecreaseAmount * _flashLightSpeedDecrease * Time.deltaTime;
		}

		// Block of If's to determine battery level and switch sprite accordingly
		if(_batteryLevel <= 100) {
			GetComponent<Image>().sprite = _flashLightSprite[0];
		} if(_batteryLevel <= 75) {
			GetComponent<Image>().sprite = _flashLightSprite[1];
		} if (_batteryLevel <= 50) {
			GetComponent<Image>().sprite = _flashLightSprite[2];
		} if(_batteryLevel <= 25) {
			GetComponent<Image>().sprite = _flashLightSprite[3];
		} if(_batteryLevel <= 0) {
			GetComponent<Image>().enabled = false;
			_batteryLevel = 0;
			_torchOne.enabled = false;
			_torchTwo.enabled = false;
		}

	}

    /// <summary>
    /// Increase the flashlight amount
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseFlashlightBattery(float amount)
    {
        // If battery level is below 100, we can add to it.
        if(_batteryLevel < 100)
        {
            _batteryLevel += amount;
        }
    }
}
