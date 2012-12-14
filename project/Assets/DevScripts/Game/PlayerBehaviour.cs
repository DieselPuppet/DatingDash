using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerState
{
	DEFAULT,
	MOVING,
	BUSY
}

public class PlayerBehaviour : MonoBehaviour
{
	private static PlayerBehaviour _instance;
	public static PlayerBehaviour instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
			}
			
			return _instance;
		}
	}		
	
	tk2dAnimatedSprite _sprite;
	
	private PlayerState _currentState;
	public PlayerState currentState
	{
		get 
		{
			return _currentState;
		}
	}
	
	PathNavigator _navigator;
	Dictionary<MoveDirection, string> _animations = new Dictionary<MoveDirection, string>();
	
	LevelItem _activeTarget = null;
	ArrayList _targetsStack  = new ArrayList();
	
	void Awake()
	{
		_sprite = gameObject.GetComponentInChildren<tk2dAnimatedSprite>();
	
		_animations.Add(MoveDirection.DOWN, "run_down");
		_animations.Add(MoveDirection.DOWN_RIGHT, "run_down_right");
		_animations.Add(MoveDirection.RIGHT, "run_right");
		_animations.Add(MoveDirection.UP_RIGHT, "run_up_right");
		_animations.Add(MoveDirection.UP, "run_up");
		_animations.Add(MoveDirection.LEFT, "run_right");
		_animations.Add(MoveDirection.DOWN_LEFT, "run_down_right");
		_animations.Add(MoveDirection.UP_LEFT, "run_up_right");
		
		_navigator = gameObject.GetComponent<PathNavigator>();
		_navigator.spriteHeight = _sprite.GetBounds().size.y;
	}
		
	void Start()
	{
		Vector2 v = PathGraph.instance.getPointByName("milk");
		_navigator.placeIn(new Vector3(v.x, v.y, -1));		
	}
		
	public void setAnimation(MoveDirection dir)
	{			
		if (!_animations.ContainsKey(dir))
		{
			Logger.message(LogLevel.LOG_ERROR, "Animation for MoveDirection "+dir.ToString()+" is not assigned!");
		}
		else 
		{
			_sprite.Play(_animations[dir]);
		}		
	}
	
	public void moveTo(LevelItem target)
	{
		if (_currentState == PlayerState.DEFAULT)
		{
			if (_activeTarget != null && target.pointName == _activeTarget.pointName)
			{
				_activeTarget.onAction();
			}
			else 
			{
				_activeTarget = target;
				
				setState(PlayerState.MOVING);
				
				_navigator.startRoute(_activeTarget);				
			}
		}
		else
		{
			if (_targetsStack.Count > 0)
			{
				LevelItem item = (LevelItem)_targetsStack[0];
				if(item.pointName == target.pointName)
					return;				
			}
			else if (_activeTarget.pointName == target.pointName)
			{
				return;
			}

			_targetsStack.Add(target);
		}
	}
	
	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 100, 50), "Stack"))
		{
			foreach(LevelItem item in _targetsStack)
			{
				Debug.Log(item.pointName);
			}
		}
	}
	
	public void onRouteFinish()
	{	
		_activeTarget.onAction();
		
		if (_targetsStack.Count > 0 && _currentState == PlayerState.DEFAULT)
		{
			LevelItem nextTarget = (LevelItem)_targetsStack[0];
			moveTo(nextTarget);
			_targetsStack.RemoveAt(0);
		}
		else 
		{
			_sprite.Play("work");
		}
	}
	
	// check for pause
	public void setBusy(float time=0)
	{
		if (time != 0)
		{
			_currentState = PlayerState.BUSY;
			Invoke("unbuzy", time);
		}
	}
	
	void unbuzy()
	{
		_currentState = PlayerState.DEFAULT;
		
		if (_targetsStack.Count > 0)
		{
			LevelItem nextTarget = (LevelItem)_targetsStack[0];
			moveTo(nextTarget);
			_targetsStack.RemoveAt(0);
		}		
	}
	
	public void onPause()
	{
	}
	
	public void onResume()
	{
	}
	
	public void setState(PlayerState state)
	{
		Logger.message(LogLevel.LOG_INFO, "Player state sets to "+state.ToString());
		
		_currentState = state;
		
		switch(_currentState)
		{
		case PlayerState.DEFAULT:
		case PlayerState.BUSY:
		case PlayerState.MOVING:
			break;
			
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown player state - "+_currentState.ToString());
			break;
		}		
	}
}
