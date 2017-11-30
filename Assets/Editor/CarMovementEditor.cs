using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CarMovement))]
[CanEditMultipleObjects]
public class CarMovementEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		base.DrawDefaultInspector ();


		CarMovement myTarget = (CarMovement)target;

		float speed = myTarget.power / myTarget.friction;
		float acceleration = myTarget.friction;
		float before = acceleration;

		speed = EditorGUILayout.FloatField ("Speed", speed);
		acceleration = EditorGUILayout.FloatField ("Acceleration", acceleration/2)*2;

		float ratio = acceleration / before;

		myTarget.power = Mathf.Max(0.1f,speed * myTarget.friction * ratio);
		myTarget.friction = Mathf.Max(0.1f,myTarget.friction * ratio);
	}
}
