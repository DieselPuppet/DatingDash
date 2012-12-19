using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ItemDesc
{
	public ItemDesc(string g="", string a="", string s="")
	{
		_actions = new ArrayList();
		
		_groupName = g;
		_atlasName = a;
		_spriteName = s;
	}
	
	private string _groupName;
	public string groupName
	{
		get 
		{
			return _groupName;
		}
	}
	
	private ArrayList _actions;
	public ArrayList actions
	{
		get 
		{
			return _actions;
		}
	}
	
	private string _atlasName;
	public string atlasName
	{
		get 
		{
			return _atlasName;
		}
	}
	
	private string _spriteName;
	public string spriteName
	{
		get 
		{
			return _spriteName;
		}
	}
	
	private Rect _colliderRect;
	public Rect colliderRect
	{
		get 
		{
			return _colliderRect;
		}
	}
}

public class ItemsCollection : MonoBehaviour 
{
	private static ItemsCollection _instance;
	public static ItemsCollection instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(ItemsCollection)) as ItemsCollection;
			}
			
			return _instance;
		}
	}	
	
	Dictionary<string, ItemDesc> _itemsDict;
	
	public void parseDB(string fileName)
	{
		_itemsDict = new Dictionary<string, ItemDesc>();
		
		XmlDocument doc = new XmlDocument();
		doc.Load(fileName);
		
		XmlNode rootNode = doc.FirstChild;
		XmlNodeList items = rootNode.SelectNodes("item");
		foreach (XmlNode item in items)
		{
			string typeName = item.Attributes["typeName"].Value;
			string groupName = item.Attributes["groupName"].Value;
			string atlasName = item.Attributes["assets"].Value;
			string spriteName = item.Attributes["sprite"].Value;
			
			XmlNodeList actionsList = item.SelectNodes("actions/action");
			foreach(XmlNode node in actionsList)
			{
				XmlNodeList componentsList = node.SelectNodes("components");
				foreach(XmlNode comp in componentsList)
				{
					string type = node.Attributes["type"].Value;
					string groupAsset = node.Attributes["group"].Value;
					string asset = node.Attributes["asset"].Value;
					
					if (type == "animation")
					{
						// precache atlases
					}					
				}
			}
			
			ItemDesc desc = new ItemDesc(groupName, atlasName, spriteName);
			_itemsDict.Add(typeName, desc);
		}
	}
	
	public ItemDesc getItemDesc(string typeName)
	{
		if (!_itemsDict.ContainsKey(typeName))
		{
			Logger.message(LogLevel.LOG_ERROR, "Unknown item type - "+typeName);
			return null;
		}
		
		return _itemsDict[typeName];
	}
	
	public TextAsset getCustomDesc(string typeName)
	{
		return new UnityEngine.TextAsset();
	}
}
