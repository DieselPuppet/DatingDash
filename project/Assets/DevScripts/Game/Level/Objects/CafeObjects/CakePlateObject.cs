using UnityEngine;
using System.Collections;

public class CakePlateObject : BaseObject 
{
	public ObjectType cakeType;
	public int defaultPieceNum;
	
	private int _pieceNum;
	public int pieceNum
	{
		get 
		{
			return _pieceNum;
		}
	}
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
		
		_pieceNum = defaultPieceNum;
		
		_type = cakeType;
	}	
	
	public override void onAction()
	{
		if (_pieceNum > 0)
		{
			if (Inventory.instance.canAddStuff())
			{
				Inventory.instance.addStuf(cakeType.ToString());
				PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
			}
		}
	}	
}
