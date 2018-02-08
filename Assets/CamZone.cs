using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZone : MonoBehaviour {
	public Vector2 size;
	public Vector3 camPos;
	public Vector3 camRot;
	// Use this for initialization

	public bool InZone(Vector3 position){
		return
			position.x>transform.position.x-size.x &&
			position.x<transform.position.x+size.x &&
			position.z>transform.position.z-size.y &&
			position.z<transform.position.z+size.y;
	}

	void OnDrawGizmos(){
		Vector3 pos = transform.position;
		for (int i = 0; i<5; i++){
			pos.y = i*0.25f;
			Gizmos.DrawLine(
				pos-Vector3.right*size.x-Vector3.forward*size.y,
				pos-Vector3.right*size.x+Vector3.forward*size.y
			);
			Gizmos.DrawLine(
				pos-Vector3.right*size.x+Vector3.forward*size.y,
				pos+Vector3.right*size.x+Vector3.forward*size.y
			);
			Gizmos.DrawLine(
				pos-Vector3.right*size.x-Vector3.forward*size.y,
				pos+Vector3.right*size.x-Vector3.forward*size.y
			);
			Gizmos.DrawLine(
				pos+Vector3.right*size.x-Vector3.forward*size.y,
				pos+Vector3.right*size.x+Vector3.forward*size.y
			);
		}
		Gizmos.DrawWireSphere(transform.position+camPos,0.1f);
		Gizmos.DrawRay(transform.position+camPos, Quaternion.Euler(camRot)*Vector3.forward);
	}

}
