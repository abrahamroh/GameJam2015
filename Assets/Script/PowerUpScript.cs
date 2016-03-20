using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {
	
	public int power;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject.layer == 8){
			other.gameObject.GetComponent<PlayerController>().PowerUp(power);
			
			Destroy(this.gameObject);
		}
		else if (other.gameObject.layer == 10){
			other.gameObject.GetComponent<PlayerController2>().PowerUp(power);
			
			Destroy(this.gameObject);
		}
		
	}
}
