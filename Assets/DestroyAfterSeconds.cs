using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

	public float seconds;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, seconds);
	}
}
