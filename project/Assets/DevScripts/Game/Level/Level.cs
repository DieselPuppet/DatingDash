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
				// refactor
				customer.transform.position = point.point.position;
				customer.GetComponent<Customer>().placement = point;
				point.isFree = false;
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
	
	ArrayList _seatings = new ArrayList();
	
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
	
	float _spawnInterval = 2;
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
		}
		
		PathGraph.instance.buildGraph(arragements[activeArarrangement].graph);
		
		_isComplete = false;
	}
	
	// move to courutine?
	public void process(GameMode mode)
	{	
		if (!_isComplete)
		{			
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
	
	void spawnCustomer(CustomerDesc desc)
	{
		GameObject customerGO = new GameObject();
		Customer customer = customerGO.AddComponent<Customer>();
		
		if (spawnArea.freePointExist())
		{
			spawnArea.placeCustomer(customerGO);
		
			customer.configure(desc);			
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
			CustomerDesc testDesc = CustomersCollection.instance.getDesc(CustomerType.BUSINESS_WOMEN);
			spawnCustomer(testDesc);
		}
	}	
}