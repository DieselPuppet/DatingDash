using UnityEngine;
using System.Collections;

public class EntryPoint : MonoBehaviour 
{
	void Awake()
	{		
		SaveManager.instance.load();
		
		Logger.init(LogLevel.LOG_WARNING);
		
		Platform.instance.init();
	}
	
	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 200, 100), "Cafe level"))
		{
			Application.LoadLevel("Location");
		}
	}
}
