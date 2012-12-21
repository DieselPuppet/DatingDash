using UnityEngine;
using System.Collections;

public class OrderTable : MonoBehaviour 
{
	public tk2dSprite _background;
	public tk2dSprite _order1;
	public tk2dSprite _order2;
	
	void Awake()
	{
		hide();
	}
	
	public void show(/*products*/)
	{
		gameObject.SetActive(true);
	}
	
	public void hide()
	{
		gameObject.SetActive(false);
	}
}
