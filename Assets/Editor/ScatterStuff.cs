using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScatterStuff : EditorWindow {

	static GameObject prefab;
	static Terrain terrain;
	static int count = 100;

	[MenuItem("GameObject/Scatter Game Objects")]
	static void Init()
	{
		ScatterStuff window = (ScatterStuff)EditorWindow.GetWindow(typeof(ScatterStuff));
		window.Show();
	}


	void OnGUI()
	{
		if (terrain == null) {
			terrain = FindObjectOfType<Terrain> ();
		}
		float width = terrain.terrainData.size.x;
		float length = terrain.terrainData.size.z;
		prefab = (GameObject) EditorGUILayout.ObjectField ("Place this", prefab, typeof(GameObject), true);
		count = EditorGUILayout.IntField ("How many?", count);
		float density = ((float)count) / (width * length);
		density = EditorGUILayout.FloatField ("Density", density*10000)/10000;
		count = Mathf.RoundToInt(density * (width * length));

		if (count > 0 && prefab != null) {
			if (GUILayout.Button("SPAWN!")){
				GameObject parent = new GameObject ();
				parent.name = "Parent";
				for (int i = 0; i < count; i++) {
					float height = terrain.terrainData.size.y;
					Vector3 position = terrain.GetPosition ()
										+Vector3.right*Random.value*width
										+Vector3.forward*Random.value*length;
					position.y = terrain.SampleHeight (position);
					GameObject go = GameObject.Instantiate (prefab);
					go.transform.parent = parent.transform;
					go.transform.position = position;
					go.transform.rotation = Quaternion.AngleAxis (360*Random.value, Vector3.up);
				}
			}
		}

	}

}
