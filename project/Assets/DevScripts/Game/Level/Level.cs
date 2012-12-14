using UnityEngine;
using System.Collections;
using System.Xml;

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
	
	ArrayList _customerQueue = new ArrayList();
	
	ArrayList _npcArray = new ArrayList();
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
				// popup, finish
			}
		}
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
	
	void spawnNpc()
	{
		// _customerQueue.Add(new Customer);
		// build npc
		// addComponent<Customer>
	}
}