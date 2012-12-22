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
		if (Inventory.instance.hasStuff(ItemTypes.CUP_BIG_COFFEE.ToString()) && Inventory.instance.hasStuff(ItemTypes.CUP_SMALL_COFFEE.ToString()))
		{
			string stuff = Inventory.instance.higherPriority(ItemTypes.CUP_BIG_COFFEE.ToString(), ItemTypes.CUP_SMALL_COFFEE.ToString());
			if (stuff == ItemTypes.CUP_BIG_COFFEE.ToString())
			{
				Inventory.instance.removeStuff(ItemTypes.CUP_BIG_COFFEE.ToString());
				Inventory.instance.addStuf(ItemTypes.CUP_BIG_COFFEE_MILK.ToString());
			}
			else if (stuff == ItemTypes.CUP_SMALL_COFFEE.ToString())
			{
				Inventory.instance.removeStuff(ItemTypes.CUP_SMALL_COFFEE.ToString());
				Inventory.instance.addStuf(ItemTypes.CUP_SMALL_COFFEE_MILK.ToString());
			}
			
			doAction("MILK_WORK");
		}
		else if (Inventory.instance.hasStuff(ItemTypes.CUP_BIG_COFFEE.ToString()))
		{
			Inventory.instance.removeStuff(ItemTypes.CUP_BIG_COFFEE.ToString());
			Inventory.instance.addStuf(ItemTypes.CUP_BIG_COFFEE_MILK.ToString());
			
			doAction("MILK_WORK");
		}
		else if (Inventory.instance.hasStuff(ItemTypes.CUP_SMALL_COFFEE.ToString()))
		{
			Inventory.instance.removeStuff(ItemTypes.CUP_SMALL_COFFEE.ToString());
			Inventory.instance.addStuf(ItemTypes.CUP_SMALL_COFFEE_MILK.ToString());
			
			doAction("MILK_WORK");
		}
		
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
	}
}
