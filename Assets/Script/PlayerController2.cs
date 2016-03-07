using UnityEngine;
using System.Collections;
using AttackScript;

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
	Attacks attackManager;
	
	// Use this for initialization
	void Start () {
		audioScript = GameObject.Find("Mic").GetComponent<AudioManager>();
		attackManager = new Attacks(GetComponent<SpriteRenderer>().sprite, transform, playerProjectile, bulletSpeed);

		rapidFireTimer = 0;
		playerHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(!ApplicationControls.isGamePaused()){
			GetMovement();
			GetAttack();
		}
	}
	
	void FixedUpdate(){
		if(!ApplicationControls.isGamePaused()){
			UpdateRapidTimer();
			GetRapidFire();

			if(playerHealth <= 0){
				Destroy(this.gameObject);
				audioScript.redWinSfx(transform.position);
				ApplicationControls.GameOver(1);
			}
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
	
	void Shoot(){
		attackManager.Shoot(1);
		audioScript.redFireSfx(transform.position);
	}
	
	public void TakeDamage(int damage){
		playerHealth -= damage;

		audioScript.redHitBlueSfx(transform.position);
	}
	
	public void HealHealth(int health){
		playerHealth += health;
		if(playerHealth > startingHealth){
			playerHealth = startingHealth;
		}
	}
}
