using UnityEngine;
using System.Collections;

public class BlenderItem : LevelItem
{
	enum State
	{
		DEFAULT,
		WORK_NORMAL,
		WORK_FAIL,
		BROCKEN
	}
	
	State _state;
	
	void Awake()
	{
		base.Awake();	
	}
	
	void Start()
	{		
		_state = State.DEFAULT;
		
		Action act = new Action(this, 5, 5);
		act.pushComponent(new Action.ActionComponent(Action.ActionComponent.Type.ANIM, "BlenderAnimation", "blender01_work_apple"));
		addAction("work_apple", act);
		
		act = new Action(this, 5, 5);
		act.pushComponent(new Action.ActionComponent(Action.ActionComponent.Type.ANIM, "BlenderAnimation", "blender01_work_orange"));
		addAction("work_orange", act);			
	}
	
	State _nextState;
	
	void setState(State s, float delay=-1)
	{
		if (delay == -1)
		{
			_state = s;
			onStateChanged();
		}
		else 
		{
			_nextState = s;
			Invoke("setStateDelayed", delay);
		}
	}	
	
	void setStateDelayed()
	{
		_state = _nextState;
		onStateChanged();
	}
	
	void onStateChanged()
	{
		switch(_state)
		{
		case State.DEFAULT:
			reset();
			
			break;
			
		default:
			break;
		}
	}
	
	public override void onAction()
	{			
		if (_state == State.DEFAULT)
		{
			if (Inventory.instance.hasStuff("apple") && Inventory.instance.hasStuff("orange"))
			{
				string stuff = Inventory.instance.higherPriority("apple", "orange");
				Inventory.instance.removeStuff(stuff);
				
				string actionName = "work_"+stuff;
				
				doAction(actionName);
				PlayerBehaviour.instance.setBusy(getAction(actionName).reqTime);
				
				setState(State.WORK_NORMAL);
				setState(State.DEFAULT, getAction(actionName).time);
			}
			else if (Inventory.instance.hasStuff("apple"))
			{
				Inventory.instance.removeStuff("apple");
				
				doAction("work_apple");
				PlayerBehaviour.instance.setBusy(getAction("work_apple").reqTime);
			
				setState(State.WORK_NORMAL);
				setState(State.DEFAULT, getAction("work_apple").time);
			}
			else if (Inventory.instance.hasStuff("orange"))
			{
				Inventory.instance.removeStuff("orange");
				
				doAction("work_orange");
				PlayerBehaviour.instance.setBusy(getAction("work_orange").reqTime);
			
				setState(State.WORK_NORMAL);	
				setState(State.DEFAULT, getAction("work_orange").time);
			}
		}
		else if (_state == State.BROCKEN)
		{
			doAction("cleanup");
		}
	}	
}
