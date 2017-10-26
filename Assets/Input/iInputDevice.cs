using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iInputDevice {

	float GetSteering();
	float GetAcceleration();
	float GetAiming();
	bool GetFire();

}
