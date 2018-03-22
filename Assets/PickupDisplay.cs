using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PickupDisplay : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = "";
		BallGame.BallPickedUp += DisplayPickup;
	}
	void OnDestroy(){
		BallGame.BallPickedUp -= DisplayPickup;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void DisplayPickup(Team team){
		text.text = team.name + " picked up the ball";
		text.color = team.color;
	}
}
