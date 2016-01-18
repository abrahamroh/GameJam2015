using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour {
	public static int playerHealth;

	public int startingHealth;
	public float movementSpeed;
	public float turningSpeed;
	public float bulletSpeed;
	public float firingRate;
	
	public GameObject playerProjectile;
	
	float rapidFireTimer;

	AudioManager audioScript;
	
	// Use this for initialization
	void Start () {
		audioScript = GameObject.Find("Mic").GetComponent<AudioManager>();

		rapidFireTimer = 0;
		playerHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		GetMovement();
		GetAttack();
	}
	
	void FixedUpdate(){
		UpdateRapidTimer();
		GetRapidFire();

		if(playerHealth <= 0){
			Destroy(this.gameObject);
			audioScript.redWinSfx(transform.position);
		}
	}
	
	void GetMovement(){
		float currentVelocityY = GetComponent<Rigidbody2D>().velocity.y;
		
		float horizontalMovement = Input.GetAxis("Horizontal2");
		if(horizontalMovement < 0){
			GetComponent<Rigidbody2D>().velocity = new Vector2(-movementSpeed, currentVelocityY);
		}
		else if(horizontalMovement > 0){
			GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, currentVelocityY);
		}
		else{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, currentVelocityY);
		}
		
		float currentVelocityX = GetComponent<Rigidbody2D>().velocity.x;
		float verticalMovement = Input.GetAxis("Vertical2");
		if(verticalMovement < 0){
			GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocityX, -movementSpeed);
		}
		else if(verticalMovement > 0){
			GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocityX, movementSpeed);
		}
		else{
			GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocityX, 0);
		}
		
		if(horizontalMovement < 0 && verticalMovement < 0){
			GetComponent<Rigidbody2D>().velocity = (new Vector2(-1, -1).normalized) * movementSpeed;
		}
		else if (horizontalMovement < 0 && verticalMovement > 0){
			GetComponent<Rigidbody2D>().velocity = (new Vector2(-1, 1).normalized) * movementSpeed;
		}
		else if (horizontalMovement > 0 && verticalMovement < 0){
			GetComponent<Rigidbody2D>().velocity = (new Vector2(1, -1).normalized) * movementSpeed;
		}
		else if (horizontalMovement > 0 && verticalMovement > 0){
			GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 1).normalized) * movementSpeed;
		}
		
		float angularMovement = Input.GetAxis("Turn2");
		if(angularMovement < 0){
			GetComponent<Rigidbody2D>().angularVelocity = turningSpeed;
		}
		else if(angularMovement > 0){
			GetComponent<Rigidbody2D>().angularVelocity = -turningSpeed;
		}
		else{
			GetComponent<Rigidbody2D>().angularVelocity = 0;
		}
	}
	
	void GetAttack(){
		if(Input.GetButtonDown("Fire2")){
			rapidFireTimer = 0;
			Shoot();
		}
	}
	
	void GetRapidFire(){
		//if(Input.GetButton("Fire2")){
		if(Input.GetAxis("Fire2") < 0){
			if(rapidFireTimer == 0){
				Shoot();
			}
		}
		else if (Input.GetAxis("Fire2") == 0){
			rapidFireTimer = 0;
		}
	}
	
	void UpdateRapidTimer(){
		//if(Input.GetButton("Fire2")){
		if(Input.GetAxis("Fire2") < 0){
			rapidFireTimer += Time.fixedDeltaTime;
		}
		
		if(rapidFireTimer > firingRate){
			rapidFireTimer = 0;
		}
	}
	
	void  Shoot(){
		Vector3 spawnOffset = new Vector3(0, GetComponent<SpriteRenderer>().sprite.bounds.size.y / 4f, 0);
		spawnOffset = transform.rotation * spawnOffset;
		Vector3 spawnPoint = transform.position - spawnOffset;
		GameObject newBullet = (GameObject)Instantiate(playerProjectile, spawnPoint, transform.localRotation);
		
		Vector2 bulletVelocity = new Vector2(spawnOffset.x, spawnOffset.y);
		bulletVelocity = bulletVelocity.normalized * bulletSpeed;
		newBullet.GetComponent<Rigidbody2D>().velocity = -bulletVelocity;

		audioScript.blueFireSfx(transform.position);
	}
	
	public void TakeDamage(int damage){
		playerHealth -= damage;

		audioScript.redHitBlueSfx(transform.position);
	}
}
