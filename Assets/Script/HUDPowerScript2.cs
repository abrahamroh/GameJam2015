using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDPowerScript2 : MonoBehaviour {
	Text currentPowerLevel;
	
	// Use this for initialization
	void Start () {
		currentPowerLevel = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		currentPowerLevel.text = "Power: " + PlayerController2.powerLevel;
	}
}
