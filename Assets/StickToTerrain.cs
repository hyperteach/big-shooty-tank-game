using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToTerrain : MonoBehaviour {

	public LayerMask terrainLayerMask;
	public float standoffHeight = 100;

	void LateUpdate () {
		RaycastHit rch;
		if (Physics.Raycast (
			    transform.position + Vector3.up * standoffHeight,
			    Vector3.down,
			    out rch,
			    2 * standoffHeight,
			    terrainLayerMask)) {
			transform.position = rch.point;
		} else {
			// TODO: Check if we need to do something
		}

	}
}
