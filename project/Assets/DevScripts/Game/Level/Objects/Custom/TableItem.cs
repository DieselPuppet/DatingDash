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
			int chair = left ? 0 : 1;
			chairs[chair].isFree = false;
			
			Customer cust = Level.instance.activeCustomer;
			cust.setActive(false);			
		}
	}
	
	public override void onAction()
	{
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
		
		if (!chairs[0].isFree)
		{
			Debug.Log("left customer");
		}
		
		if (!chairs[1].isFree)
		{
			Debug.Log("right customer");
		}
	}
	
	protected override void onTouch(){}
}
