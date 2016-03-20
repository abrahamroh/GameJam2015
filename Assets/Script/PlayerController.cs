using UnityEngine;
using System.Collections;
using AttackScript;

public class PlayerController : MonoBehaviour {
	public static int playerHealth;
	public static int powerLevel;
	
	public int startingHealth;
	public float movementSpeed;
	public float turningSpeed;
	public float bulletSpeed;
	public float firingRate;

	public GameObject playerProjectile;

	float rapidFireTimer;

	const int MAX_POWER_LEVEL = 3;
	const int INIT_POWER_LEVEL = 1;

	AudioManager audioScript;
	Attacks attackManager;

	// Use this for initialization
	void Start () {
		audioScript = GameObject.Find("Mic").GetComponent<AudioManager>();
		attackManager = new Attacks(GetComponent<SpriteRenderer>().sprite, transform, playerProjectile, bulletSpeed);

		rapidFireTimer = 0;
		playerHealth = startingHealth;
		powerLevel = INIT_POWER_LEVEL;
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
				audioScript.blueWinSfx(transform.position);
				ApplicationControls.GameOver(2);
			}
		}
	}

	void GetMovement(){
		float currentVelocityY = GetComponent<Rigidbody2D>().velocity.y;
		
		float horizontalMovement = Input.GetAxis("Horizontal");
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
		float verticalMovement = Input.GetAxis("Vertical");
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
		
		float angularMovement = Input.GetAxis("Turn");
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
		if(Input.GetButtonDown("Fire1")){
			rapidFireTimer = 0;
			Shoot();
		}
	}

	void GetRapidFire(){
		if(Input.GetButton("Fire1")){
			if(rapidFireTimer == 0){
				Shoot();
			}
		}
	}

	void UpdateRapidTimer(){
		if(Input.GetButton("Fire1")){
			rapidFireTimer += Time.fixedDeltaTime;
		}

		if(rapidFireTimer > firingRate){
			rapidFireTimer = 0;
		}
	}

	void Shoot(){
		attackManager.Shoot(powerLevel);
		audioScript.redFireSfx(transform.position);
	}

	public void TakeDamage(int damage){
		playerHealth -= damage;
		if(playerHealth < 0){
			playerHealth = 0;
		}

		audioScript.blueHitRedSfx(transform.position);
	}

	public void HealHealth(int health){
		playerHealth += health;
		if(playerHealth > startingHealth){
			playerHealth = startingHealth;
		}
	}
	
	public void PowerUp(int power){
		powerLevel += power;
		if(powerLevel > MAX_POWER_LEVEL){
			powerLevel = MAX_POWER_LEVEL;
		}
	}
}
