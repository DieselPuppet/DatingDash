using UnityEngine;
using System.Collections;
using System.Xml;

[System.Serializable]
public class SpawnPoint
{
	public Transform point;
	
	[System.NonSerialized] 
	public bool isFree = true;
}

[System.Serializable]
public class SpawnArea
{
	public SpawnPoint[] spawnPoints;
	
	public bool freePointExist()
	{
		foreach (SpawnPoint point in spawnPoints)
		{
			if (point.isFree)
				return true;
		}
		
		return false;
	}
	
	public void placeCustomer(GameObject customer)
	{
		foreach(SpawnPoint point in spawnPoints)
		{
			if (point.isFree)
			{
				customer.transform.position = point.point.position;
				customer.GetComponent<Customer>().placement = point;
				point.isFree = false;
				break;
			}
		}
	}
}

public class Level : MonoBehaviour
{	
	private static Level _instance;
	public static Level instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(Level)) as Level;
			}
			
			return _instance;
		}
	}		
	
	public TextAsset graphAsset;
	
	public SpawnArea spawnArea;
	
	ArrayList seatPositions = new ArrayList();
	
	ArrayList _customerQueue = new ArrayList();
	
	ArrayList _customerArray = new ArrayList();
	ArrayList _customersDeleteQueue = new ArrayList();
	
	ArrayList _objectsArray = new ArrayList();
	
	ArrayList _tasks = new ArrayList();
	
	bool _isComplete = false;
	public bool isComplete
	{
		get 
		{
			return _isComplete;
		}
	}
	
	float _spawnInterval = 2;
	float _lastSpawnTime;
	
	void Awake()
	{		
		PathGraph.instance.buildGraph(graphAsset);
		
		// TODO : remove! Place this in level parsing
		foreach (ChairItem chair in gameObject.GetComponentsInChildren<ChairItem>())
		{
			seatPositions.Add(chair);
		}
		
		_isComplete = false;
	}
	
	// move to courutine?
	public void process(GameMode mode)
	{	
		if (!_isComplete)
		{
			foreach(Customer cust in _customerArray)
			{
				if (cust.isActive)
					cust.think();
			}
			
			foreach(Customer c in _customersDeleteQueue)
			{
				if (_customerArray.Contains(c))
				{
					Destroy(c.gameObject);
					_customerArray.Remove(c);
				}
			}
			
			float currentTime = Time.time;
			if ((currentTime - _lastSpawnTime) > _spawnInterval)
			{
				//spawnNpc();
				_lastSpawnTime = currentTime;
			}
			
			if (mode == GameMode.COMPANY && checkTasks())
			{
				_isComplete = true;
			}
		}
	}
	
	public void removeCustomer(Customer cust)
	{
		_customersDeleteQueue.Add(cust);
	}
	
	public void pushLevelObject(LevelItem obj)
	{
		_objectsArray.Add(obj);
	}
	
	public LevelItem getObject(string obj)
	{
		foreach(LevelItem item in _objectsArray)
		{
			if (item.name == obj)
				return item;
		}
		
		return null;
	}
	
	public void pushTask(LevelTask task)
	{
		_tasks.Add(task);
	}
	
	bool checkTasks()
	{
		return false;
	}
	
	public ChairItem getNearestChair(Vector3 pos)
	{		
		foreach (ChairItem chair in seatPositions)
		{			
			if (pos.x > chair.gameObject.transform.position.x-chair.gameObject.collider.bounds.size.x/2 &&
				pos.x < chair.gameObject.transform.position.x+chair.gameObject.collider.bounds.size.x/2 &&
				pos.y > chair.gameObject.transform.position.y-chair.gameObject.collider.bounds.size.y/2 &&
				pos.y < chair.gameObject.transform.position.y+chair.gameObject.collider.bounds.size.y/2)
			{
				return chair;
			}
		}
		
		return null;
	}
	
	void spawnNpc()
	{
		GameObject customerGO = new GameObject();
				
		Customer customer = customerGO.AddComponent<Customer>();
		
		if (spawnArea.freePointExist())
		{
			spawnArea.placeCustomer(customerGO);
			
			_customerArray.Add(customer);
		
			customer.configure(Balance.instance.getRandomDesc());			
		}
		else 
		{
			customerGO.SetActive(false);
			_customerQueue.Add(customer);
		}
	}
	
	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 100, 50), "SpawnNPC"))
		{
			spawnNpc();
		}
	}	
}