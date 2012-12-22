using UnityEngine;
using System.Collections;
 
public class BlenderObject : BaseObject 
{
	[System.Serializable]
	public class UpgradeSettings
	{
		public string level;
		
		public string potionZeroSprite;
		public string potionOneAppleSprite;
		public string potionTwoAppleSprite;
		public string potionOneOrangeSprite;
		public string potionTwoOrangeSprite;		
		
		public string indicatorAtlasName;
		public string indicatorAnimationAtlasName;
		
		public string brockenOrangeSprite;
		public string brockenAppleSprite;
		
		public Vector2 indicatorOffset = Vector2.zero;
		
		public float speed;
		
		public int portionNum;
	}
	
	public UpgradeSettings[] upgrades;
	UpgradeSettings usettings;
	
	public tk2dAnimatedSprite indicator;
	public string timerGreenAnimation;
	public string timerRedAnimation;
	
	public tk2dAnimatedSprite brockenSprite;
	
	ItemTypes _currentProduct = ItemTypes.UNKNOWN;
	
	enum BlenderState
	{
		IDLE,
		PREPARE,
		WORK_NORMAL,
		WORK_DANGER,
		BROCKEN
	}
	
	BlenderState _state;
	BlenderState _nextState;
	
	int _portionCapacity;
	int _currentPortionNum;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
		
		usettings = upgrades[level];
		
		indicator.gameObject.SetActive(false);
		indicator.gameObject.transform.Translate(usettings.indicatorOffset.x, usettings.indicatorOffset.y, 0);
		
		ContentManager.instance.configureObject(indicator, usettings.indicatorAtlasName, "");
		ContentManager.instance.precacheAnimation(indicator, usettings.indicatorAnimationAtlasName);
		
		brockenSprite.gameObject.SetActive(false);
		
		_portionCapacity = usettings.portionNum;	
		_currentPortionNum = 0;	
		
		_type = ObjectType.BLENDER;
		_state = BlenderState.IDLE;
	}
	
	void setState(BlenderState state, float delay=-1)
	{
		if (delay == -1)
		{
			switch(state)
			{
			case BlenderState.IDLE:
				if (_currentProduct != ItemTypes.UNKNOWN)
				{
					_sprite.Stop();
					Inventory.instance.addStuf(_currentProduct.ToString());
					_currentProduct = ItemTypes.UNKNOWN;
				}
				
				resetToDefaults();
				indicator.Stop();
				indicator.gameObject.SetActive(false);
				break;
				
			case BlenderState.WORK_NORMAL:
				break;
				
			case BlenderState.WORK_DANGER:
				setState(BlenderState.BROCKEN, getAction("MAKE_ORANGE").actionTime);
				break;
				
			case BlenderState.BROCKEN:
				_sprite.Stop();
				
				brockenSprite.gameObject.SetActive(true);
				
				if (_currentProduct == ItemTypes.APPLE_JUCE)
					ContentManager.instance.configureObject(brockenSprite, settings.spriteAtlas, usettings.brockenAppleSprite);
				else 
					ContentManager.instance.configureObject(brockenSprite, settings.spriteAtlas, usettings.brockenOrangeSprite);
				
				break;
				
			default:
				Logger.message(LogLevel.LOG_ERROR, "Unknown blender state - "+state.ToString());
				break;
			}
			
			_state = state;
		}
		else 
		{
			_nextState = state;
			Invoke("onStateChanged", delay);
		}
	}
	
	void onStateChanged()
	{
		setState(_nextState);
	}
	
	public override void onDoAction(string actionName)
	{		
		if (actionName == "MAKE_ORANGE")
		{
			indicator.gameObject.SetActive(true);
			indicator.Play(timerGreenAnimation);			
			
			Invoke("updateToFull", getAction("MAKE_ORANGE").actionTime);
		}
		else if (actionName == "MAKE_APPLE")
		{	
			indicator.gameObject.SetActive(true);
			indicator.Play(timerGreenAnimation);			
			
			Invoke("updateToFull", getAction("MAKE_APPLE").actionTime);
		}
	}
	
	void updateToFull()
	{
		updatePortionNum(2);
	}
	
	void updatePortionNum(int num)
	{		
		if (_state == BlenderState.WORK_NORMAL)
		{
			setState(BlenderState.WORK_DANGER);
			indicator.Play(timerRedAnimation);		
		}
		else 
		{
			indicator.Stop();
			indicator.gameObject.SetActive(false);
			CancelInvoke();
			_sprite.Stop();
		}
		
		_currentPortionNum = num;
		_state = BlenderState.IDLE;
		
		if (num == 2)
		{
			if (_currentProduct == ItemTypes.APPLE_JUCE)
				ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, usettings.potionTwoAppleSprite);		
			else 
				ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, usettings.potionTwoOrangeSprite);	
		}
		else if (num == 1)
		{
			
			if (_currentProduct == ItemTypes.APPLE_JUCE)
				ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, usettings.potionOneAppleSprite);
			else 
				ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, usettings.potionOneOrangeSprite);
		}
		else if (num == 0)
		{
			ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, usettings.potionZeroSprite);
		}
	}
	
	public override void onAction()
	{		
		if (_state == BlenderState.IDLE)
		{
			if (_currentPortionNum > 0)
			{
				Inventory.instance.addStuf(_currentProduct.ToString());
				_currentPortionNum--;
				
				updatePortionNum(_currentPortionNum);
				
				PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
			}
			else 
			{	
				if (Inventory.instance.hasStuff("ORANGE") || Inventory.instance.hasStuff("APPLE"))
				{
					string sources;
					
					if (Inventory.instance.hasStuff("ORANGE") && Inventory.instance.hasStuff("APPLE"))
						sources = Inventory.instance.higherPriority("ORANGE", "APPLE");		
					else if (Inventory.instance.hasStuff("ORANGE"))
						sources = "ORANGE";
					else 
						sources = "APPLE";
					
					if (sources == "ORANGE")
						_currentProduct = ItemTypes.ORANGE_JUCE;
					else 
						_currentProduct = ItemTypes.APPLE_JUCE;				
					
					Inventory.instance.removeStuff(sources);
					
					doAction("PREPARE");
					
					string workActionName = "MAKE_"+sources;
					doAction(workActionName, getAction("PREPARE").requiredTime);
					
					setState(BlenderState.WORK_NORMAL);
				}	
				else 
				{
					PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
				}
			}
		}
		else if (_state == BlenderState.WORK_NORMAL)
		{
			PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
		}
		else if (_state == BlenderState.WORK_DANGER)
		{
			setState(BlenderState.IDLE);
			updateToFull();
		}
		else if (_state == BlenderState.BROCKEN)
		{
			PlayerBehaviour.instance.setBusy(2);
			brockenSprite.gameObject.SetActive(false);
			indicator.gameObject.SetActive(false);
		}
	}
}
