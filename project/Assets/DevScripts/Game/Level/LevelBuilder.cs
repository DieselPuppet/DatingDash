using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour 
{
	private static LevelBuilder _instance;
	public static LevelBuilder instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(LevelBuilder)) as LevelBuilder;
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
		
		Level level = rootObject.GetComponent<Level>();
		level.name = levelName;
		
		if (mode == GameMode.COMPANY)
		{
			//level.pushTask(new LevelTask());
		}
		
		return level;
	}
}
