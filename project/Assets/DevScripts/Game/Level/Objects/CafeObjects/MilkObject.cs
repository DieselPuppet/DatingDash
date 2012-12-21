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
		if (Inventory.instance.hasStuff(OrderProducts.COFFEE_BIG.ToString()) && Inventory.instance.hasStuff(OrderProducts.COFFEE_SMALL.ToString()))
		{
			string stuff = Inventory.instance.higherPriority(OrderProducts.COFFEE_BIG.ToString(), OrderProducts.COFFEE_SMALL.ToString());
			if (stuff == OrderProducts.COFFEE_BIG.ToString())
			{
				Inventory.instance.removeStuff(OrderProducts.COFFEE_BIG.ToString());
				Inventory.instance.addStuf(OrderProducts.COFFEE_MILK_BIG.ToString());
			}
			else if (stuff == OrderProducts.COFFEE_SMALL.ToString())
			{
				Inventory.instance.removeStuff(OrderProducts.COFFEE_SMALL.ToString());
				Inventory.instance.addStuf(OrderProducts.COFFEE_MILK_SMALL.ToString());
			}
			
			doAction("MILK_WORK");
		}
		else if (Inventory.instance.hasStuff(OrderProducts.COFFEE_BIG.ToString()))
		{
			Inventory.instance.removeStuff(OrderProducts.COFFEE_BIG.ToString());
			Inventory.instance.addStuf(OrderProducts.COFFEE_MILK_BIG.ToString());
			
			doAction("MILK_WORK");
		}
		else if (Inventory.instance.hasStuff(OrderProducts.COFFEE_SMALL.ToString()))
		{
			Inventory.instance.removeStuff(OrderProducts.COFFEE_SMALL.ToString());
			Inventory.instance.addStuf(OrderProducts.COFFEE_MILK_SMALL.ToString());
			
			doAction("MILK_WORK");
		}
		
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
	}
}
