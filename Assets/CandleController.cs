using UnityEngine;
using System.Collections;

public class CandleController : MonoBehaviour {

	[Tooltip("The speed of which this candle decreases")]
	public float _candleDecreaseSpeed = 0.0f;

	[Tooltip("The amount decreased by")]
	public float _amountDecreasedBy = 0.0f;

	[Tooltip("The Light object, turn off when candle stick empty")]
	public Light _candleLight;

	public float startingAmount;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
