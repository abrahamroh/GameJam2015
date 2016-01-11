using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	public float restTime;
	public float movementSpeed;
	public Vector3[] wayPoints;

	float timeRested;
	int destinationIndex;
	Vector3 nextDestination;

	// Use this for initialization
	void Start () {
		destinationIndex = -1;
		timeRested = 0;
		nextDestination = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(wayPoints.Length > 1){
			if(Vector3.Distance(gameObject.transform.position, nextDestination) < (movementSpeed * Time.fixedDeltaTime / 2f)){
				if(timeRested >= restTime){
					destinationIndex = (destinationIndex + 1) % wayPoints.Length;
					nextDestination = wayPoints[destinationIndex];
					MoveTowardsDestination();
					timeRested = 0;
				}
				else{
					StopMovement();
					timeRested += Time.fixedDeltaTime;
				}
			}
		}
	}

	void MoveTowardsDestination(){
		Vector3 moveDirection = (nextDestination - transform.position).normalized;
		GetComponent<Rigidbody2D>().velocity = moveDirection * movementSpeed;
	}

	void StopMovement(){
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
	}
}
