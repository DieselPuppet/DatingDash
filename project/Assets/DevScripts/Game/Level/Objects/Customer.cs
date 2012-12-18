using UnityEngine;
using System.Collections;

public enum CustomerState
{
	WAITING,
	ORDER,
	WAIT_FOR_ORDER,
	BREAK,
	SIT_ANGRY,
	STAND_ANGRY
}

public class Interest
{	
	private string[] _interesrs;
	
	public Interest(string[] interests)
	{
		_interesrs = interests;
	}
	
	public int compare(Interest other)
	{
		int equalsLevel =0;
		return equalsLevel;
	}
}

[System.Serializable]
public class CustomerDesc
{
	public string name;
	
	public Action[] action;
	public Interest interests;
	
	public string[] orders;
	
	public int moodPercent;
	public int moodDownSpeed;
	
	// for launch rand animation in WaitingMode
	public int randAnimRangeMin;
	public int randAnimRangeMax;	
	
	public int orderTime;
	public int meatTime;
	
	public string spriteGroup;
	public string spriteName;
}

// set LevelItem as base class
public class Customer : MonoBehaviour 
{
	CustomerDesc _cachedDesc;
	
	int moodPercent;
	int moodDownSpeed;
	
	int randAnimRangeMin;
	int randAnimRangeMax;		
	
	int orderTime;
	int meatTime;	
	
	bool seatLeft;
	
	public SpawnPoint placement;
	// TODO
	//public setPlacement
	
	ChairItem seatPosition = null;
	
	private bool _isActive = false;
	public bool isActive
	{
		get 
		{
			return _isActive;
		}
	}
	
	private bool _isTouched = false;
	
	private tk2dAnimatedSprite _sprite;
	private tk2dAnimatedSprite _cloudSprite;
	
	private ArrayList _orders = new ArrayList();	
	public ArrayList orders
	{
		get 
		{
			return _orders;
		}
	}

	private Interest _interests;
	public Interest interests
	{
		get 
		{
			return _interests;
		}
	}
	
	private CustomerState _currentState;
	public CustomerState currentState
	{
		get
		{
			return _currentState;
		}
	}
	
	public void setState(CustomerState state)
	{
		Logger.message(LogLevel.LOG_INFO, "Customer state set to "+state.ToString());
	
		_currentState = state;
		
		Debug.Log("SetState - "+_currentState);
		
		switch(_currentState)
		{
		case CustomerState.WAITING:
			
			_isActive = true;
			
			_sprite.Play("hello");
			lastMoodChange = Time.time;
			break;
			
		case CustomerState.BREAK:
			if (seatPosition)
				seatPosition.isFree = true;
			
			_isActive = false;
			Level.instance.removeCustomer(this);
			break;
			
		case CustomerState.ORDER:
			_sprite.Play("sit_happy");
			
			collider.enabled = false;
			
			foreach(string order in _orders)
			{
				Inventory.instance.addOrder(new Order(order, this));
			}
			
			moodPercent = 100;
			seatTime = Time.time;
			break;
			
		case CustomerState.WAIT_FOR_ORDER:
			lastOrderTime = Time.time;
			_cloudSprite.gameObject.SetActive(true);
			break;
			
		case CustomerState.SIT_ANGRY:
			_cloudSprite.gameObject.SetActive(false);
			_sprite.Play("sit_angry");
			break;
			
		case CustomerState.STAND_ANGRY:
			_sprite.Play("stand_angry");
			break;
			
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer state - "+_currentState.ToString());
			
			break;
		}
	}
	
