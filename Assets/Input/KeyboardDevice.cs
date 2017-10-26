using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardDevice : iInputDevice {

	public KeyCode left = KeyCode.Q;
	public KeyCode right = KeyCode.D;
	public KeyCode forward = KeyCode.Z;
	public KeyCode backward = KeyCode.S;

	public KeyCode fire = KeyCode.Space;

	public KeyCode aimleft = KeyCode.J;
	public KeyCode aimright = KeyCode.L;




	float iInputDevice.GetSteering(){
		return (Input.GetKey(left) ? -1 : 0) + (Input.GetKey(right) ? 1 : 0);
	}
	float iInputDevice.GetAcceleration(){
		return (Input.GetKey(backward) ? -1 : 0) + (Input.GetKey(forward) ? 1 : 0);
	}
	float iInputDevice.GetAiming(){
		return (Input.GetKey(aimleft) ? -1 : 0) + (Input.GetKey(aimright) ? 1 : 0);
	}
	bool iInputDevice.GetFire(){
		return Input.GetKey(fire);
	}

}
