using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

	public iInputDevice device;
	public float speed;
	public float rotationSpeed;

	void Start (){
		device = new KeyboardDevice (); // TODO: Assign a device based on settings
	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime * device.GetAcceleration ();
		transform.Rotate (Vector3.up, rotationSpeed * Time.deltaTime * device.GetSteering());
	}
}
