using UnityEngine;
using System.Collections;

public enum ComponentType
{
	ANIMATION,
	SOUND
}

[System.Serializable]
public class ActionComponent
{
	public ComponentType type;
	public string assets;
}

[System.Serializable]
public class Action
{
	public string name;
	
	public ActionComponent[] components;
	public float actionTime;
	public float requiredTime;
	
	BaseObject _owner;
	public void setOwner(BaseObject o)
	{
		_owner = o;
	}
	
	public void start()
	{
		foreach(ActionComponent comp in components)
		{
			if (comp.type == ComponentType.ANIMATION)
			{
				_owner.sprite.Stop();
				_owner.sprite.Play(comp.assets);
			}
			else if (comp.type == ComponentType.SOUND)
			{
			}
		}	
		
		if (requiredTime > 0)
			PlayerBehaviour.instance.setBusy(requiredTime);
		
		_owner.onDoAction(name);
	}
}
