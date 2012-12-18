using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerDesc
{
	public string name;
	
	public Action[] action;
	public Interest interests;
	
	public OrderItem[] orders;
	
	public int moodPercent;
	public int moodDownSpeed;
	
	public int randAnimRangeMin;
	public int randAnimRangeMax;	
	
	public int orderTime;
	public int meatTime;
	
	public string spriteGroup;
	public string spriteName;
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
	
	Dictionary<string, CustomerDesc> _customersDict;
	
	public void parseDB(TextAsset sources)
	{
	}
}
