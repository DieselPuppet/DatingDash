using UnityEngine;
using System.Collections;

public class SeatPlaceObject : BaseObject 
{
	public bool isLeft = true;
	[System.NonSerialized]
	public bool isFree = true;	
	[System.NonSerialized]
	public Customer customer = null;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);	
				
		_type = ObjectType.SEAT_PLACE;
	}		
	
	protected override void onTouch()
	{
	}
}

