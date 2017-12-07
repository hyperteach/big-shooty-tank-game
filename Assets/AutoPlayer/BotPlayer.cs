using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState {
	HUNTING,
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
		case AIState.HUNTING:
			Hunt ();
			break;
		}
	}

	[Header("Wander state")]
	public float wanderDirectionChangeTime = 3;
	float wanderTargetAngle = 0;
	float wanderNextTarget = 0;
	public float wanderSteerPower = 0.4f;
	void Wander(){
		wanderNextTarget -= Time.deltaTime;
		if (wanderNextTarget <= 0) {
			wanderNextTarget += wanderDirectionChangeTime;
			wanderTargetAngle = Random.value * Mathf.PI * 2;
			if (Random.value < 0.25) {
				state = AIState.HUNTING;
			}
					
		}

		float currentAngle = Mathf.Atan2 (transform.forward.z, transform.forward.x);
		float rotation = wanderTargetAngle - currentAngle;

		// Tricks!
		rotation = ((rotation + Mathf.PI) % (Mathf.PI*2)) - Mathf.PI;

		aim = 0;
		fire = false;
		accelerate = 0.5f;
		steer = Mathf.Clamp (rotation*wanderSteerPower, -1, 1);
	}


	[Header("Hunting state")]
	public float huntSteerPower = 0.4f;
	public float huntEatRange = 2;
	Food huntCurrentTarget;
	void Hunt(){
		if (huntCurrentTarget == null || !huntCurrentTarget.gameObject.activeInHierarchy) {
			Debug.Log ("I AM HUNGRY");
			// Get all food
			List<Food> allFood = new List<Food>(FindObjectsOfType<Food> ());
			// Sort food by distance
			allFood = allFood.OrderBy(
				x => Vector2.Distance(this.transform.position,x.transform.position)
			).ToList();
			// Get first element from food
			huntCurrentTarget = allFood [0];
		}

		Vector3 delta = transform.position - huntCurrentTarget.transform.position;
		// If in range of food
		if (delta.magnitude < huntEatRange) {
			// Eat it
			huntCurrentTarget.gameObject.SetActive (false);
			Destroy (huntCurrentTarget.gameObject);
			// And wander
			state = AIState.WANDERING;
			return;
		}


		float currentAngle = Mathf.Atan2 (transform.forward.z, transform.forward.x);
		float targetAngle = Mathf.Atan2 (delta.z, delta.x);
		float rotation = targetAngle - currentAngle;

		// Tricks!
		rotation = ((rotation + Mathf.PI) % (Mathf.PI*2)) - Mathf.PI;

		aim = 0;
		fire = false;
		accelerate = 1;
		steer = Mathf.Clamp (rotation*huntSteerPower, -1, 1);
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
