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
		if (Inventory.instance.hasStuff(OrderTypes.COFFEE_BIG.ToString()) && Inventory.instance.hasStuff(OrderTypes.COFFEE_SMALL.ToString()))
		{
			string stuff = Inventory.instance.higherPriority(OrderTypes.COFFEE_BIG.ToString(), OrderTypes.COFFEE_SMALL.ToString());
			if (stuff == OrderTypes.COFFEE_BIG.ToString())
			{
				Inventory.instance.removeStuff(OrderTypes.COFFEE_BIG.ToString());
				Inventory.instance.addStuf(OrderTypes.COFFEE_MILK_BIG.ToString());
			}
			else if (stuff == OrderTypes.COFFEE_SMALL.ToString())
			{
				Inventory.instance.removeStuff(OrderTypes.COFFEE_SMALL.ToString());
				Inventory.instance.addStuf(OrderTypes.COFFEE_MILK_SMALL.ToString());
			}
			
			doAction("MILK_WORK");
		}
		else if (Inventory.instance.hasStuff(OrderTypes.COFFEE_BIG.ToString()))
		{
			Inventory.instance.removeStuff(OrderTypes.COFFEE_BIG.ToString());
			Inventory.instance.addStuf(OrderTypes.COFFEE_MILK_BIG.ToString());
			
			doAction("MILK_WORK");
		}
		else if (Inventory.instance.hasStuff(OrderTypes.COFFEE_SMALL.ToString()))
		{
			Inventory.instance.removeStuff(OrderTypes.COFFEE_SMALL.ToString());
			Inventory.instance.addStuf(OrderTypes.COFFEE_MILK_SMALL.ToString());
			
			doAction("MILK_WORK");
		}
		
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
	}
}
