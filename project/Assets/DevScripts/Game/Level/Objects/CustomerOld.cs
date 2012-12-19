using UnityEngine;
using System.Collections;

// Deprecated
public enum CustomerStateOld
{
	WAITING,
	ORDER,
	WAIT_FOR_ORDER,
	BREAK,
	SIT_ANGRY,
	STAND_ANGRY,
	EAT
}

// set LevelItem as base class
public class Customer : MonoBehaviour 
{
	CustomerDescOld _cachedDesc;
	
	int moodPercent;
	int moodDownSpeed;
	
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
	
	private CustomerStateOld _currentState;
	public CustomerStateOld currentState
	{
		get
		{
			return _currentState;
		}
	}
	
	public void setState(CustomerStateOld state)
	{
		Logger.message(LogLevel.LOG_INFO, "Customer state set to "+state.ToString());

		Debug.Log("SetState - "+state);
		
		switch(state)
		{
		case CustomerStateOld.WAITING:
			
			_isActive = true;
			
			_sprite.Play("hello");
			lastMoodChange = Time.time;
			break;
			
		case CustomerStateOld.BREAK:
			if (seatPosition)
				seatPosition.isFree = true;
			
			if (_currentState == CustomerStateOld.STAND_ANGRY)
				placement.isFree = true;
			
			_isActive = false;
			Level.instance.removeCustomer(this);
			break;
			
		case CustomerStateOld.ORDER:
			_sprite.Play("sit_happy");
			
			placement.isFree = true;
			collider.enabled = false;
			
			foreach(Order order in _orders)
			{
				Inventory.instance.addOrder(order);
			}
			
			moodPercent = 100;
			seatTime = Time.time;
			break;
			
		case CustomerStateOld.WAIT_FOR_ORDER:
			lastOrderTime = Time.time;
			
			_cloudSprite.gameObject.SetActive(true);
			break;
			
		case CustomerStateOld.SIT_ANGRY:
			_cloudSprite.gameObject.SetActive(false);
			_sprite.Play("sit_angry");
			break;
			
		case CustomerStateOld.STAND_ANGRY:
			_sprite.Play("stand_angry");
			break;
			
		case CustomerStateOld.EAT:
			_cloudSprite.gameObject.SetActive(false);
			_sprite.Play("sit_eat_drink");
			break;
			
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer state - "+_currentState.ToString());
			
			break;
		}
		
		_currentState = state;
	}
	
	public void configure(CustomerDescOld desc)
	{		
		_cachedDesc = desc;
		
		moodPercent = desc.moodPercent;
		moodDownSpeed = desc.moodDownSpeed;
		
		orderTime = desc.orderTime;
		meatTime = desc.meatTime;
		
		foreach(OrderItem order in desc.orders)
		{
			_orders.Add(new Order(order.ToString(), this));
		}
		
		_sprite = gameObject.AddComponent<tk2dAnimatedSprite>();
		ContentManager.instance.configureObject(_sprite, desc.spriteGroup, desc.spriteName);
		
		GameObject cloud = new GameObject("cloud");
		cloud.transform.parent = gameObject.transform;
		cloud.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -2);
		
		cloud.SetActive(false);
		
		_cloudSprite = cloud.AddComponent<tk2dAnimatedSprite>();
		ContentManager.instance.configureObject(_cloudSprite, desc.spriteGroup, "cloud");
		
		BoxCollider box = gameObject.AddComponent<BoxCollider>();
		box.size = new Vector3(_sprite.GetBounds().size.x, _sprite.GetBounds().size.y, 1);		
		
		ContentManager.instance.precacheAnimation(_sprite, "Customer1Animation");
				
		setState(CustomerStateOld.WAITING);
	}
	
	public void OnMouseDown()
	{		
		if (_currentState == CustomerStateOld.WAITING || _currentState == CustomerStateOld.STAND_ANGRY)
			_isTouched = true;
	}		
	
	float lastMoodChange;
	float lastOrderTime;
	float seatTime;
	
	public void think()
	{
		float currentTime = Time.time;
		
		switch (_currentState)
		{
		case CustomerStateOld.WAITING:
			if ((currentTime - lastMoodChange) >= 1)
			{
				moodPercent -= moodDownSpeed;
				lastMoodChange = currentTime;
			}
			
			if (moodPercent < 50)
			{
				setState(CustomerStateOld.STAND_ANGRY);
			}
			
			break;			
			
		case CustomerStateOld.WAIT_FOR_ORDER:
	
			if ((currentTime - lastOrderTime) >= 1)
			{
				moodPercent -= moodDownSpeed;
				lastOrderTime = currentTime;
			}
			
			if (moodPercent < 50)
			{
				setState(CustomerStateOld.SIT_ANGRY);
			}
			
			break;
				
		case CustomerStateOld.SIT_ANGRY:
			if ((currentTime - lastMoodChange) >= 0.5f)
			{
				moodPercent -= moodDownSpeed;
				lastMoodChange = currentTime;
			}			
			
			if (moodPercent <= 0)
			{
				setState(CustomerStateOld.BREAK);
			}			
			break;
			
		case CustomerStateOld.STAND_ANGRY:
			if ((currentTime - lastMoodChange) >= 0.5f)
			{
				moodPercent -= moodDownSpeed;
				lastMoodChange = currentTime;
			}			
			
			if (moodPercent <= 0)
			{
				setState(CustomerStateOld.BREAK);
			}			
			break;			
			
		case CustomerStateOld.EAT:	
		case CustomerStateOld.BREAK:
			break;
			
		case CustomerStateOld.ORDER:
			if ((currentTime - seatTime) >= orderTime)
			{
				setState(CustomerStateOld.WAIT_FOR_ORDER);
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
					
					// replace this hell to method
					gameObject.transform.position = chair.gameObject.transform.position;
					seatPosition = chair;
					placement.isFree = true;
					chair.isFree = false;
					chair.customer = this;
					
					setState(CustomerStateOld.ORDER);
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