	public void configure(CustomerDesc desc)
	{		
		_cachedDesc = desc;
		
		moodPercent = desc.moodPercent;
		moodDownSpeed = desc.moodDownSpeed;
		
		randAnimRangeMin = desc.randAnimRangeMin;
		randAnimRangeMax = desc.randAnimRangeMax;
		
		orderTime = desc.orderTime;
		meatTime = desc.meatTime;
		
		foreach(string order in desc.orders)
		{
			_orders.Add(order);
		}
		
		_sprite = gameObject.AddComponent<tk2dAnimatedSprite>();
		ContentManager.instance.configureObject(_sprite, desc.spriteGroup, desc.spriteName);
		
		GameObject cloud = new GameObject("cloud");
		cloud.transform.parent = gameObject.transform;
		cloud.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
		
		cloud.SetActive(false);
		
		_cloudSprite = cloud.AddComponent<tk2dAnimatedSprite>();
		ContentManager.instance.configureObject(_cloudSprite, desc.spriteGroup, "cloud");
		
		BoxCollider box = gameObject.AddComponent<BoxCollider>();
		box.size = new Vector3(_sprite.GetBounds().size.x, _sprite.GetBounds().size.y, 1);		
		
		ContentManager.instance.precacheAnimation(_sprite, "Customer1Animation");
				
		setState(CustomerState.WAITING);
	}
	
	public void OnMouseDown()
	{		
		if (_currentState == CustomerState.WAITING || _currentState == CustomerState.STAND_ANGRY)
			_isTouched = true;
		else if (_currentState == CustomerState.WAIT_FOR_ORDER)
		{
			Debug.Log("Customers order :");
			
			foreach(string order in _orders)
			{
				Debug.Log(order);	
			}
		}
	}		
	
	float lastMoodChange;
	float lastOrderTime;
	float seatTime;
	
	public void think()
	{
		float currentTime = Time.time;
		
		switch (_currentState)
		{
		case CustomerState.WAITING:
			if ((currentTime - lastMoodChange) >= 1)
			{
				moodPercent -= moodDownSpeed;
				lastMoodChange = currentTime;
			}
			
			if (moodPercent < 50)
			{
				setState(CustomerState.STAND_ANGRY);
			}
			
			break;			
			
		case CustomerState.WAIT_FOR_ORDER:
	
			if ((currentTime - lastOrderTime) >= 1)
			{
				moodPercent -= moodDownSpeed;
				lastOrderTime = currentTime;
			}
			
			if (moodPercent < 50)
			{
				setState(CustomerState.SIT_ANGRY);
			}
			
			break;
				
		case CustomerState.SIT_ANGRY:
			if ((currentTime - lastMoodChange) >= 0.5f)
			{
				moodPercent -= moodDownSpeed;
				lastMoodChange = currentTime;
			}			
			
			if (moodPercent <= 0)
			{
				setState(CustomerState.BREAK);
			}			
			break;
			
		case CustomerState.STAND_ANGRY:
			if ((currentTime - lastMoodChange) >= 0.5f)
			{
				moodPercent -= moodDownSpeed;
				lastMoodChange = currentTime;
			}			
			
			if (moodPercent <= 0)
			{
				setState(CustomerState.BREAK);
			}			
			break;			
			
		case CustomerState.BREAK:
			break;
			
		case CustomerState.ORDER:
			if ((currentTime - seatTime) >= orderTime)
			{
				setState(CustomerState.WAIT_FOR_ORDER);
			}
			break;
			
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer state - "+_currentState.ToString());
			break;
		}
	}
	
	void FixedUpdate()
	{
		if (_isTouched)
		{
			gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z);
		
			if (Input.GetMouseButtonUp(0))
			{		
				ChairItem chair = Level.instance.getNearestChair(Input.mousePosition);
				
				if (chair != null && chair.isFree)
				{
					if (!chair.isLeft)
					{
						gameObject.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
					}
					
					gameObject.transform.position = chair.gameObject.transform.position;
					seatPosition = chair;
					placement.isFree = true;
					chair.isFree = false;
					
					setState(CustomerState.ORDER);
				}
				else 
				{
					gameObject.transform.position = placement.point.position;
				}
				
				_isTouched = false;
			}
		}
	}
}
