using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDevice : iInputDevice {

	float iInputDevice.GetSteering(){
		return 0;
	}
	float iInputDevice.GetAcceleration(){
		return 0;
	}
	float iInputDevice.GetAiming(){
		return 0;
	}
	bool iInputDevice.GetFire(){
		return false;
	}

}
