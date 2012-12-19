using UnityEngine;
using System.Collections;

public class Config : MonoBehaviour 
{
	private static Config _instance;
	public static Config instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(Config)) as Config;
			}
			
			return _instance;
		}
	}
	
	public bool isDevelopmentBuild;
	
	public string dataPath;
	
	public string itemsDB;
	public string customersDB;
}
