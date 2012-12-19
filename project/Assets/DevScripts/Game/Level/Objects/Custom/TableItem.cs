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
		
	/*	if (chairs[0].customer != null && 
			(chairs[0].customer.currentState == CustomerStateOld.WAIT_FOR_ORDER || chairs[0].customer.currentState == CustomerStateOld.SIT_ANGRY))
		{			
			foreach(Order order in chairs[0].customer.orders)			
			{		
				if (Inventory.instance.canCompleteOrder(order.productID))
				{	
					order.complete();
					Inventory.instance.removeStuff(order.productID);
				}		
			}
						
			bool orderSuccess = true;
			
			foreach(Order order in chairs[0].customer.orders)	
			{
				if (!order.isComplete)
					orderSuccess = false;
			}
			
			if (orderSuccess)
				chairs[0].customer.setState(CustomerStateOld.EAT);
		}
	
		if (chairs[1].customer != null && 
			(chairs[1].customer.currentState == CustomerStateOld.WAIT_FOR_ORDER || chairs[1].customer.currentState == CustomerStateOld.SIT_ANGRY))
		{			
			foreach(Order order in chairs[1].customer.orders)			
			{				
				if (Inventory.instance.canCompleteOrder(order.productID))
				{	
					order.complete();
					Inventory.instance.removeStuff(order.productID);
				}					
			}
						
			bool orderSuccess = true;
			
			foreach(Order order in chairs[1].customer.orders)	
			{
				if (!order.isComplete)
					orderSuccess = false;
			}
			
			if (orderSuccess)
				chairs[1].customer.setState(CustomerStateOld.EAT);				
		}*/
	}
	
	protected override void onTouch(){}
}
