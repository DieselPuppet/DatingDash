using UnityEngine;
using System.Collections;
using System.Xml;

[System.Serializable]
public class SpawnPoint
{
	public Transform transform;
	
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
	
	public void placeCustomer(Customer customer)
	{
		foreach(SpawnPoint point in spawnPoints)
		{
			if (point.isFree)
			{
				customer.place(point);
				break;
			}
		}
	}
}

[System.Serializable]
public class TableArrangement
{
	public int count;
	public GameObject tableTemplate;
	public TextAsset graph;
	public Vector2[] positions;
}

public class Seating
{
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
		
	public string name;
	
	public Transform interactiveParent;
	
	public SpawnArea spawnArea;
	
	public TableArrangement[] arragements;	
	public int activeArarrangement;
	
	public SeatPlaceObject[] waitSeatingsRef;
	
	ArrayList _seatings = new ArrayList();
	ArrayList _waitSeatings = new ArrayList();
	
	ArrayList _customerQueue = new ArrayList();
	
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
	
	float _spawnDelay;
	float _lastSpawnTime;
	
	void Awake()
	{					
		for (int i = 0; i < arragements[activeArarrangement].count; i++)
		{
			GameObject table = (GameObject)Instantiate(arragements[activeArarrangement].tableTemplate, arragements[activeArarrangement].positions[i], Quaternion.identity);
			table.GetComponent<TableObject>().pointInGraph = "table0"+(i+1);
			table.transform.parent = interactiveParent;
			
			foreach (ChairObject chair in table.GetComponent<TableObject>().chairs)
			{
				_seatings.Add(chair);
			}
			
			foreach (SeatPlaceObject place in waitSeatingsRef)
			{
				_waitSeatings.Add(place);
			}
		}
		
		PathGraph.instance.buildGraph(arragements[activeArarrangement].graph);
		
		_isComplete = false;
	}
	
	LevelDesc _desc;
	
	public void configure(LevelDesc desc)
	{
		_desc = desc;
		
		CustomerSpawn cs = (CustomerSpawn)_desc.customersQueue[0];
		_spawnDelay = cs.delay;
		
		_lastSpawnTime = Time.time;
	}
	
	// move to courutine?
	public void process(GameMode mode)
	{	
		if (!_isComplete)
		{			
			float currentTime = Time.time;
			if ((currentTime - _lastSpawnTime) > _spawnDelay)
			{
				CustomerSpawn cs = (CustomerSpawn)_desc.customersQueue[0];
				spawnCustomer(CustomersCollection.instance.getDesc(cs.type), cs.orders);
				
				_desc.customersQueue.RemoveAt(0);
				
				if (_desc.customersQueue.Count > 0)
				{
					cs = (CustomerSpawn)_desc.customersQueue[0];
					_spawnDelay = cs.delay;
				}
				
				_lastSpawnTime = currentTime;
			}
			
			if (_customerQueue.Count > 0 && spawnArea.freePointExist())
			{
				Customer customer = (Customer)_customerQueue[0];
				customer.gameObject.SetActive(true);
				spawnArea.placeCustomer(customer);	
				
				_customerQueue.RemoveAt(0);
			}
			
			if (mode == GameMode.COMPANY && checkTasks())
			{
				_isComplete = true;
			}
		}
	}
	
	public void pushLevelObject(BaseObject obj)
	{
		_objectsArray.Add(obj);
	}
	
	public BaseObject getObject(ObjectType type)
	{
		foreach (BaseObject obj in _objectsArray)
		{
			if (obj.type == type)
				return obj;
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
	
	public ChairObject getNearestChair(Vector3 pos)
	{			
		foreach (ChairObject chair in _seatings)
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

	public SeatPlaceObject getNearestSeating(Vector3 pos)
	{
		foreach (SeatPlaceObject seat in _waitSeatings)
		{				
			if (pos.x > seat.gameObject.transform.position.x-seat.gameObject.collider.bounds.size.x/2 &&
				pos.x < seat.gameObject.transform.position.x+seat.gameObject.collider.bounds.size.x/2 &&
				pos.y > seat.gameObject.transform.position.y-seat.gameObject.collider.bounds.size.y/2 &&
				pos.y < seat.gameObject.transform.position.y+seat.gameObject.collider.bounds.size.y/2)
			{
				return seat;
			}
		}
		
		return null;		
	}
	
	void spawnCustomer(CustomerDesc desc, string[] orders)
	{		
		GameObject customerGO = new GameObject();
		//CustomerDeprecated customer = customerGO.AddComponent<CustomerDeprecated>();
		//customer.pushOrders(orders);
		//customer.configure(desc);	
		Customer customer = customerGO.AddComponent<Customer>();
		customer.configure(desc);
		
		if (spawnArea.freePointExist())
		{	
			spawnArea.placeCustomer(customer);	
		}
		else 
		{
			customerGO.SetActive(false);
			_customerQueue.Add(customer);
		}		
	}	
}