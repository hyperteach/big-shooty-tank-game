using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogGameEvents : MonoBehaviour {

	public GameObject LogPrefab;

	// Use this for initialization
	void Start () {
		BallGame.TeamScored += TeamScored;
		BallGame.BallPickedUp += BallGet;
	}
	void OnDestroy() {
		BallGame.TeamScored -= TeamScored;
		BallGame.BallPickedUp -= BallGet;
	}
	
	void TeamScored (Team team){
		LogWithText(team.name + " scored a point!", team.color);
	}
	void BallGet (Team team){
		LogWithText(team.name + " has the ball!", team.color);
	}
	
	void LogWithText (string log, Color color) {
		GameObject go = Instantiate(LogPrefab, this.transform);
		Text text = go.GetComponent<Text>();
		text.text = log;
		text.color = color;
	}
}
