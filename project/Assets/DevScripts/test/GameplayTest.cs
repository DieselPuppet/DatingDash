using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayTest : MonoBehaviour 
{	
	ArrayList _actualOrders = new ArrayList();
	
	// Orders work
	void Awake()
	{
		_actualOrders.Add("orange");
		_actualOrders.Add("apple");
		_actualOrders.Add("fruit_cake");
	}
	
	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 100, 50), "Add Random order"))
		{
			int orderIndex = Random.Range(0, _actualOrders.Count);
		}
	}
}