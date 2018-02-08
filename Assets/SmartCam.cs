using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCam : MonoBehaviour {

	public Transform follow;
	public float smoothTime;
	public float rotationLerp;
	Vector3 offset;
	Vector3 velocity;
	CamZone[] zones;
	Quaternion originalRotation;

	// Use this for initialization
	void Start () {
		offset = transform.position - follow.position;
		originalRotation = transform.rotation;
		zones = FindObjectsOfType<CamZone>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (CamZone zone in zones){
			if (zone.InZone(follow.position)){
				transform.position = Vector3.SmoothDamp(transform.position, zone.transform.position + zone.camPos, ref velocity, smoothTime);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(zone.camRot), Time.deltaTime*rotationLerp);
				return;
			}
		}
		transform.position = Vector3.SmoothDamp(transform.position, follow.position+offset, ref velocity, smoothTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime*rotationLerp);
	}

}
