using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TankGeneratorMenu : EditorWindow {
	[MenuItem("GameObject/Tanks/New Tank")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		TankGeneratorMenu window =
			(TankGeneratorMenu)EditorWindow.GetWindow(typeof(TankGeneratorMenu));
		window.Show();
	}
	[MenuItem("GameObject/Tanks/Destroy tanks")]
	static void DestroyAllTanks(){
		foreach (CarMovement go in FindObjectsOfType<CarMovement>()) {
			go.name += "BOOM";
		}
	}

	void OnGUI()
	{
		
	}

}
