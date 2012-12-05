using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
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
		
		string levelPath = "Levels/"+levelName;
		if (Config.instance.isDevelopmentBuild)
			levelPath = "Levels/References/"+levelName+"Reference";
		
		GameObject rootObject = (GameObject)Instantiate(Resources.Load(levelPath), Vector3.zero, Quaternion.identity);
		rootObject.name = "Level";
		
		Level level = rootObject.GetComponent<Level>();
		
		if (mode == GameMode.COMPANY)
		{
			level.pushTask(new LevelTask());
		}
		
		return level;
	}
}