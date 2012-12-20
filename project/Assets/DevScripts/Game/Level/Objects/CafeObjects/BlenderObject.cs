using UnityEngine;
using System.Collections;
 
public class BlenderObject : BaseObject 
{	
	enum BlenderState
	{
		IDLE,
		WORK_NORMAL,
		BROCKEN
	}
	
	BlenderState _state;
	BlenderState _nextState;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
		
		_type = ObjectType.BLENDER;
		_state = BlenderState.IDLE;
	}
	
	void setState(BlenderState state, float delay=-1)
	{
		if (delay == -1)
		{
			switch(state)
			{
			case BlenderState.WORK_NORMAL:
				break;
			default:
				Logger.message(LogLevel.LOG_ERROR, "Unknown blender state - "+state.ToString());
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
		if (_state == BlenderState.IDLE)
		{
			if (Inventory.instance.hasStuff("ORANGE") && Inventory.instance.hasStuff("APPLE"))
			{
				string stuff = Inventory.instance.higherPriority("ORANGE", "APPLE");
				Inventory.instance.removeStuff(stuff);
				
				string actionName = "MAKE_"+stuff;
				
				doAction(actionName);
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);
				
				setState(BlenderState.WORK_NORMAL);
				//setState(BlenderState.DEFAULT, getAction(actionName).time);
			}
			else if (Inventory.instance.hasStuff("APPLE"))
			{
				Inventory.instance.removeStuff("APPLE");
				
				string actionName = "MAKE_APPLE";
				
				doAction(actionName);
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);
			
				setState(BlenderState.WORK_NORMAL);
				//setState(BlenderState.DEFAULT, getAction("work_"+appleID).time);
			}
			else if (Inventory.instance.hasStuff("ORANGE"))
			{
				Inventory.instance.removeStuff("ORANGE");
				
				string actionName = "MAKE_ORANGE";
				
				doAction(actionName);
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);				
			
				setState(BlenderState.WORK_NORMAL);	
				//setState(BlenderState.DEFAULT, getAction("work_orange").time);
			}
			else 
			{
				PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
			}			
		}
	}
}
