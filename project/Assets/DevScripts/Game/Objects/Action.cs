using UnityEngine;
using System.Collections;

public class DependLink
{
	public DependLink(string t, string n)
	{
		_target = t;
		_name = n;
	}
	
	private string _target;
	public string target
	{
		get 
		{
			return _target;
		}
	}
	
	private string _name;
	public string name
	{
		get 
		{
			return _name;
		}
	}
}

// Shared Action class for Customers & Objects
public class Action
{
	//bool isStarted;
	//bool isDone;
	
	public class ActionComponent
	{
		public enum Type
		{
			ANIM,
			SOUND
		}
		
		private Type _type;
		public Type type
		{
			get 
			{
				return _type;
			}
		}
		
		private string _groupAsset;
		public string groupAsset
		{
			get 
			{
				return _groupAsset;
			}
		}
		
		private string _asset;
		public string asset
		{
			get 
			{
				return _asset;
			}
		}
		
		public ActionComponent(Type t, string g, string a)
		{
			_type = t;
			_asset = a;
			_groupAsset = g;
		}
	}
	
	private float _time;
	public float time
	{
		get 
		{
			return _time;
		}
	}
	
	// for Player
	private float _reqTime;
	public float reqTime
	{
		get 
		{
			return _reqTime;
		}
	}
	
	LevelItem _owner;
	
	ArrayList _components;
	
	ArrayList _depends;
	
	public Action(LevelItem owner, float t, float rt)
	{				
		_components = new ArrayList();
		_depends = new ArrayList();
		
		_time = t;
		_reqTime = rt;
		
		_owner = owner;
	}
	
	public void addDepend(string t, string n)
	{
		_depends.Add(new DependLink(t, n));
	}
	
	public void pushComponent(ActionComponent comp)
	{	
		if (comp.type == ActionComponent.Type.ANIM)
			ContentManager.instance.precacheAnimation(_owner.sprite, comp.groupAsset);
		
		_components.Add(comp);
	}
	
	public void start()
	{
		foreach(ActionComponent comp in _components)
		{
			if (comp.type == ActionComponent.Type.ANIM)
			{
				_owner.sprite.Play(comp.asset);
			}
			else if (comp.type == ActionComponent.Type.SOUND)
			{
			}
		}
		
		//foreach(DependLink link in _depends)
		//{
		//	LevelItem item = Level.instance.getObject(link.target);
		//	item.doAction(link.name);
		//}
	}
	
	public void cancel()
	{
		foreach(ActionComponent comp in _components)
		{
			if (comp.type == ActionComponent.Type.ANIM)
			{
			}
			else if (comp.type == ActionComponent.Type.SOUND)
			{
			}
		}		
	}
	
	public void onPause()
	{
		foreach(ActionComponent comp in _components)
		{
			if (comp.type == ActionComponent.Type.ANIM)
			{
			}
			else if (comp.type == ActionComponent.Type.SOUND)
			{
			}
		}		
	}
	
	public void onResume()
	{
		foreach(ActionComponent comp in _components)
		{
			if (comp.type == ActionComponent.Type.ANIM)
			{
				
			}
			else if (comp.type == ActionComponent.Type.SOUND)
			{
				
			}
		}		
	}
}
