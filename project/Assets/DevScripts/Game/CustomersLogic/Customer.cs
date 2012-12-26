using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CustomerState
{
	INITIAL_STATE,
	
	WAITING_STAND_HAPPY,
	WAITING_STAND_ANGRY,
	WAITING_SIT_HAPPY,
	WAITING_SIT_ANGRY,
	
	MAKE_ORDER,
	WAITING_ORDER_HAPPY,
	WAITING_ORDER_ANGRY,
	EAT,
	TALK,
	
	UNKNOWN
}

// TODO : add proxies
public class Customer : MonoBehaviour 
{
	Dictionary<string, string> _animationMap = new Dictionary<string, string>();
	Dictionary<CustomerState, CustomerStateDesc> _statesMap = new Dictionary<CustomerState, CustomerStateDesc>();
	
	tk2dAnimatedSprite _sprite;
	
	CustomerDesc _cachedDesc;
	
	CustomerState _currentState = CustomerState.UNKNOWN;
	public CustomerState currentState
	{
		get 
		{
			return _currentState;
		}
	}
	
	int _currentMood;
	float _moodDownSpeed;

	// timers (add Invoker class)
	float lastMoodChangeTime;
	float startOrderTime;
	float startEat;	
	
	void Awake()
	{
		_sprite = gameObject.AddComponent<tk2dAnimatedSprite>();
	}
	
	public void configure(CustomerDesc desc)
	{
		foreach(CustomerStateDesc stateDesc in desc.stateDescribers)
		{
			_statesMap.Add(stateDesc.state, stateDesc);
		}
		
		ContentManager.instance.configureObject(_sprite, desc.spriteAtlas, desc.spriteName);
		ContentManager.instance.precacheAnimation(_sprite, desc.animationAtlas);		
		
		BoxCollider box = gameObject.AddComponent<BoxCollider>();
		box.size = new Vector3(_sprite.GetBounds().size.x, _sprite.GetBounds().size.y, 1);	
		
		_currentMood = 100;
		_moodDownSpeed = 60f/desc.moodDownTime;		
		
		setState(CustomerState.INITIAL_STATE);
		
		_cachedDesc = desc;
	}
	
	public void setState(CustomerState state)
	{
		if (state == _currentState)
			return;
		
		if (_statesMap[state].animName != "")
		{
			playAnim(_statesMap[state].animName);
		}
		
		switch(state)
		{
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer state - "+state);
			break;
		}
		
		_currentState = state;
	}
	
	public void adjustMood(int val)
	{
		_currentMood += val;
		if (_currentMood>=100)
			_currentMood = 100;
	}
	
	private void playAnim(string clipName)
	{
		if (!_animationMap.ContainsKey(clipName))
		{
			Logger.message(LogLevel.LOG_ERROR, "Animation for key "+clipName+" is not assigned");
		}
		else 
		{
			_sprite.Play(_animationMap[clipName]);
		}
	}
}
