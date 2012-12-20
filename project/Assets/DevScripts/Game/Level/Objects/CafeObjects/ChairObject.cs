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
	public Customer customer = null;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);	
		
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
