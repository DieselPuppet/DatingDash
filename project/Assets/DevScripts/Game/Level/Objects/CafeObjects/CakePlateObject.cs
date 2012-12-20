using UnityEngine;
using System.Collections;

public class CakePlateObject : BaseObject 
{
	public string emptySprite;
	public string onePieceSprite;
	public string twoPieceSprite;
	public string threePieceSprite;
	public string fourPieceSprite;
	public string fivePieceSprite;
	
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
	
	public void test()
	{
		_pieceNum = 4;
		
		GraphicsSettings settings = graphicSettings[level];
		ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, fourPieceSprite);
	}
	
	public override void onAction()
	{
		if (_pieceNum > 0)
		{
			if (Inventory.instance.canAddStuff())
			{
				Inventory.instance.addStuf(cakeType.ToString());
				
				_pieceNum--;
				
				GraphicsSettings settings = graphicSettings[level];
				
				if (_pieceNum == 3)
					ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, threePieceSprite);
				else if (_pieceNum == 2)
					ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, twoPieceSprite);
				else if (_pieceNum == 1)
					ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, onePieceSprite);
				else 
					ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, emptySprite);
			}
		}
		
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
	}	
}
