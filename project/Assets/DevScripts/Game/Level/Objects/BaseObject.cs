using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ObjectType
{
	// cafe objects
	APPLE,
	ORANGE,
	FRUIT_CAKE,
	CROISSANT,
	BLENDER,
	CUP_BIG,
	CUP_SMALL,
	PIPES1,
	PIPES2,
	COFFEE_MACHINE,
	CAKE,
	CAKE_PLATE1,
	CAKE_PLATE2
}

[System.Serializable]
public class GraphicsSettings
{
	public string level;
	public string spriteAtlas;
	public string animationAtlas;
	public string defaultSprite;
}

public abstract class BaseObject : MonoBehaviour
{	
	public GraphicsSettings[] graphicSettings;
	
	public string pointInGraph;
	
	public Action[] actions;
	private Dictionary<string, Action> _actions = new Dictionary<string, Action>();
	
	protected ObjectType _type;
	public ObjectType type
	{
		get 
		{
			return _type;
		}
	}
	
	protected tk2dAnimatedSprite _sprite;
	public tk2dAnimatedSprite sprite
	{
		get 
		{
			return _sprite;
		}
	}		
	
	void Awake()
	{
		buildObject(0);
		
		foreach (Action action in actions)
		{
			action.setOwner(this);			
			_actions.Add(action.name, action);
		}
	}
	
	protected virtual void buildObject(int level)
	{
		_sprite = gameObject.GetComponent<tk2dAnimatedSprite>();
		
		GraphicsSettings settings = graphicSettings[level];
		
		ContentManager.instance.configureObject(_sprite, settings.spriteAtlas, settings.defaultSprite);
		ContentManager.instance.precacheAnimation(_sprite, settings.animationAtlas);	
		
		Vector3 position = gameObject.transform.localPosition;
		position.z = (((position.y-_sprite.GetBounds().size.y/2)*0.01f))+.01f;
		gameObject.transform.localPosition = position;				
		
		if (gameObject.GetComponent<BoxCollider>() == null)
		{
			BoxCollider box = gameObject.AddComponent<BoxCollider>();
			box.size = new Vector3(_sprite.GetBounds().size.x, _sprite.GetBounds().size.y, 1);
		}		
	}
	
	protected virtual void onTouch()
	{
		PlayerBehaviour.instance.moveTo(this);
	}	
	
	public virtual void onAction()
	{
	}
	
	// TODO : add proxies
	public virtual void setupProxies()
	{
	}	
	
	// click callback
	public void OnMouseDown()
	{
		onTouch();
	}		
	
	protected void doAction(string name)
	{
		if (!_actions.ContainsKey(name))
		{
			Logger.message(LogLevel.LOG_ERROR, "Unknown action - "+name);
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
		}

		return _actions[name];
	}
}