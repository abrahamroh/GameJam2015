using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDLivesScript : MonoBehaviour {
	Text remaingLives;

	// Use this for initialization
	void Start () {
		remaingLives = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		remaingLives.text = "Lives: " + PlayerController.playerHealth;
	}
}
