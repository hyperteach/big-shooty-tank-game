﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState {
	// HUNTING,
	// ATTACKING,
	// FLEEING,
	// DEFENDING,
	WANDERING,
}

public class BotPlayer : MonoBehaviour {

	AIState state = AIState.WANDERING;
	float steer = 0;
	float accelerate = 0;
	float aim = 0;
	bool fire = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case AIState.WANDERING:
			Wander ();
			break;
		}
	}

	[Header("Wander state")]
	public float wanderDirectionChangeTime = 3;
	float wanderTargetAngle = 0;
	float wanderNextTarget = 0;
	void Wander(){
		wanderNextTarget -= Time.deltaTime;
		if (wanderNextTarget <= 0) {
			wanderNextTarget += wanderDirectionChangeTime;
			wanderTargetAngle = Random.value * Mathf.PI * 2;
		}

		aim = 0;
		fire = false;
		accelerate = 0.75f;
		steer = Mathf.Sin (Time.time);
	}

	public float GetSteering(){
		return steer;
	}
	public float GetAcceleration(){
		return accelerate;
	}
	public float GetAiming(){
		return aim;
	}
	public bool GetFire(){
		return fire;
	}

}
