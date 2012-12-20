using UnityEngine;
using System.Collections;

public class CakeObject : BaseObject
{
	public string targetObjectName;
	
	CakePlateObject target;
	
	enum CakeState
	{
		IDLE,
		WORK
	}
	
	CakeState _state;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
		
		target = GameObject.Find(targetObjectName).GetComponent<CakePlateObject>();
		
		_type = ObjectType.CAKE;
	}
	
	public override void onAction()
	{
		target.test();
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
	}
}
