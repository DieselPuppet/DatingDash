using UnityEngine;
using System.Collections;

[System.Serializable]
public class ChairItem : LevelItem
{
	[System.NonSerialized]
	public bool isFree = true;
	[System.NonSerializedAttribute]
	public TableItem table;
	
	public bool isLeft = true;
	
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
