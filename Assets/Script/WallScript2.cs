using UnityEngine;
using System.Collections;

public class WallScript2 : MonoBehaviour {
	public float restTime;
	public float rotationSpeed;
	public float[] rotationPoints;
	
	float timeRested;
	int angleIndex;
	float nextAngle;
	
	// Use this for initialization
	void Start () {
		angleIndex = -1;
		timeRested = 0;
		nextAngle = transform.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(rotationPoints.Length > 1){
			if((Mathf.Abs(nextAngle - gameObject.transform.rotation.eulerAngles.z) % 360f) < Mathf.Abs(rotationSpeed * Time.fixedDeltaTime / 2f)){
				if(timeRested >= restTime){
					angleIndex = (angleIndex + 1) % rotationPoints.Length;
					nextAngle = rotationPoints[angleIndex];
					MoveTowardsAngle();
					timeRested = 0;
				}
				else{
					StopRotation();
					timeRested += Time.fixedDeltaTime;
				}
			}
		}
	}
	
	void MoveTowardsAngle(){
		JointMotor2D newMotor = GetComponent<HingeJoint2D>().motor;
		newMotor.motorSpeed = rotationSpeed;
		GetComponent<HingeJoint2D>().motor = newMotor;
	}
	
	void StopRotation(){
		JointMotor2D newMotor = GetComponent<HingeJoint2D>().motor;
		newMotor.motorSpeed = 0;
		GetComponent<HingeJoint2D>().motor = newMotor;
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
	}
}
