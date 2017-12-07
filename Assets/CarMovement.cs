using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarType {
	HUMAN,
	ROBOT
}

public class CarMovement : MonoBehaviour {

	public CarType type;
	public iInputDevice device;

	public float power=20;
	public float friction=2;

	public float steerPower=500;
	public float angularFriction=2;

	Vector3 velocity;
	float orientation;
	float angularVelocity;

	void Start (){
		switch (type) {
		case CarType.HUMAN:
			device = new KeyboardDevice (); // TODO: Assign a device based on settings
			break;
		case CarType.ROBOT:
			device = new AutoDevice (this.gameObject); // TODO: Write an AIDevice
			break;
		}
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
