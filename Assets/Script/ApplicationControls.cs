using UnityEngine;
using System.Collections;

public class ApplicationControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UnityEngine.Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Exit")){
			Application.Quit();
			UnityEngine.Cursor.visible = true;
		}

		if(Input.GetButtonDown("Restart")){
			UnityEngine.Cursor.visible = false;
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
