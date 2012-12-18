using UnityEngine;
using System.Collections;

// TODO : rewrite
public class Order
{	
	public Order(string p, Customer o)
	{
		_productID = p;
		_owner = o;
		
		_isComplete = false;
	}
	
	private string _productID;
	public string productID
	{
		get 
		{
			return _productID;
		}
	}
	
	private Customer _owner;
	public Customer owner
	{
		get 
		{
			return _owner;
		}
	}
	
	private bool _isComplete;
	public bool isComplete
	{
		get 
		{
			return _isComplete;
		}
	}
	
	public void complete()
	{
		_isComplete = true;
	}
}

public class Inventory : MonoBehaviour
{
	private static Inventory _instance;
	public static Inventory instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(Inventory)) as Inventory;
			}
			
			return _instance;
		}
	}		
	
	private ArrayList _stuffArray = new ArrayList();
	private ArrayList _ordersArray = new ArrayList();
	
	int _capacity;
	
	void Awake()
	{		
		_capacity = 3;
	}
	
	public void addOrder(Order order)
	{
		_ordersArray.Add(order);
	}
	
	/*public void finishOrder(string orderId)
	{		
		foreach (Order order in _ordersArray)
		{
			if (order.productID == orderId)
			{
				_ordersArray.Remove(order);	
			}
		}	
	}*/
	
	public bool canCompleteOrder(string order)
	{
		if (order.Contains("+"))
		{	
			bool canComplete = true;
			
			string[] orderParts = order.Split('+');
			foreach (string part in orderParts)
			{
				if (!_stuffArray.Contains(part))
					canComplete = false;
			}
			
			return canComplete;
		}
		
		return _stuffArray.Contains(order);
	}
	
	public string higherPriority(string order1, string order2)
	{
		if (_stuffArray.IndexOf(order1) < _stuffArray.IndexOf(order2))
		{
			return order1;
		}
		
		return order2;
	}
	
	public void addStuf(string stuffId)
	{		
		if (_stuffArray.Count < _capacity)
		{
			Debug.Log(stuffId);
			_stuffArray.Add(stuffId);
		}
		else 
			Logger.message(LogLevel.LOG_WARNING, "Stuff array is already full!");
	}
	
	public void removeStuff(string stuffId)
	{		
		if (_stuffArray.Contains(stuffId))
		{
			_stuffArray.Remove(stuffId);
		}
		else 
			Logger.message(LogLevel.LOG_ERROR, "StuffArray do not contains "+stuffId);
	}
	
	public void removeAllStuff()
	{
		_stuffArray.Clear();
	}
	
	public bool canAddStuff()
	{
		return _stuffArray.Count < _capacity;
	}
	
	public bool hasStuff(string stuffId)
	{
		return _stuffArray.Contains(stuffId);
	}	
}
