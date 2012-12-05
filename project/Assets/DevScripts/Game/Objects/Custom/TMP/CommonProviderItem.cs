using UnityEngine;
using System.Collections;

// THINGS
/*
 Array<States> states;
 
 State : 
 
 
 
 */

public class CommonProviderItem : LevelItem 
{
	enum State
	{
		IDLE,
		WORK
	}
	
	State _state;
	
	void Awake()
	{
		base.Awake();
		
		// TODO : will be placed in ItemsDB
		Action act = new Action(this, 5, 0);
		act.pushComponent(new Action.ActionComponent(Action.ActionComponent.Type.ANIM, "SimpleAnimations", objectType+"_work"));
		addAction(objectType+"_work", act);
		_state = State.IDLE;
	}
	
	public override void onAction()
	{
		if (Inventory.instance.canAddStuff())
		{	
			if (_state == State.IDLE)
				doAction(objectType + "_work");
			
			Inventory.instance.addStuf(objectType);
			
			_state = State.WORK;
		}
	}
}
