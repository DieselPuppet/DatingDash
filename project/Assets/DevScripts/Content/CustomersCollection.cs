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

public class CustomerDesc
{
	#region common params
	public string[] interests;
	public string[] orders;
	
	public int orderTime;
	public int eatTime;
	public float moodDownTime;
	
	#endregion
	
	#region graphics
	public string spriteAtlas;
	public string animationAtlas;
	public string spriteName;
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
	
	Dictionary<CustomerType, CustomerDesc> _customersDict;
	
	public void parseDB(string fileName)
	{		
		XmlDocument doc = new XmlDocument();
		doc.Load(fileName);
		
		XmlNode rootNode = doc.FirstChild;
		XmlNodeList customers = rootNode.SelectNodes("customer");		
		
		foreach(XmlNode customerNode in customers)
		{
			CustomerDesc desc = new CustomerDesc();

			string type = customerNode.Attributes["type"].Value;
			string time = customerNode.Attributes["orderTime"].Value;
		}
	}
	
	public CustomerDesc getDesc(CustomerType type)
	{
		if (!_customersDict.ContainsKey(type))
		{
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer type - "+type);
		}
		
		return _customersDict[type];
	}
}
