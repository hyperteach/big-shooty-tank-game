using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

	public iInputDevice device;


	public float power;
	public float friction;

	public float steerPower;
	public float angularFriction;

	Vector3 velocity;
	float orientation;
	float angularVelocity;



	void Start (){
		device = new KeyboardDevice (); // TODO: Assign a device based on settings
	}

	// Update is called once per frame
	void Update () {

		//   a*t + b*t = (a+b) * t

		velocity += (transform.forward * power  * device.GetAcceleration () - velocity * friction) * Time.deltaTime;
		angularVelocity += (steerPower * device.GetSteering () - angularVelocity * angularFriction) * Time.deltaTime;

		transform.position += velocity * Time.deltaTime;
		orientation += angularVelocity * Time.deltaTime;
		transform.rotation = Quaternion.AngleAxis (orientation, Vector3.up);

	}
}
