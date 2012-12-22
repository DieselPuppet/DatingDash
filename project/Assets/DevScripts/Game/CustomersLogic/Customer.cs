using UnityEngine;
using System.Collections;

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
	
	UNKNOWN
}

public class Customer : MonoBehaviour 
{
	CustomerDesc _cachedDesc;
	
	CustomerState _currentState;
	public CustomerState currentState
	{
		get 
		{
			return _currentState;
		}
	}
	
	public void setState(CustomerState state)
	{
		if (state == _currentState)
			return;
		
		switch(state)
		{
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer state - "+state);
			break;
		}
		
		_currentState = state;
	}
}
