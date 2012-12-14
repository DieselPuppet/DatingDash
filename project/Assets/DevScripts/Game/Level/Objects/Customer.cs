using UnityEngine;
using System.Collections;

public enum CustomerState
{
	
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
	
	public int moodPercent;
	public int moodDownSpeed;
	
	// for launch rand animation in WaitingMode
	public int randAnimRangeMin;
	public int randAnimRangeMax;	
	
	public int orderTime;
	public int meatTime;
	// link to gfx
}

// set LevelItem as base class
public class Customer : MonoBehaviour 
{
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
	
	public void setState(CustomerState state)
	{
		_currentState = state;
	}
	
	void Awake()
	{
		_sprite = gameObject.GetComponent<tk2dAnimatedSprite>();
	}
	
	// NPCDesc desc?
	void configure()
	{
		// set sprite
		// set collider size
		// set interests
		// etc (from DB) 
	}
}
