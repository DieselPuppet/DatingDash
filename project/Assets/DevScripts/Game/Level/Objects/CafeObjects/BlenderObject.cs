using UnityEngine;
using System.Collections;
 
public class BlenderObject : BaseObject 
{
	[System.Serializable]
	public class UpgradeSettings
	{
		public string level;
		public string indicatorAtlasName;
		public string indicatorAnimationAtlasName;
		
		public Vector2 indicatorOffset = Vector2.zero;
		
		public float speed;
		
		public int portionNum;
	}
	
	public UpgradeSettings[] upgrades;
	
	public tk2dAnimatedSprite indicator;
	public string timerGreenAnimation;
	
	OrderProducts _currentProduct = OrderProducts.UNKNOWN;
	
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
		
		UpgradeSettings settings = upgrades[level];
		
		indicator.gameObject.SetActive(false);
		indicator.gameObject.transform.Translate(settings.indicatorOffset.x, settings.indicatorOffset.y, 0);
		
		ContentManager.instance.configureObject(indicator, settings.indicatorAtlasName, "");
		ContentManager.instance.precacheAnimation(indicator, settings.indicatorAnimationAtlasName);
		
		_portionCapacity = settings.portionNum;	
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
				if (_currentProduct != OrderProducts.UNKNOWN)
				{
					_sprite.Stop();
					Inventory.instance.addStuf(_currentProduct.ToString());
					_currentProduct = OrderProducts.UNKNOWN;
				}
				
				resetToDefaults();
				indicator.Stop();
				indicator.gameObject.SetActive(false);
				break;
				
			case BlenderState.WORK_NORMAL:
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
	
	void startJuice()
	{
		indicator.gameObject.SetActive(true);
		indicator.Play(timerGreenAnimation);
		
		//Invoke("juceReady", getAction("MAKE_"+_currentProduct.ToString()).actionTime);
	}
	
	public override void onDoAction(string actionName)
	{
		Debug.Log("onDoAction "+actionName);
		
		if (actionName == "MAKE_ORANGE")
		{
			Invoke("juiceComplete", getAction("MAKE_ORANGE").actionTime);
		}
		else if (actionName == "MAKE_APPLE")
		{
			Invoke("juiceComplete", getAction("MAKE_APPLE").actionTime);
		}
	}
	
	void updatePortionNum(int num = 4)
	{
		_currentPortionNum = num;
		_state = BlenderState.IDLE;
		_sprite.Stop();
		
		//if (num == 4)
		//	ContentManager.instance.configureObject(_sprite, settings.spriteAtlas
	}
	
	public override void onAction()
	{		
		if (_state == BlenderState.IDLE)
		{
			if (_currentPortionNum > 0)
			{
				if (_currentProduct == OrderProducts.APPLE_JUCE)
					Inventory.instance.addStuf(OrderProducts.APPLE_JUCE.ToString());
				else if (_currentProduct == OrderProducts.ORANGE_JUCE)
					Inventory.instance.addStuf(OrderProducts.ORANGE_JUCE.ToString());
				
				_currentPortionNum--;
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
						_currentProduct = OrderProducts.ORANGE_JUCE;
					else 
						_currentProduct = OrderProducts.APPLE_JUCE;				
					
					doAction("PREPARE");
					
					string workActionName = "MAKE_"+sources;
					doAction(workActionName, getAction("PREPARE").requiredTime);
					
					setState(BlenderState.WORK_NORMAL);
				}	
			}
		}
		else if (_state == BlenderState.WORK_NORMAL)
		{
			PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
		}
		else if (_state == BlenderState.BROCKEN)
		{
			// TODO : add cleanup work
			PlayerBehaviour.instance.setBusy(1);
			PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
		}
	}
}
