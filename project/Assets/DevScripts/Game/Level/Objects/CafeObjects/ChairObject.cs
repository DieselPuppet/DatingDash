using UnityEngine;
using System.Collections;

[System.Serializable]
public class ChairObject : BaseObject
{
	[System.NonSerialized]
	public bool isFree = true;
	[System.NonSerialized]
	public TableObject table;
	
	public bool isLeft = true;
	
	// remove
	[System.NonSerialized]
	public CustomerDeprecated customer = null;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);	
		
		gameObject.transform.parent = null;
		Vector3 position = gameObject.transform.localPosition;
		position.z = (((position.y-_sprite.GetBounds().size.y/2)*0.01f))+.01f;
		gameObject.transform.localPosition = position;		
		
		_type = ObjectType.CHAIR;
	}		
	
	protected override void onTouch()
	{		
		if (table != null)
		{		
			table.onChair(isLeft);
		}
		else 
		{
			Logger.message(LogLevel.LOG_ERROR, "Table is not assign");
		}		
	}
}
