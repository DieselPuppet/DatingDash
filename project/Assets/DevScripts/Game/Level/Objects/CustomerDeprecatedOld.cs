using UnityEngine;
using System.Collections;

public enum CustomerStateDeprecated
{
	WAITING_STAND,
	WAITING_STAND_ANGRY,
	WAITING_SIT,
	MAKE_ORDER,
	WAITING_ORDER,
	WAITING_ORDER_ANGRY,
	
	EAT,
	
	HAPPY_OUT,
	
	UNKNOWN
}

// TODO : add Proxies (see Drafts.cs)
public class CustomerDeprecated : MonoBehaviour
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
	private CustomerStateDeprecated _currentState = CustomerStateDeprecated.UNKNOWN;
	public CustomerStateDeprecated currentState
	{
		get 
		{
			return _currentState;
		}
	}
	#endregion
	
	tk2dAnimatedSprite _sprite;
	
	OrderTable _orderTable;
	
	public ArrayList orders = new ArrayList();
	
	int _currentMood;
	float _moodDownSpeed;
	float _moodDownSpeedCoeff;
	
	// timers
	float lastMoodChangeTime;
	float startOrderTime;
	float startEat;
	
	private bool _isTouched = false;
	
	public ChairObject seatPosition = null;
	public SeatPlaceObject waitSeatPosition = null;
	public SpawnPoint placement = null;
	
	public void configure(CustomerDesc desc)
	{
		_desc = desc;
		
		_sprite = gameObject.AddComponent<tk2dAnimatedSprite>();
		ContentManager.instance.configureObject(_sprite, _desc.spriteAtlas, _desc.spriteName);
		Debug.Log("Old animation atlas - "+_desc.animationAtlas);
		ContentManager.instance.precacheAnimation(_sprite, _desc.animationAtlas);
		
		_currentMood = 100;
		_moodDownSpeed = 60f/desc.moodDownTime;

		GameObject tableGO = (GameObject)Instantiate((GameObject)Resources.Load("Prefabs/OrderTable"), Vector3.zero, Quaternion.identity);
		
		_orderTable = tableGO.GetComponent<OrderTable>();
		
		BoxCollider box = gameObject.AddComponent<BoxCollider>();
		box.size = new Vector3(_sprite.GetBounds().size.x, _sprite.GetBounds().size.y, 1);		
		
		_sprite.Play("hello");
		
		setState(CustomerStateDeprecated.WAITING_STAND);
	}
	
	//TMP!!!
	public void pushOrders(string[] o)
	{
		foreach(string order in o)
		{
			orders.Add(new Order(order, this));
		}
	}
	
	public void OnMouseDown()
	{		
		if (_currentState == CustomerStateDeprecated.WAITING_STAND || _currentState == CustomerStateDeprecated.WAITING_SIT)
			_isTouched = true;
	}		
	
	public void setState(CustomerStateDeprecated state)
	{
		if (state == _currentState)
			return;
		
		switch(state)
		{
		case CustomerStateDeprecated.WAITING_STAND:
			_moodDownSpeedCoeff = 1f;
			lastMoodChangeTime = Time.time;
			break;
			
		case CustomerStateDeprecated.WAITING_SIT:
			placement.isFree = true;
			_moodDownSpeedCoeff = 0.5f;		
			lastMoodChangeTime = Time.time;		
			break;
				
		case CustomerStateDeprecated.MAKE_ORDER:
			collider.enabled = false;
			_sprite.Play("sit_happy");
			startOrderTime = Time.time;
			break;
		
		case CustomerStateDeprecated.EAT:
			_orderTable.hide();
			startEat = Time.time;
			_sprite.Play("sit_eat_drink");
			break;
			
		case CustomerStateDeprecated.WAITING_ORDER:
			
			// HACK
			_orderTable.gameObject.transform.position = gameObject.transform.position;
			_orderTable.gameObject.transform.Translate(0, 0, -2);
			
			Order ord = (Order)orders[0];
			_orderTable.show(ord.productID);
			
			_moodDownSpeedCoeff = 0.5f;	
			lastMoodChangeTime = Time.time;	
			break;			
			
		case CustomerStateDeprecated.HAPPY_OUT:
			_sprite.Play("hello");

			seatPosition.isFree = true;
			Destroy(gameObject, 2);
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
		case CustomerStateDeprecated.UNKNOWN:
			break;
			
		case CustomerStateDeprecated.HAPPY_OUT:
			break;
			
		case CustomerStateDeprecated.EAT:
			if ((currentTime - startEat) >= _desc.eatTime)
			{
				setState(CustomerStateDeprecated.HAPPY_OUT);
			}
			break;
			
		case CustomerStateDeprecated.WAITING_STAND:
		case CustomerStateDeprecated.WAITING_SIT:
			if ((currentTime-lastMoodChangeTime) >= 1)
			{
				_currentMood -= (int)(_moodDownSpeed*_moodDownSpeedCoeff);
				lastMoodChangeTime = currentTime;
			}
			
			if (_currentMood <= 0)
			{
				if (_currentState == CustomerStateDeprecated.WAITING_STAND)
					_sprite.Play("stand_angry");
				else 
					_sprite.Play("sit_engry");
				
				Destroy(gameObject, 1);
			}
			
			break;		
			
		case CustomerStateDeprecated.MAKE_ORDER:
			if ((currentTime - startOrderTime) > _desc.orderTime)
			{
				foreach (Order o in orders)
				{
					Inventory.instance.addOrder(o);
				}
				
				setState(CustomerStateDeprecated.WAITING_ORDER);
			}
			break;
			
		case CustomerStateDeprecated.WAITING_ORDER:
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
		
			if (Level.instance.getNearestChair(Input.mousePosition) != null)
			{
				if (!Level.instance.getNearestChair(Input.mousePosition).isLeft)
					gameObject.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
				else 
					gameObject.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
				
				_sprite.Play("sit_happy");
			}
			else if(Level.instance.getNearestSeating(Input.mousePosition) != null)
			{
				if (!Level.instance.getNearestSeating(Input.mousePosition).isLeft)
					gameObject.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
				else 
					gameObject.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
				
				_sprite.Play("sit_happy");				
			}
			else
			{
				_sprite.Play("hello");	
			}
			
			if (Input.GetMouseButtonUp(0))
			{		
				if (Level.instance.getNearestChair(Input.mousePosition) != null)
				{
					ChairObject chair = Level.instance.getNearestChair(Input.mousePosition);
					
					if (chair.isFree)
					{	
						if (!chair.isLeft)
						{
							gameObject.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
						}
						
						// replace this hell to method
						gameObject.transform.position = new Vector3(chair.gameObject.transform.position.x+_desc.seatOffset.x, chair.gameObject.transform.position.y+_desc.seatOffset.y, -1);
						seatPosition = chair;
						placement.isFree = true;
						if (waitSeatPosition)
							waitSeatPosition.isFree = true;
						chair.isFree = false;
						chair.customer = this;
						
						setState(CustomerStateDeprecated.MAKE_ORDER);
					}
				}
				else if (Level.instance.getNearestSeating(Input.mousePosition) != null)
				{
					SeatPlaceObject seat = Level.instance.getNearestSeating(Input.mousePosition);
					
					if (seat.isFree)
					{
						if (!seat.isLeft)
						{
							gameObject.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
						}
						
						// replace this hell to method
						gameObject.transform.position = new Vector3(seat.gameObject.transform.position.x+_desc.seatOffset.x, seat.gameObject.transform.position.y+_desc.seatOffset.y, -1);
						waitSeatPosition = seat;
						placement.isFree = true;
						seat.isFree = false;
						seat.customer = this;
						
						setState(CustomerStateDeprecated.WAITING_SIT);					
					}
				}
				else 
				{
					gameObject.transform.position = placement.transform.position;
				}
				
				_isTouched = false;
			}
		}		
	}
}