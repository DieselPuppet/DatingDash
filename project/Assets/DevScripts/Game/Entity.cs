using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum NPC_STATE
{
	INITIAL,
	WAITING,
	WAITING_ORDER,
	WAITING_CALC,
	WAITING_IN_ZONE, // ?
	SITTING,
	SPEAKING,
	MEAL,
	HAPPY,
	ANGRY
}

public class Interest
{	
	private string[] _interesrs;
	
	public Interest(string[] interests)
	{
		_interesrs = interests;
	}
	
	public bool compare(Interest other)
	{
		return true;
	}
}

public class NPC
{	
	private NPC_STATE _currentState;
	public NPC_STATE currentState
	{
		get 
		{
			return _currentState;
		}
	}
	
	private int _maxMood;
	private int _moodPercent;
	public int moodPercent
	{
		get 
		{
			return _moodPercent;
		}
	}	
	
	private int _moodDownSpeed;
	public int moodDownSpeed
	{
		get
		{
			return _moodDownSpeed;
		}
	}
	
	private Interest _interest;
	public Interest interests
	{
		get 
		{
			return _interest;
		}
	}
	
	float lastTime;
	
	public NPC(NPCDesc desc)
	{
		_maxMood = desc.moodPercent;
		_moodPercent = desc.moodPercent;
		
		_moodDownSpeed = desc.moodDownSpeed;
		_moodDownSpeed = 10;
		
		_interest = desc.interests;
		
		_currentState = NPC_STATE.INITIAL;
	}
	
	public void setState(NPC_STATE state)
	{
		if (_currentState != state)
		{
			Logger.message(LogLevel.LOG_INFO, "NPC.setState "+state);
			_currentState = state;
		}
		
		lastTime = Time.time;
	}
	
	/*public void seatTo(LevelObject obj)
	{
		if (obj.type == ObjectType.TABLE)
		{
			// check empty table, mood etc
		}
		else // if divan 
		{
		}
	}*/
	
	public void adjustMood(int mood)
	{
		_moodPercent += mood;
		
		if (_moodPercent > _maxMood)
		{
			_moodPercent = _maxMood;
		}
		else if (_moodPercent <= 0)
		{
			// PLayerProfile.adjustScores(-Balance.NPC_LEAVE_SCORES);	
		}
	}

	public void think()
	{		
		float currentTime = Time.time;
		
		switch(_currentState)
		{
		case NPC_STATE.INITIAL:
			break;
			
		case NPC_STATE.ANGRY:
			break;
			
		case NPC_STATE.MEAL:
			break;
			
		case NPC_STATE.SITTING:
			break;
			
		case NPC_STATE.SPEAKING:
			break;
			
		case NPC_STATE.WAITING:
			if ((currentTime-lastTime) >= 1)
			{
				_moodPercent-=_moodDownSpeed;
				lastTime = Time.time;
				
				if (_moodPercent <= 0)
				{
					setState(NPC_STATE.ANGRY);
				}
			}
			break;
			
		case NPC_STATE.WAITING_IN_ZONE:
			if ((currentTime-lastTime) >= 1)
			{
				_moodPercent-=_moodDownSpeed;
				lastTime = Time.time;
				
				if (_moodPercent <= 0)
				{
					setState(NPC_STATE.ANGRY);
				}
			}
			break;			
			
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown NPC state - "+_currentState.ToString());
			break;
		}
	}
	
	public float getCoeffWishes(NPC compareNPC)
	{
		if (_interest.compare(compareNPC.interests))
			return 100500f;
		
		return 0f;
	}
}
