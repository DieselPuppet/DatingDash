using UnityEngine;
using System.Collections;

public class TableItem : LevelItem
{
	public ChairItem[] chairs;
	
	void Awake()
	{
		base.Awake();
		foreach(ChairItem chair in chairs)
		{
			chair.table = this;
		}
	}
	
	public void onChair(bool left)
	{
		PlayerBehaviour.instance.moveTo(this);
	}
	
	public override void onAction()
	{
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
		
		if (!chairs[0].isFree)
		{
			if (chairs[0].customer != null && chairs[0].customer.currentState == CustomerState.WAIT_FOR_ORDER)
			{
				// Check in inventory or right here?..	
				
				foreach(string order in chairs[0].customer.orders)			
				{
					if (Inventory.instance.hasStuff(order))
					{
						// ?
						Inventory.instance.finishOrder(order);
						chairs[0].customer.orders.Remove(order);
					}					
				}
			}
		}
		
		if (!chairs[1].isFree)
		{
			if (chairs[1].customer != null && chairs[1].customer.currentState == CustomerState.WAIT_FOR_ORDER)
			{
				// Check in inventory or right here?..
				
				foreach(string order in chairs[1].customer.orders)			
				{
					if (Inventory.instance.hasStuff(order))
					{
						// ?
						Inventory.instance.finishOrder(order);
						chairs[1].customer.orders.Remove(order);
					}					
				}				
			}
		}
	}
	
	protected override void onTouch(){}
}
