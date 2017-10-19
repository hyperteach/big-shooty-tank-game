using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

	public float speed;
	public float rotationSpeed;
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime * Input.GetAxis ("Vertical");
		transform.Rotate (Vector3.up, rotationSpeed * Time.deltaTime * Input.GetAxis ("Horizontal"));
	}
}
