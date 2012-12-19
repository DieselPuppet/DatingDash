using UnityEngine;
using System.Collections;

public enum CustomerState
{
	WAITING_STAND,
	WAITING_STAND_ANGRY,
	WAITING_SEAT,
	
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
	
	int _currentMood;
	float _moodDownSpeed;
	float _moodDownSpeedCoeff;
	
	// timers
	float lastMoodChangeTime;
	
	private bool _isTouched = false;
	
	public void configure(CustomerDesc desc)
	{
		_desc = desc;
		
		_sprite = gameObject.AddComponent<tk2dAnimatedSprite>();
		ContentManager.instance.configureObject(_sprite, _desc.spriteAtlas, _desc.spriteName);
		ContentManager.instance.precacheAnimation(_sprite, _desc.animationAtlas);
		
		_currentMood = 100;
		_moodDownSpeed = 60f/desc.moodDownTime;
		
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
		
		if (state == CustomerState.WAITING_STAND)
		{
			_moodDownSpeedCoeff = 1f;
			lastMoodChangeTime = Time.time;
		}
		else if (state == CustomerState.WAITING_SEAT)
		{
			_moodDownSpeedCoeff = 0.5f;		
			lastMoodChangeTime = Time.time;	
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
		case CustomerState.WAITING_SEAT:
			if ((currentTime-lastMoodChangeTime) >= 1)
			{
				_currentMood -= (int)(_moodDownSpeed*_moodDownSpeedCoeff);
				lastMoodChangeTime = currentTime;
			}
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
				ChairItem chair = Level.instance.getNearestChair(Input.mousePosition);
				
				if (chair != null && chair.isFree)
				{
					/*if (!chair.isLeft)
					{
						gameObject.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
					}
					
					// replace this hell to method
					gameObject.transform.position = chair.gameObject.transform.position;
					seatPosition = chair;
					placement.isFree = true;
					chair.isFree = false;
					chair.customer = this;
					
					setState(CustomerStateOld.ORDER);*/
				}
				else 
				{
					//gameObject.transform.position = placement.point.position;
				}
				
				_isTouched = false;
			}
		}		
	}
}