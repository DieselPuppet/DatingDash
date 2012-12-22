using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ProductIcon
{
	public ItemTypes product;
	public string spriteName;
}

public class ResourcesDictionaty : MonoBehaviour 
{
	private static ResourcesDictionaty _instance;
	public static ResourcesDictionaty instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(ResourcesDictionaty)) as ResourcesDictionaty;
			}
			
			return _instance;
		}
	}
	
	public ProductIcon[] icons;
	Dictionary<string, string> _icons = new Dictionary<string, string>();
	
	public void initData()
	{
		foreach(ProductIcon icon in icons)
		{
			_icons.Add(icon.product.ToString(), icon.spriteName);
		}
	}
	
	public string getIconName(string itemName)
	{
		if (!_icons.ContainsKey(itemName))
		{
			Logger.message(LogLevel.LOG_ERROR, "Icon for item "+itemName+" not found!");
			return _icons["UNKNOWN"];
		}
		
		return _icons[itemName];
	}
}
