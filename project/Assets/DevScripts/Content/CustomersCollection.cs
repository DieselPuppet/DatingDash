using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// TMP!
public class OrdersGraphics
{
	private static Dictionary<string, string> _orderSprites = new Dictionary<string, string>();
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
	Dictionary<string, CustomerDesc> _customersDict = new Dictionary<string, CustomerDesc>();
	
	void Awake()
	{
		foreach(CustomerDesc desc in descriptions)
		{
			_customersDict.Add(desc.type.ToString(), desc);
		}
	}
	
	public CustomerDesc getDesc(string type)
	{
		if (!_customersDict.ContainsKey(type))
		{
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer type - "+type);
			return null;
		}
		
		return _customersDict[type];
	}
}
