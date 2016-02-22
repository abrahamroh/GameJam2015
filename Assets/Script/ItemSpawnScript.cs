using UnityEngine;
using System.Collections;

public class ItemSpawnScript : MonoBehaviour {

	public float spawnCoolDown;
	public GameObject spawnItem;

	float coolDownTimer;
	GameObject spawnedItem;

	// Use this for initialization
	void Start () {
		coolDownTimer = spawnCoolDown;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(spawnedItem == null){
			coolDownTimer -= Time.fixedDeltaTime;

			if(coolDownTimer <= 0 ){
				if(spawnItem != null){
					spawnedItem = (GameObject)Instantiate(spawnItem, transform.position, transform.rotation);
				}
				coolDownTimer = spawnCoolDown;
			}
		}
	}
}
