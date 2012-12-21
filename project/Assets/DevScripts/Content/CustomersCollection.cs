using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public enum CustomerType
{
	BUSINESS_WOMEN,
	BUSINESS_MEN,
	PRETTY_GIRL,
	SPORT_GUY,
	TEACHER_GIRL,
	BOOKWORM_GUY,
	HIPSTER_GIRL,
	HIPSTER_GUY
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
public class CustomerDesc
{
	public CustomerType type;
	
	#region common params
	public string[] interests;
	
	public int ordersCount;
	
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

public class CustomersCollection : MonoBehaviour 
{
	private static CustomersCollection _instance;
	public static CustomersCollection instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(CustomersCollection)) as CustomersCollection;
			}
			
			return _instance;
		}
	}
	
	public CustomerDesc[] descriptions;
	Dictionary<CustomerType, CustomerDesc> _customersDict = new Dictionary<CustomerType, CustomerDesc>();
	
	void Awake()
	{
		foreach(CustomerDesc desc in descriptions)
		{
			_customersDict.Add(desc.type, desc);
		}
	}
	
	public CustomerDesc getDesc(CustomerType type)
	{
		if (!_customersDict.ContainsKey(type))
		{
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer type - "+type);
			return null;
		}
		
		return _customersDict[type];
	}
}
