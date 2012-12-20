using UnityEngine;
using System.Collections;

public class ProviderObject : BaseObject
{
	public ObjectType sourceType;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
		
		_type = sourceType;
	}	
	
	public override void onAction()
	{
		if (Inventory.instance.canAddStuff())
		{					
			//if (_curState == State.IDLE)
			//{
			doAction("GET_"+sourceType.ToString());
			//}
			
			Inventory.instance.addStuf(sourceType.ToString());
			PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
		}			
	}
}
