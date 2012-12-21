using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LevelDesc
{
	public string levelName;
	
	public int levelTime;
	
	ArrayList _customers;
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
		XmlNodeList levelsList = rootNode.SelectNodes("Levels/");
		Debug.Log("Levels count = "+levelsList.Count);
		foreach (XmlNode node in levelsList)
		{
		}
	}
}