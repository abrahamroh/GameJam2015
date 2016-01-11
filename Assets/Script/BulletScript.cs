using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public float lifeSpan;
	public int bulletStrength;

	// Use this for initialization
	void Start () {
		StartCoroutine(DeathTimer(lifeSpan));
	}

	IEnumerator DeathTimer (float timer){
		yield return new WaitForSeconds(timer);
		Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.layer == 8 || other.gameObject.layer == 10){
			other.gameObject.GetComponent<PlayerController2>().TakeDamage(bulletStrength);
		}

		Destroy(this.gameObject);
	}
}
