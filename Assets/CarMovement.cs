using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

	public iInputDevice device;


	public float power;
	public float friction;
	public Vector3 velocity;

	public float orientation;
	public float steerPower;
	public float angularVelocity;
	public float angularFriction;




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
