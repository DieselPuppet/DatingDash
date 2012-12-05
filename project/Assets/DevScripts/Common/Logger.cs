using UnityEngine;
using System.Collections;

public enum LogLevel
{
	LOG_ALL,
	LOG_INFO,
	LOG_WARNING,
	LOG_ERROR,
	LOG_FATAL
}

public class Logger : MonoBehaviour 
{
	private static LogLevel _currentLevel;
	
	public static void init(LogLevel level = LogLevel.LOG_ALL)
	{
		_currentLevel = level;
	}
	
	public static void message(LogLevel level, string message)
	{
		if (!Config.instance.isDevelopmentBuild)
			return;
		
		if (level > _currentLevel || level == LogLevel.LOG_ALL)
		{
			Debug.Log(level.ToString()+": "+message);
		}
	}
}
