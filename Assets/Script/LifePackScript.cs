using UnityEngine;
using System.Collections;

public class LifePackScript : MonoBehaviour {

	public int healStrength;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.layer == 8){
			other.gameObject.GetComponent<PlayerController>().HealHealth(healStrength);

			Destroy(this.gameObject);
		}
		else if (other.gameObject.layer == 10){
			other.gameObject.GetComponent<PlayerController2>().HealHealth(healStrength);
			
			Destroy(this.gameObject);
		}

	}
}
