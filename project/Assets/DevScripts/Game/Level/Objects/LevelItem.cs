using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BehaviourType
{
	PROVIDER, 
	FACTORY,
	INTERACTION,
}

public class LevelItem : MonoBehaviour
{	
	public string objectType = "##";
	public string pointName;
	
	public string objectName;
	
	// TODO: private!
	public BehaviourType _behaviour;

	private ItemDesc _cachedDesc;
	
	protected tk2dAnimatedSprite _sprite;
	public tk2dAnimatedSprite sprite
	{
		get 
		{
			return _sprite;
		}
	}	
	
	protected Dictionary<string, Action> _actions = new Dictionary<string, Action>();
	
	enum State
	{
		IDLE,
		WORK
	}
	
	State _curState;
	
	// setState(state, delay);
	// inStateChanged. See BlenderState
	// configure in xml
	
	public void Awake()
	{			
		configureFromDB();
	
		if (_behaviour == BehaviourType.PROVIDER)
		{
			Action act = new Action(this, 5, 0);
			act.pushComponent(new Action.ActionComponent(Action.ActionComponent.Type.ANIM, "SimpleAnimations", objectType+"_work"));
			addAction(objectType+"_work", act);	
		}
		
		_curState = State.IDLE;
		
		Level.instance.pushLevelObject(this);		
	}
	
	void configureFromDB()
	{		
		_cachedDesc = ItemsCollection.instance.getItemDesc(objectType);
	
		if (gameObject.GetComponent<tk2dAnimatedSprite>() == null && _cachedDesc != null)
		{
			_sprite = gameObject.AddComponent<tk2dAnimatedSprite>();
	
			if (_cachedDesc.groupName == "PROVIDER")
				_behaviour = BehaviourType.PROVIDER;
			else if (_cachedDesc.groupName == "INTERACTION")
				_behaviour = BehaviourType.INTERACTION;
	
			ContentManager.instance.configureObject(_sprite, _cachedDesc.atlasName, _cachedDesc.spriteName);
		}
		else
		{
			_sprite = gameObject.GetComponent<tk2dAnimatedSprite>();
		}			
		
		Vector3 position = gameObject.transform.localPosition;
		position.z = (((position.y-_sprite.GetBounds().size.y/2)*0.01f))+.01f;
		gameObject.transform.localPosition = position;			
		
		if (gameObject.GetComponent<BoxCollider>() == null)
		{
			BoxCollider box = gameObject.AddComponent<BoxCollider>();
			box.size = new Vector3(_sprite.GetBounds().size.x, _sprite.GetBounds().size.y, 1);
		}
	}
	
	public void reset()
	{
		_sprite.Stop();
		ContentManager.instance.configureObject(_sprite, _cachedDesc.atlasName, _cachedDesc.spriteName);
	}
	
	public void OnMouseDown()
	{
		onTouch();
	}		
	
	public void addAction(string name, Action action)
	{	
		_actions.Add(name, action);
	}
	
	public void doAction(string name)
	{					
		if (!_actions.ContainsKey(name))
		{
			Logger.message(LogLevel.LOG_ERROR, "Unknown action - "+name+" for item "+objectName);
		}
		else 
		{
			_actions[name].start();
		}
	}
	
	protected Action getAction(string name)
	{
		if (!_actions.ContainsKey(name))
		{
			Logger.message(LogLevel.LOG_ERROR, "Unknown action - "+name);
			return new Action(this, 0, 0);
		}

		return _actions[name];
	}
	
	protected virtual void onTouch()
	{
		PlayerBehaviour.instance.moveTo(this);
	}		
	
	public virtual void onAction()
	{
		if (_behaviour == BehaviourType.PROVIDER)
		{
			if (Inventory.instance.canAddStuff())
			{					
				if (_curState == State.IDLE)
				{
					doAction(objectType + "_work");
				}
				
				Inventory.instance.addStuf(objectType+"_sources");
			}			
		}
		
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
	}	
}
