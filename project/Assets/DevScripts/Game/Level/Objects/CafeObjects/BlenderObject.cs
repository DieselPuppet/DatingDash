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
		
		public float speed;
	}
	
	public UpgradeSettings[] upgrades;
	
	public tk2dAnimatedSprite indicator;
	public string timerGreenAnimation;
	
	OrderProducts _currentProduct = OrderProducts.UNKNOWN;
	
	enum BlenderState
	{
		IDLE,
		WORK_NORMAL,
		WORK_DANGER,
		BROCKEN
	}
	
	BlenderState _state;
	BlenderState _nextState;
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
		
		UpgradeSettings settings = upgrades[level];
		
		indicator.gameObject.SetActive(false);
		ContentManager.instance.configureObject(indicator, settings.indicatorAtlasName, "");
		ContentManager.instance.precacheAnimation(indicator, settings.indicatorAnimationAtlasName);
		
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
	
	void playTimerAnimation(string animName)
	{
		indicator.gameObject.SetActive(true);
		indicator.Play(animName);
	}
	
	public override void onAction()
	{
		if (_state == BlenderState.IDLE)
		{
			if (Inventory.instance.hasStuff("ORANGE") && Inventory.instance.hasStuff("APPLE"))
			{
				string stuff = Inventory.instance.higherPriority("ORANGE", "APPLE");
				Inventory.instance.removeStuff(stuff);
				
				string actionName = "MAKE_"+stuff;
				
				doAction(actionName);
				playTimerAnimation(timerGreenAnimation);
				
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);
				
				if (stuff == "ORANGE")
					_currentProduct = OrderProducts.ORANGE_JUCE;
				else 
					_currentProduct = OrderProducts.APPLE_JUCE;
				
				setState(BlenderState.WORK_NORMAL);
				setState(BlenderState.IDLE, getAction(actionName).actionTime);
			}
			else if (Inventory.instance.hasStuff("APPLE"))
			{
				Inventory.instance.removeStuff("APPLE");
				
				string actionName = "MAKE_APPLE";
				
				doAction(actionName);
				playTimerAnimation(timerGreenAnimation);
				
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);
			
				_currentProduct = OrderProducts.APPLE_JUCE;
				
				setState(BlenderState.WORK_NORMAL);
				setState(BlenderState.IDLE, getAction("MAKE_APPLE").actionTime);
			}
			else if (Inventory.instance.hasStuff("ORANGE"))
			{
				Inventory.instance.removeStuff("ORANGE");
				
				string actionName = "MAKE_ORANGE";
				
				doAction(actionName);
				playTimerAnimation(timerGreenAnimation);
				
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);				
			
				_currentProduct = OrderProducts.ORANGE_JUCE;
				
				setState(BlenderState.WORK_NORMAL);	
				setState(BlenderState.IDLE, getAction("MAKE_ORANGE").actionTime);
			}
			else 
			{
				PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
			}			
		}
	}
}
