using UnityEngine;
using System.Collections;

public class BlenderItem : LevelItem
{
	// TODO: delete. Base class already contains State enum
	enum BlenderState
	{
		DEFAULT,
		WORK_NORMAL,
		WORK_FAIL,
		BROCKEN
	}
	
	BlenderState _state;
	
	void Awake()
	{
		base.Awake();	
	}
	
	void Start()
	{		
		_state = BlenderState.DEFAULT;
		
		Action act = new Action(this, 5, 5);
		act.pushComponent(new Action.ActionComponent(Action.ActionComponent.Type.ANIM, "BlenderAnimation", "blender01_work_apple"));
		addAction("work_apple", act);
		
		act = new Action(this, 5, 5);
		act.pushComponent(new Action.ActionComponent(Action.ActionComponent.Type.ANIM, "BlenderAnimation", "blender01_work_orange"));
		addAction("work_orange", act);			
	}
	
	BlenderState _nextState;
	
	void setState(BlenderState s, float delay=-1)
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
		case BlenderState.DEFAULT:
			reset();
			
			break;
			
		default:
			break;
		}
	}
	
	public override void onAction()
	{		
		if (_state == BlenderState.DEFAULT)
		{
			if (Inventory.instance.hasStuff("apple_sources") && Inventory.instance.hasStuff("orange_sources"))
			{
				string stuff = Inventory.instance.higherPriority("apple_sources", "orange_sources");
				Inventory.instance.removeStuff(stuff);
				
				string actionName = "work_"+stuff;
				
				doAction(actionName);
				PlayerBehaviour.instance.setBusy(getAction(actionName).reqTime);
				
				setState(BlenderState.WORK_NORMAL);
				setState(BlenderState.DEFAULT, getAction(actionName).time);
			}
			else if (Inventory.instance.hasStuff("apple_sources"))
			{
				Inventory.instance.removeStuff("apple_sources");
				
				doAction("work_apple");
				PlayerBehaviour.instance.setBusy(getAction("work_apple").reqTime);
			
				setState(BlenderState.WORK_NORMAL);
				setState(BlenderState.DEFAULT, getAction("work_apple").time);
			}
			else if (Inventory.instance.hasStuff("orange_sources"))
			{
				Inventory.instance.removeStuff("orange_sources");
				
				doAction("work_orange");
				PlayerBehaviour.instance.setBusy(getAction("work_orange").reqTime);
			
				setState(BlenderState.WORK_NORMAL);	
				setState(BlenderState.DEFAULT, getAction("work_orange").time);
			}
			else 
			{
				PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
			}
		}
		else if (_state == BlenderState.BROCKEN)
		{
			doAction("cleanup");
		}
	}	
}
