using UnityEngine;
using System.Collections;

public class OrderTable : MonoBehaviour 
{
	public tk2dSprite _background;
	public tk2dSprite _order1;
	
	private string _orderName;
	public string orderName
	{
		get 
		{
			return _orderName;
		}
	}
	
	void Awake()
	{
		hide();
	}
	
	public void show(string itemName)
	{
		_orderName = itemName;
		
		string spriteName = ResourcesDictionaty.instance.getIconName(_orderName);
		
		Debug.Log("sprite name - "+spriteName+" for item "+itemName);
		
		ContentManager.instance.configureObject(_order1, "Common/CommonIcons", spriteName);
		gameObject.SetActive(true);
	}
	
	public void hide()
	{
		gameObject.SetActive(false);
	}
}
