using UnityEngine;
using System.Collections;

// storage for profiles and progress, settings etc
public class SaveManager : MonoBehaviour 
{
	private static SaveManager _instance;
	public static SaveManager instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(SaveManager)) as SaveManager;
			}
			
			return _instance;
		}
	}		
	
	public void load()
	{
		createEmpty();
	}
	
	void createEmpty()
	{
	}
}
