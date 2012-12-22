using UnityEngine;
using System.Collections;

public enum CustomerType
{
	CustomerType1,
	CustomerType2,
	CustomerType3,
	CustomerType4,
	CustomerType5,
	CustomerType6,
	CustomerType7,
	CustomerType8,
	CustomerType9,
	CustomerType10
}

public enum ReactionType
{
	NONE, 
	MOOD_UP,
	MOOD_UP_ALL,
	MOOD_DOWN,
	MOOD_DOWN_ALL
}

[System.Serializable]
public class CustomerStateDesc
{
	public CustomerState state;
	public string animName;
	
	public float randomAnimRangeMin;
	public float randomAnimRangeMax;
}

[System.Serializable]
public class CustomerDesc
{
	public CustomerType type;
	
	public CustomerStateDesc[] stateDescribers;
	
	#region common params
	public string[] interests;
	
	public int eatTime;	
	public int orderTime;
	public float moodDownTime;
	
	#endregion
	
	#region graphics
	public string spriteAtlas;
	public string animationAtlas;
	public string spriteName;
	
	public Vector2 seatOffset;
	#endregion
	
	#region behaviour
	public ReactionType pairAttempt;
	public ReactionType pairFail;
	public ReactionType pairSuccess;	
	#endregion	
	
	#region events
	#endregion
}