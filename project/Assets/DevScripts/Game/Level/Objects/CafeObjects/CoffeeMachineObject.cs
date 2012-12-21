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
		
		public Vector2 indicatorOffset = Vector2.zero;
		
		public float brockenTime;		
	}	
	
	public UpgradeSettings[] upgrades;
	UpgradeSettings usettings;
	
	public tk2dAnimatedSprite indicator;
	public string timerGreenAnimation;	
	public string timerRedAnimation;	
	bool _hasCoffee = false;

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

		usettings = upgrades[level];

		indicator.gameObject.SetActive(false);

		indicator.gameObject.transform.Translate(usettings.indicatorOffset.x, usettings.indicatorOffset.y, 0);

		ContentManager.instance.configureObject(indicator, usettings.indicatorAtlasName, "");
		ContentManager.instance.precacheAnimation(indicator, usettings.indicatorAnimationAtlasName);		

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

			case CoffeeMachineState.WORK_DANGER:
				playTimerAnimation(timerRedAnimation);
				setState(CoffeeMachineState.BROCKEN, usettings.brockenTime);
				break;				

			case CoffeeMachineState.BROCKEN:
				_sprite.Stop();

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
		indicator.gameObject.SetActive(true);
		indicator.Play(animName);
	}	

	public override void onDoAction(string actionName)
	{
		setState(CoffeeMachineState.WORK_DANGER, getAction(actionName).actionTime);
	}

	public override void onAction()
	{
		if (_state == CoffeeMachineState.IDLE)
		{
			if (_hasCoffee)
			{
				_hasCoffee = false;

				resetToDefaults();

				Inventory.instance.addStuf(_currentProduct.ToString());
			}
			else 
			{
				if (Inventory.instance.hasStuff("CUP_SMALL") || Inventory.instance.hasStuff("CUP_BIG"))
				{
					string sources;

					if (Inventory.instance.hasStuff("CUP_SMALL") && Inventory.instance.hasStuff("CUP_BIG"))
						sources = Inventory.instance.higherPriority("CUP_SMALL", "CUP_BIG");		
					else if (Inventory.instance.hasStuff("CUP_SMALL"))
						sources = "CUP_SMALL";
					else 
						sources = "CUP_BIG";

					Inventory.instance.removeStuff(sources);

					string actionName = "MAKE_"+sources;

					doAction(actionName);
					playTimerAnimation(timerGreenAnimation);

					if (sources=="CUP_SMALL")	
						_currentProduct = OrderProducts.COFFEE_SMALL;
					else 
						_currentProduct = OrderProducts.COFFEE_BIG;

					setState(CoffeeMachineState.WORK_NORMAL);
				}
			}
		}

		else if (_state == CoffeeMachineState.WORK_NORMAL)
		{
			PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
		}
		else if (_state == CoffeeMachineState.WORK_DANGER)
		{
			setState(CoffeeMachineState.IDLE);

			indicator.Stop();
			indicator.gameObject.SetActive(false);
			CancelInvoke();
			_sprite.Stop();		
		}
		else if (_state == CoffeeMachineState.BROCKEN)
		{
			PlayerBehaviour.instance.setBusy(2);
			indicator.gameObject.SetActive(false);
		}		

		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
	}		
}
