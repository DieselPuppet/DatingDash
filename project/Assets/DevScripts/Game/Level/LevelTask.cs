using UnityEngine;
using System.Collections;

// TODO : добавить сложные таски (определенные типы пар и т.д.)

public enum Task
{
	SCORES,
	PAIRS
}

public class LevelTask
{
	public LevelTask(Task t) 
	{
		_type = t;
	}
	
	private Task _type;
	public Task type
	{
		get 
		{
			return _type;
		}
	}
	
	public virtual bool isComplete(int comp) {return false;}
}

public class ScoresTask : LevelTask
{
	public ScoresTask(int scores) : base(Task.SCORES)
	{
		_requiredScores = scores;
	}
	
	private int _requiredScores;
	
	public override bool isComplete(int comp)
	{
		if (comp > _requiredScores)
			return true;
		
		return false;
	}
}

public class PairsTask : LevelTask
{
	public PairsTask(int pairs) : base(Task.PAIRS)
	{
		_requiredPairs = pairs;
	}	
	
	private int _requiredPairs;
	
	public override bool isComplete(int comp)
	{
		if (comp > _requiredPairs)
			return true;
		
		return false;
	}	
}