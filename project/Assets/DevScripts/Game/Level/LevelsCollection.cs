using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class CustomerSpawn
{
	public string type;
	public float delay;
	
	public string[] orders;
}

public class LevelDesc
{
	public string levelName;
	
	public int levelTime;
	
	public ArrayList customersQueue = new ArrayList();
}

public class LevelsCollection : MonoBehaviour
{	
	private static LevelsCollection _instance;
	public static LevelsCollection instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(LevelsCollection)) as LevelsCollection;
			}
			
			return _instance;
		}
	}		
	
	Dictionary<string, LevelDesc> _levelsDict = new Dictionary<string, LevelDesc>();
	
	public void parse(TextAsset sources)
	{
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(sources.text);
		
		XmlNode rootNode = doc.FirstChild;
		XmlNode levels = doc.SelectSingleNode("Levels");
		
		XmlNodeList levelsList = levels.ChildNodes;
		foreach (XmlNode node in levelsList)
		{
			LevelDesc desc = new LevelDesc();
			string name = node.Name;
			
			XmlNodeList linesList = node.SelectNodes("Lines/SpawnLine"); 
			foreach(XmlNode line in linesList)
			{
				foreach(XmlNode customer in line.ChildNodes)
				{
					CustomerSpawn cs = new CustomerSpawn();
					cs.delay = float.Parse(customer.Attributes["Delay"].Value);
					cs.type = customer.Name;
					
					int ordersCount = customer.SelectNodes("Order").Count;
					cs.orders = new string[ordersCount];
					
					int i = 0;
					foreach(XmlNode order in customer.SelectNodes("Order"))
					{
						cs.orders[i++] = order.Attributes["Name"].Value;	
					}	
					
					desc.customersQueue.Add(cs);
				}
			}
			
			_levelsDict.Add(name, desc);
		}
	}
	
	public LevelDesc getDesc(string name)
	{
		if (!_levelsDict.ContainsKey(name))
		{
			Logger.message(LogLevel.LOG_ERROR, "LevelDesc not found - "+name);
			return null;
		}
		else 
		{
			return _levelsDict[name];
		}
	}
}