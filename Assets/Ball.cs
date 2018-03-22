using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	RugbyAgent _carriedBy;
	public RugbyAgent carriedBy{
		get {
			return _carriedBy;
		}
		set {
			BallGame.instance.BallPickup(value);
			_carriedBy = value;
		}
	}

}
