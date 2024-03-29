using UnityEngine;
using System.Collections;

public class TableObject : BaseObject
{
	public ChairObject[] chairs;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
				
		foreach(ChairObject chair in chairs)
		{
			chair.table = this;
		}		
		
		_type = ObjectType.TABLE;
	}	
	
	public void onChair(bool left)
	{
		PlayerBehaviour.instance.moveTo(this);
	}
	
	public override void onAction()
	{
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
		
		if (chairs[0].customer != null && 
			(chairs[0].customer.currentState == CustomerStateDeprecated.WAITING_ORDER))
		{			
			foreach(Order order in chairs[0].customer.orders)			
			{		
				Debug.Log("Check order - "+order.productID);
				
				if (Inventory.instance.canCompleteOrder(order.productID))
				{	
					Debug.Log("true");
					
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
				chairs[0].customer.setState(CustomerStateDeprecated.EAT);
		}
	
		if (chairs[1].customer != null && 
			(chairs[1].customer.currentState == CustomerStateDeprecated.WAITING_ORDER))
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
				chairs[1].customer.setState(CustomerStateDeprecated.EAT);		
		}
	}
	
	protected override void onTouch(){}
}
