using UnityEngine;
using System.Collections;

public class CakeItem : LevelItem
{
	// TODO : move to xml
	public string dependItem;
	
	void Awake()
	{
		base.Awake();
	
		ActionDeprecated act = new ActionDeprecated(this, 0, 0);
		
		act.addDepend(dependItem, "work");
		addAction("work", act);
	}
	
	public override void onAction()
	{
		PlayerBehaviour.instance.setState(PlayerState.DEFAULT);
		
		doAction("work");
		Level.instance.getObject(dependItem).gameObject.GetComponentInChildren<tk2dAnimatedSprite>().Play("Work");
	}
}
