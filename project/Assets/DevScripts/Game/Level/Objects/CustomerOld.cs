using UnityEngine;
using System.Collections;

public enum OrderProducts
{
	COFFEE_SMALL,
	COFFEE_BIG,
	ORANGE_JUCE,
	PIPES1,
	PIPES2,
	APPLE_JUCE,
	FRUIT_CAKE,
	CAKE,
	
	UNKNOWN
}

public enum CustomerState
{
	WAITING_STAND,
	WAITING_STAND_ANGRY,
	WAITING_SIT,
	MAKE_ORDER,
	WAITING_ORDER,
	
	UNKNOWN
}

// TODO : add Proxies (see Drafts.cs)
public class Customer : MonoBehaviour
{
	#region CustomerDesc
	private CustomerDesc _desc;
	public CustomerDesc desc
	{
		get
		{
			return _desc;
		}
	}
	#endregion
	
	#region CustomerState
	private CustomerState _currentState = CustomerState.UNKNOWN;
	public CustomerState currentState
	{
		get 
		{
			return _currentState;
		}
	}
	#endregion
	
	tk2dAnimatedSprite _sprite;
	
	public ArrayList orders = new ArrayList();
	
	int _currentMood;
	float _moodDownSpeed;
	float _moodDownSpeedCoeff;
	
	// timers
	float lastMoodChangeTime;
	float startOrderTime;
	
	private bool _isTouched = false;
	
	public ChairObject seatPosition = null;
	public SpawnPoint placement = null;
	
	public void configure(CustomerDesc desc)
	{
		_desc = desc;
		
		_sprite = gameObject.AddComponent<tk2dAnimatedSprite>();
		ContentManager.instance.configureObject(_sprite, _desc.spriteAtlas, _desc.spriteName);
		ContentManager.instance.precacheAnimation(_sprite, _desc.animationAtlas);
		
		_currentMood = 100;
		_moodDownSpeed = 60f/desc.moodDownTime;
		
		orders.Add(new Order(OrderProducts.APPLE_JUCE.ToString()+OrderProducts.PIPES1.ToString(), this));
		orders.Add(new Order(OrderProducts.FRUIT_CAKE.ToString(), this));
		
		BoxCollider box = gameObject.AddComponent<BoxCollider>();
		box.size = new Vector3(_sprite.GetBounds().size.x, _sprite.GetBounds().size.y, 1);		
		
		setState(CustomerState.WAITING_STAND);
	}
	
	public void OnMouseDown()
	{		
		if (_currentState == CustomerState.WAITING_STAND)
			_isTouched = true;
	}		
	
	void setState(CustomerState state)
	{
		if (state == _currentState)
			return;
	
		Debug.Log("Set customer state - "+state);
		
		switch(state)
		{
		case CustomerState.WAITING_STAND:
			_moodDownSpeedCoeff = 1f;
			lastMoodChangeTime = Time.time;
			break;
			
		case CustomerState.WAITING_SIT:
			collider.enabled = false;
			_moodDownSpeedCoeff = 0.5f;		
			lastMoodChangeTime = Time.time;		
			break;
				
		case CustomerState.MAKE_ORDER:
			_sprite.Play("sit_happy");
			startOrderTime = Time.time;
			break;
		
		case CustomerState.WAITING_ORDER:
			break;			
			
		default:
			Logger.message(LogLevel.LOG_ERROR, "Unknown customer state - "+state);
			break;
		}
			
		_currentState = state;
	}
	
	void FixedUpdate()
	{
		float currentTime = Time.time;
		
		switch(_currentState)
		{
		case CustomerState.UNKNOWN:
			break;
			
		case CustomerState.WAITING_STAND:
		case CustomerState.WAITING_SIT:
			if ((currentTime-lastMoodChangeTime) >= 1)
			{
				_currentMood -= (int)(_moodDownSpeed*_moodDownSpeedCoeff);
				lastMoodChangeTime = currentTime;
			}
			break;		
			
		case CustomerState.MAKE_ORDER:
			if ((currentTime - startOrderTime) > _desc.orderTime)
			{
				//foreach (string o in _desc.orders)
				//{
				//	Inventory.instance.addOrder(new Order(o, this));
				//}
				
				setState(CustomerState.WAITING_ORDER);
			}
			break;
			
		case CustomerState.WAITING_ORDER:
			break;
			
		default:
			Logger.message(LogLevel.LOG_ERROR, "Invalid customer state - "+_currentState);
			break;
		}
		
		if (_isTouched)
			updateInput();
	}
	
	void updateInput()
	{
		if (_isTouched)
		{
			gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z);
		
			if (Input.GetMouseButtonUp(0))
			{		
				ChairObject chair = Level.instance.getNearestChair(Input.mousePosition);
				
				if (chair != null && chair.isFree)
				{
					if (!chair.isLeft)
					{
						gameObject.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
					}
					
					// replace this hell to method
					gameObject.transform.position = new Vector3(chair.gameObject.transform.position.x+_desc.seatOffset.x, chair.gameObject.transform.position.y+_desc.seatOffset.y, -1);
					seatPosition = chair;
					placement.isFree = true;
					chair.isFree = false;
					chair.customer = this;
					
					setState(CustomerState.MAKE_ORDER);
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