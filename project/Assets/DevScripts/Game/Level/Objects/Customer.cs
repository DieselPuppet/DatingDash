using UnityEngine;
using System.Collections;

public enum CustomerState
{
	WAITING,
	ORDER,
	BREAK
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
		int equalsCount =0;
		return equalsCount;
	}
}

// rename..
public class CustomerDesc
{
	public Action[] action;
	public Interest interests;
	
	public int moodPercent = 100;
	public int moodDownSpeed = 25;
	
	// for launch rand animation in WaitingMode
	public int randAnimRangeMin;
	public int randAnimRangeMax;	
	
	public int orderTime = 5;
	public int meatTime = 5;
	
	public string spriteGroup = "Customers/Customer1";
	public string spriteName = "stand_hello001";
}

// set LevelItem as base class
public class Customer : MonoBehaviour 
{
	ArrayList _orders = new ArrayList();
	
	int moodPercent;
	int moodDownSpeed;
	
	int randAnimRangeMin;
	int randAnimRangeMax;		
	
	int orderTime;
	int meatTime;	
	
	private bool _isActive = false;
	public bool isActive
	{
		get 
		{
			return _isActive;
		}
	}
	
	public void setActive(bool flag)
	{
		_isActive = flag;
	}
	
	private tk2dAnimatedSprite _sprite;
	
	Order[] orders;	

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
	
	public void moveTo(Vector2 pos)
	{
		gameObject.transform.position = new Vector3(pos.x, pos.y, gameObject.transform.position.z);
	}
	
	public void setState(CustomerState state)
	{
		Logger.message(LogLevel.LOG_INFO, "Customer state set to "+state.ToString());
		
		_currentState = state;
		
		switch(_currentState)
		{
		case CustomerState.WAITING:
			lastMoodChange = Time.time;
			
			break;
			
		case CustomerState.BREAK:
		case CustomerState.ORDER:
			break;
			
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer state - "+_currentState.ToString());
			
			break;
		}
	}
	
	public void configure(CustomerDesc desc)
	{		
		moodPercent = desc.moodPercent;
		moodDownSpeed = desc.moodDownSpeed;
		
		randAnimRangeMin = desc.randAnimRangeMin;
		randAnimRangeMax = desc.randAnimRangeMax;
		
		orderTime = desc.orderTime;
		meatTime = desc.meatTime;
		
		_sprite = gameObject.AddComponent<tk2dAnimatedSprite>();
	
		ContentManager.instance.configureObject(_sprite, desc.spriteGroup, desc.spriteName);
		
		BoxCollider box = gameObject.AddComponent<BoxCollider>();
		box.size = new Vector3(_sprite.GetBounds().size.x, _sprite.GetBounds().size.y, 1);		
		
		_orders.Add("orange");
		_orders.Add("fruit_cake");
	}
	
	public void OnMouseDown()
	{
		Debug.Log("Customer select");
		
		_isActive = true;
	}		
	
	float lastMoodChange;
	
	public void think()
	{
		switch (_currentState)
		{
		case CustomerState.WAITING:
			float currentTime = Time.time;
			
			if ((currentTime - lastMoodChange) >= 1)
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
		case CustomerState.ORDER:
			break;
			
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer state - "+_currentState.ToString());
			break;
		}
	}
}
