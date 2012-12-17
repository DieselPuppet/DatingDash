using UnityEngine;
using System.Collections;

public class Order
{	
	public Order(string p, Customer o)
	{
		_productID = p;
		_owner = o;
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
		// tmp value
		_capacity = 2;
	}
	
	public void addOrder(Order order)
	{
		_ordersArray.Add(order);
	}
	
	// TODO: add more params for check ind order (owner e.g.)!!!!
	public void finishOrder(string orderId)
	{
		bool finishSuccess = false;
		
		foreach (Order order in _ordersArray)
		{
			if (order.productID == orderId)
			{
				finishSuccess = true;
				_ordersArray.Remove(order);	
			}
		}	
	}
	
	public bool hasOrder(string orderId)
	{
		foreach (Order order in _ordersArray)
		{
			if (order.productID == orderId)
				return true;
		}
		
		return false;
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
			_stuffArray.Add(stuffId);
		}
		else 
			Logger.message(LogLevel.LOG_WARNING, "Stuff array is already full!");
	}
	
	public void removeStuff(string stuffId)
	{
		// TODO : check exists orders
		
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
