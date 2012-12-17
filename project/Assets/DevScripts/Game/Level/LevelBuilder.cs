using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LevelBuilder : MonoBehaviour 
{
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0, 100, 50), "Build level"))
		{
			Debug.Log("Serealize level - "+gameObject.name);
		}
	}
}
