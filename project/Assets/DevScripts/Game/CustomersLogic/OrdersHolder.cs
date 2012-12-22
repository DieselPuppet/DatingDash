using UnityEngine;
using System.Collections;

public class OrdersHolder : MonoBehaviour 
{
	private static OrdersHolder _instance;
	public static OrdersHolder instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(OrdersHolder)) as OrdersHolder;
			}
			
			return _instance;
		}
	}
	
	ArrayList _orderTables = new ArrayList();
	
	// Order?..
	public void pushOrderTable(OrderTable table)
	{
		_orderTables.Add(table);
	}
}
