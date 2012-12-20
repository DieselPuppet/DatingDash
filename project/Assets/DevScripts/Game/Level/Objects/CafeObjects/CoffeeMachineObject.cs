using UnityEngine;
using System.Collections;

public class CoffeeMachineObject : BaseObject 
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
	
	enum CoffeeMachineState
	{
		IDLE,
		WORK_NORMAL,
		WORK_DANGER,
		BROCKEN
	}	
	
	CoffeeMachineState _state;
	CoffeeMachineState _nextState;	
	
	protected override void buildObject(int level)
	{		
		base.buildObject(level);
		
		UpgradeSettings settings = upgrades[level];
		
		indicator.gameObject.SetActive(false);
		ContentManager.instance.configureObject(indicator, settings.indicatorAtlasName, "");
		ContentManager.instance.precacheAnimation(indicator, settings.indicatorAnimationAtlasName);		
		
		_type = ObjectType.COFFEE_MACHINE;
		_state = CoffeeMachineState.IDLE;
	}	
	
	void setState(CoffeeMachineState state, float delay=-1)
	{
		if (delay == -1)
		{
			switch(state)
			{
			case CoffeeMachineState.IDLE:
				if (_currentProduct != OrderProducts.UNKNOWN)
				{
					Inventory.instance.addStuf(_currentProduct.ToString());
					_currentProduct = OrderProducts.UNKNOWN;
				}
				
				indicator.Stop();
				indicator.gameObject.SetActive(false);
				resetToDefaults();
				break;
			case CoffeeMachineState.WORK_NORMAL:
				break;
			default:
				Logger.message(LogLevel.LOG_ERROR, "Unknown coffee machine state - "+state.ToString());
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
		Debug.Log("Play "+animName);
		indicator.gameObject.SetActive(true);
		indicator.Play(animName);
	}	
	
	public override void onAction()
	{
		if (_state == CoffeeMachineState.IDLE)
		{
			if (Inventory.instance.hasStuff("CUP_SMALL") && Inventory.instance.hasStuff("CUP_BIG"))
			{
				string stuff = Inventory.instance.higherPriority("CUP_SMALL", "CUP_BIG");
				Inventory.instance.removeStuff(stuff);
				
				string actionName = "MAKE_"+stuff;
				
				doAction(actionName);
				playTimerAnimation(timerGreenAnimation);
				
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);
				
				if (stuff=="CUP_SMALL")	
					_currentProduct = OrderProducts.COFFEE_SMALL;
				else 
					_currentProduct = OrderProducts.COFFEE_BIG;
				
				setState(CoffeeMachineState.WORK_NORMAL);
				setState(CoffeeMachineState.IDLE, getAction(actionName).actionTime);
			}
			else if (Inventory.instance.hasStuff("CUP_SMALL"))
			{
				Inventory.instance.removeStuff("CUP_SMALL");
				
				string actionName = "MAKE_CUP_SMALL";
				
				doAction(actionName);
				playTimerAnimation(timerGreenAnimation);
				
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);
			
				_currentProduct = OrderProducts.COFFEE_SMALL;
				
				setState(CoffeeMachineState.WORK_NORMAL);
				setState(CoffeeMachineState.IDLE, getAction(actionName).actionTime);
			}
			else if (Inventory.instance.hasStuff("CUP_BIG"))
			{
				Inventory.instance.removeStuff("CUP_BIG");
				
				string actionName = "MAKE_CUP_BIG";
				
				doAction(actionName);
				playTimerAnimation(timerGreenAnimation);
				
				if (getAction(actionName).requiredTime > 0)
					PlayerBehaviour.instance.setBusy(getAction(actionName).requiredTime);				
			
				_currentProduct = OrderProducts.COFFEE_BIG;
				
				setState(CoffeeMachineState.WORK_NORMAL);	
				setState(CoffeeMachineState.IDLE, getAction(actionName).actionTime);
			}
			else 
			{
				PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
			}			
		}
	}	
}
