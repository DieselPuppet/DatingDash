using UnityEngine;
using System.Collections;

public class SettingsManager : MonoBehaviour 
{
	private static SettingsManager _instance;
	public static SettingsManager instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(SettingsManager)) as SettingsManager;
			}
			
			return _instance;
		}
	}		
}
