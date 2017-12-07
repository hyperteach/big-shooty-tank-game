using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDevice : iInputDevice {

	BotPlayer bot;

	public AutoDevice(GameObject myself){
		bot = myself.GetComponent<BotPlayer> ();
		if (bot == null) {
			bot = myself.AddComponent<BotPlayer> ();
		}
	}

	float iInputDevice.GetSteering(){
		return bot.GetSteering();
	}
	float iInputDevice.GetAcceleration(){
		return bot.GetAcceleration();
	}
	float iInputDevice.GetAiming(){
		return bot.GetAiming();
	}
	bool iInputDevice.GetFire(){
		return bot.GetFire();
	}

}
