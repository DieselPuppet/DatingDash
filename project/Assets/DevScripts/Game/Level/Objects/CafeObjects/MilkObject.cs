using UnityEngine;
using System.Collections;

public class MilkObject : BaseObject 
{
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
				
		_type = ObjectType.MILK;
	}	
	
	public override void onAction ()
	{
		if (Inventory.instance.hasStuff(ObjectType.CUP_BIG.ToString()) && Inventory.instance.hasStuff(ObjectType.CUP_SMALL.ToString()))
		{
			string stuff = Inventory.instance.higherPriority(ObjectType.CUP_BIG.ToString(), ObjectType.CUP_SMALL.ToString());
			if (stuff == ObjectType.CUP_BIG.ToString())
			{
				Inventory.instance.removeStuff(ObjectType.CUP_BIG.ToString());
				Inventory.instance.addStuf(ObjectType.CUP_BIG_MILK.ToString());
			}
		}
		else if (Inventory.instance.hasStuff(ObjectType.CUP_BIG.ToString()))
		{
			Inventory.instance.removeStuff(ObjectType.CUP_BIG.ToString());
			Inventory.instance.addStuf(ObjectType.CUP_BIG_MILK.ToString());			
		}
		else if (Inventory.instance.hasStuff(ObjectType.CUP_SMALL.ToString()))
		{
			Inventory.instance.removeStuff(ObjectType.CUP_SMALL.ToString());
			Inventory.instance.addStuf(ObjectType.CUP_SMALL_MILK.ToString());			
		}	
		
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
	}
}
