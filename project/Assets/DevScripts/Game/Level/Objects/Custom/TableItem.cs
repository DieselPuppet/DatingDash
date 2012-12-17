using UnityEngine;
using System.Collections;

public class TableItem : LevelItem
{
	public ChairItem[] chairs;
	
	enum State
	{
		EMPTY,
		FULL
	}
	
	void Awake()
	{
		base.Awake();
		foreach(ChairItem chair in chairs)
		{
			chair.table = this;
		}
	}
	
	State _state;	
	
	public void onChair(bool left)
	{
		if (Level.instance.activeCustomer == null)
		{
			PlayerBehaviour.instance.moveTo(this);
		}
		else 
		{
			Customer cust = Level.instance.activeCustomer;
			cust.moveTo(PathGraph.instance.getPointByName(pointName));
			cust.setActive(false);
			cust.setState(CustomerState.ORDER);
			
			int chair = left ? 0 : 1;
			chairs[chair].isFree = false;
		}
	}
	
	public override void onAction()
	{
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
	}
	
	protected override void onTouch(){}
}
