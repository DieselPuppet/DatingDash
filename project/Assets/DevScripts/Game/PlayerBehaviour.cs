using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerState
{
	DEFAULT,
	WALKING,
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
	
	LevelItem _activeTarget = null;
	ArrayList _targetsStack  = new ArrayList();
	
	void Awake()
	{
		_sprite = gameObject.GetComponentInChildren<tk2dAnimatedSprite>();
		
		_navigator = gameObject.GetComponent<PathNavigator>();
		_navigator.spriteHeight = _sprite.GetBounds().size.y;
	}
		
	void Start()
	{
		Vector2 v = PathGraph.instance.getPointByName("milk");
		_navigator.placeIn(new Vector3(v.x, v.y, -1));		
	}
	
	public void moveTo(LevelItem target)
	{
		if (_currentState != PlayerState.WALKING && _currentState != PlayerState.BUSY && _activeTarget.pointName != target.pointName)
		{
			_activeTarget = target;
			setState(PlayerState.WALKING);
			
			_navigator.startRoute(target);
		}
		else
		{
			if (_targetsStack.Count > 0 && target == _targetsStack[0])
				return;
			
			_targetsStack.Add(target);
		}
	}		
	
	public void onRouteFinish()
	{
		//_sprite.Play("work");
		
		// tmp
		setState(PlayerState.DEFAULT);
		//
		
		_activeTarget.onAction();
		
		if (_targetsStack.Count > 0 && _currentState == PlayerState.DEFAULT)
		{		
			LevelItem nextTarget = (LevelItem)_targetsStack[0];
			
			moveTo(nextTarget);
			
			_targetsStack.RemoveAt(0);
		}		
	}
	
	public void seatVisitor(NPC npc)
	{
		// check
		npc.setState(NPC_STATE.SITTING);
	}	
	
	public void serveVisitor(NPC npc)
	{
		npc.setState(NPC_STATE.MEAL);
	}
	
	public void cleanUpPosition()
	{
	}
	
	public void calculateNPC(NPC npc)
	{
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
		
		if (_targetsStack.Count > 0 && _currentState == PlayerState.DEFAULT)
		{			
			moveTo((LevelItem)_targetsStack[0]);
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
		_currentState = state;
		
		switch(_currentState)
		{
		case PlayerState.DEFAULT:
		case PlayerState.BUSY:
		case PlayerState.WALKING:
			break;
			
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown player state - "+_currentState.ToString());
			break;
		}		
	}
}
