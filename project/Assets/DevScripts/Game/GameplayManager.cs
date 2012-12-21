using UnityEngine;
using System.Collections;

public enum GameMode
{
	COMPANY,
	ENDLESS
}

public class GameplayManager : MonoBehaviour 
{
	private static GameplayManager _instance;
	public static GameplayManager instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(GameplayManager)) as GameplayManager;
			}
			
			return _instance;
		}
	}
	
	private bool _isStarted = false;
	private bool _isPaused = false;
	
	Level _currentLevel;
	
	private GameMode _currentGameMode;
	public GameMode currentGameMode
	{
		get 
		{
			return _currentGameMode;
		}
	}
	
	void Start()
	{
		startGame(GameMode.COMPANY, "Cafe");
	}
	
	public void startGame(GameMode gmode, string levelName = "TestLevel")
	{
		_currentGameMode = gmode;
		
		_currentLevel = LevelBuilder.instance.buildLevel("Cafe", "Level", GameMode.COMPANY);
		
		_isPaused = false;
		_isStarted = true;
		
		Logger.message(LogLevel.LOG_INFO, "GameplayManager.startGame(). Current game mode = "+_currentGameMode.ToString());
	}
	
	public void pauseGame()
	{
		_isPaused = true;
		
		Logger.message(LogLevel.LOG_INFO, "GameplayManager.pauseGame()");
	}
	
	public void resumeGame()
	{
		_isPaused = false;
		
		Logger.message(LogLevel.LOG_INFO, "GameplayManager.resumeGame()");
	}
	
	void OnGUI()
	{
		if (GUI.Button(new Rect(0, Screen.height-50, 100, 50), "Apply"))
		{			
			Destroy(_currentLevel.gameObject);
			startGame(GameMode.COMPANY, "Cafe");
		}
	}	
	
	void finishGame()
	{
		if (_currentGameMode == GameMode.COMPANY)
		{
		}
		else 
		{
		}
		
		Logger.message(LogLevel.LOG_INFO, "GameplayManager.fihishGame().");
	}
	
	void Update()
	{		
		if (_isStarted && !_isPaused)
		{
			_currentLevel.process(_currentGameMode);
			
			if (_currentLevel.isComplete)
			{
				// stop Game Proccess
				// show win popup
			}
		}
	}
}
