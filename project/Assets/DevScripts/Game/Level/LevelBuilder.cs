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
	
	public Level buildLevel(string worldName, string levelName, GameMode mode)
	{		
		Logger.message(LogLevel.LOG_INFO, "Building level - "+worldName);
		
		string levelPath = "Levels/"+worldName+"/Level";
		
		GameObject rootObject = (GameObject)Instantiate(Resources.Load(levelPath), Vector3.zero, Quaternion.identity);
		rootObject.name = "Level";
		
		LevelDesc desc = LevelsCollection.instance.getDesc(levelName);
		
		Level level = rootObject.GetComponent<Level>();
		level.configure(desc);
		
		if (mode == GameMode.COMPANY)
		{
			//level.pushTask(new LevelTask());
		}
		
		return level;
	}
}
