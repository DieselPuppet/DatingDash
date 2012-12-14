using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// static class or move to GameplayManager?..
public class RoundStats
{
	public RoundStats()
	{
		_scores = 0;
		_lostClients = 0;
	}
	
	private int _scores;
	public int scores
	{
		get 
		{
			return _scores;
		}
	}
	
	public void adjustScores(int scores)
	{
		_scores = scores;
	}
	
	private int _lostClients;
	public int lostClients
	{
		get 
		{
			return _lostClients;
		}
	}
}

public class PlayerProfile
{
	public PlayerProfile() 
	{
		
	}
	
	private int _scores;
	public int scores
	{
		get 
		{
			return _scores;
		}
	}
	
 	public void adjustScores(int scores)
	{
		_scores += scores;
		if (_scores < 0)
			_scores = 0;
	}
}

public class GlobalProfile
{
	private int _earnedStarsCount;
	public int earnedStarsCount
	{
		get 
		{
			return _earnedStarsCount;
		}
	}
}

public class Player
{
	Dictionary<int, PlayerProfile> _profilesHistory;
	
	public Player()
	{
		_profilesHistory = new Dictionary<int, PlayerProfile>();
	}
	
	public PlayerProfile getProfile(int level)
	{
		if (!_profilesHistory.ContainsKey(level))
		{
			Logger.message(LogLevel.LOG_WARNING, "No such profile for level "+level.ToString());
		}
		
		return _profilesHistory[level];
	}
}