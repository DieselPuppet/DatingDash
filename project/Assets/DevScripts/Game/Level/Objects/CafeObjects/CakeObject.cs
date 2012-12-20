using UnityEngine;
using System.Collections;

public class CakeObject : BaseObject
{
	public ObjectType target;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
		
		_type = ObjectType.CAKE;
	}
	
	public override void onAction()
	{
	}
}
