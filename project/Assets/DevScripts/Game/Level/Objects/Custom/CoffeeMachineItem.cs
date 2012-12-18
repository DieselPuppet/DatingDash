using UnityEngine;
using System.Collections;

public class CoffeeMachineItem : LevelItem
{
	// TODO: delete. Base class already contains State enum
	enum CoffeeMachineState
	{
		DEFAULT,
		WORK_NORMAL,
		WORK_FAIL,
		REPAIRING,
		BROCKEN		
	}
	
	CoffeeMachineState _state;
	
	void Awake()
	{
		base.Awake();
		
		Action act1= new Action(this, 2, 2);
		act1.pushComponent(new Action.ActionComponent(Action.ActionComponent.Type.ANIM, "CofeeMachineAnimation", "work_cup_small"));
		addAction("work_cup_small", act1);
		
		Action act2= new Action(this, 2, 2);
		act2.pushComponent(new Action.ActionComponent(Action.ActionComponent.Type.ANIM, "CofeeMachineAnimation", "work_cup_big"));
		addAction("work_cup_big", act2);		
	}
	
	void setState(CoffeeMachineState s)
	{
		_state = s;
	}	
	
	public override void onAction()
	{
		if (_state == CoffeeMachineState.DEFAULT)
		{
			if (Inventory.instance.hasStuff("cup_small") && Inventory.instance.hasStuff("cup_big"))
			{
				string stuff = Inventory.instance.higherPriority("cup_small", "cup_big");
				Inventory.instance.removeStuff(stuff);
				string actionName = "work_"+stuff;
				
				doAction(actionName);
				PlayerBehaviour.instance.setBusy(getAction(actionName).reqTime);
				
				setState(CoffeeMachineState.WORK_NORMAL);
			}
			else if (Inventory.instance.hasStuff("cup_small"))
			{
				string actionName = "work_cup_small";
				Inventory.instance.removeStuff("cup_small");
				
				doAction(actionName);
				PlayerBehaviour.instance.setBusy(getAction(actionName).reqTime);
				
				setState(CoffeeMachineState.WORK_NORMAL);			
			}
			else if (Inventory.instance.hasStuff("cup_big"))
			{
				string actionName = "work_cup";
				Inventory.instance.removeStuff("cup_big");
				
				doAction(actionName);
				PlayerBehaviour.instance.setBusy(getAction(actionName).reqTime);
				
				setState(CoffeeMachineState.WORK_NORMAL);			
			}			
		}
		else if (_state == CoffeeMachineState.BROCKEN)
		{
			doAction("cleanup");
		}
	}	
}
