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
	
	public Customer customer = null;
	
	protected override void onTouch()
	{		
		if (table != null)
		{
			Customer cust = Level.instance.activeCustomer;
			if (cust !=null)
			{
				cust.moveTo(gameObject.transform.position);
				cust.setState(CustomerState.ORDER);	
			}
			
			table.onChair(isLeft);
		}
		else 
		{
			Logger.message(LogLevel.LOG_ERROR, "Table is not assign");
		}		
	}
}
