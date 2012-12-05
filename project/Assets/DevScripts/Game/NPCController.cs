using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDesc
{
	public Action[] action;
	public Interest interests;
	
	public int moodPercent;
	public int moodDownSpeed;
	
	// for launch rand animation in WaitingMode
	public int randAnimRangeMin;
	public int randAnimRangeMax;	
	
	public int orderTime;
	public int meatTime;
	//time params etc
	// link to gfx
}

public class NPCFactory : MonoBehaviour
{
	
	
	Dictionary<string, NPCDesc> _entityTemplates;
	
	public void parseTemplates(TextAsset sources)
	{
		_entityTemplates = new Dictionary<string, NPCDesc>();
		
		// load from xml
	}		
	
	public NPC createNPC(string type)
	{
		if (!_entityTemplates.ContainsKey(type))
		{
			Logger.message(LogLevel.LOG_ERROR, "Unknown NPC type - "+type);
		}
		
		return new NPC(_entityTemplates[type]);
	}
}
