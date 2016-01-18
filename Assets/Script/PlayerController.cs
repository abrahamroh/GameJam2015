﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
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
			audioScript.blueWinSfx(transform.position);
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

	void  Shoot(){
		Vector3 spawnOffset = new Vector3(0, GetComponent<SpriteRenderer>().sprite.bounds.size.y / 4f, 0);
		spawnOffset = transform.rotation * spawnOffset;
		Vector3 spawnPoint = transform.position - spawnOffset;
		GameObject newBullet = (GameObject)Instantiate(playerProjectile, spawnPoint, transform.localRotation);
		
		Vector2 bulletVelocity = new Vector2(spawnOffset.x, spawnOffset.y);
		bulletVelocity = bulletVelocity.normalized * bulletSpeed;
		newBullet.GetComponent<Rigidbody2D>().velocity = -bulletVelocity;

		audioScript.redFireSfx(transform.position);
	}

	public void TakeDamage(int damage){
		playerHealth -= damage;

		audioScript.blueHitRedSfx(transform.position);
	}
}
