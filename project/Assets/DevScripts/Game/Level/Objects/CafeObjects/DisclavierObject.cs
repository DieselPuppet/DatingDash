using UnityEngine;
using System.Collections;

public class DisclavierObject : BaseObject 
{
	public tk2dAnimatedSprite baseSprite;
	
	enum DicslavierState
	{
		CLOSED,
		PLAYED
	}
	
	DicslavierState _state;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
		
		_type = ObjectType.DISCLAVIER;
		_state = DicslavierState.CLOSED;
	}		
	
	public override void onAction()
	{
		doAction("Play");
	}
}
