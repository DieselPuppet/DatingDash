using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
	Level level;
	
	public tk2dSpriteCollection testCollection;
	
	private static LevelLoader _instance;
	public static LevelLoader instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(LevelLoader)) as LevelLoader;
			}
			
			return _instance;
		}
	}		
	
	public Level buildLevel(string levelName, GameMode mode)
	{		
		Logger.message(LogLevel.LOG_INFO, "Building level - "+levelName);
		
		string levelPath = "Levels/"+levelName+"/Level";
		
		GameObject rootObject = (GameObject)Instantiate(Resources.Load(levelPath), Vector3.zero, Quaternion.identity);
		rootObject.name = "Level";
		
		level = rootObject.GetComponent<Level>();
		level.name = levelName;
		
		if (mode == GameMode.COMPANY)
		{
			//level.pushTask(new LevelTask());
		}
		
		return level;
	}
}