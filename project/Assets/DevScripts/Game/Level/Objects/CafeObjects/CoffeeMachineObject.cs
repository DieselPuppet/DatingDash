using UnityEngine;
using System.Collections;

public class CoffeeMachineObject : BaseObject 
{
	enum CoffeeMachineState
	{
		IDLE,
		WORK_NORMAL,
		BROCKEN
	}	
	
	CoffeeMachineState _state;
	CoffeeMachineState _nextState;	
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
		
		_type = ObjectType.COFFEE_MACHINE;
		_state = CoffeeMachineState.IDLE;
	}	
	
	void setState(CoffeeMachineState state, float delay=-1)
	{
		if (delay == -1)
		{
			switch(state)
			{
			case CoffeeMachineState.WORK_NORMAL:
				break;
			default:
				Logger.message(LogLevel.LOG_ERROR, "Unknown coffee machine state - "+state.ToString());
				break;
			}
			
			_state = state;
		}
		else 
		{
			_nextState = state;
			Invoke("onStateChanged", delay);
		}
	}
	
	void onStateChanged()
	{
		setState(_nextState);
	}	
	
	public override void onAction()
	{
		if (_state == CoffeeMachineState.IDLE)
		{
			if (Inventory.instance.hasStuff("CUP_SMALL") && Inventory.instance.hasStuff("CUP_BIG"))
			{
				string stuff = Inventory.instance.higherPriority("CUP_SMALL", "CUP_BIG");
				Inventory.instance.removeStuff(stuff);
				
				string actionName = "MAKE_"+stuff;
				
				doAction(actionName);
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);
				
				setState(CoffeeMachineState.WORK_NORMAL);
				//setState(BlenderState.DEFAULT, getAction(actionName).time);
			}
			else if (Inventory.instance.hasStuff("CUP_SMALL"))
			{
				Inventory.instance.removeStuff("CUP_SMALL");
				
				string actionName = "MAKE_CUP_SMALL";
				
				doAction(actionName);
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);
			
				setState(CoffeeMachineState.WORK_NORMAL);
				//setState(BlenderState.DEFAULT, getAction("work_"+appleID).time);
			}
			else if (Inventory.instance.hasStuff("CUP_BIG"))
			{
				Inventory.instance.removeStuff("CUP_BIG");
				
				string actionName = "MAKE_CUP_BIG";
				
				doAction(actionName);
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);				
			
				setState(CoffeeMachineState.WORK_NORMAL);	
				//setState(BlenderState.DEFAULT, getAction("work_orange").time);
			}
			else 
			{
				PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
			}			
		}
	}	
}
