using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour {

	Text text;
	float cool = 0;
	public float displayTime = 1;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = "";
		BallGame.TeamScored += TeamScoreDisplay;
	}

	void OnDestroy() {
		BallGame.TeamScored -= TeamScoreDisplay;
	}
	
	// Update is called once per frame
	void Update () {
		cool -= Time.deltaTime;
		if (cool<=0){
			text.text = "";
		}
	}

	void TeamScoreDisplay(Team team){
		text.text = team.name + " a marqué, woo!";
		text.color = team.color;
		cool = displayTime;
	}


}
