using UnityEngine;
using System.Collections;

public class CakeItem : LevelItem
{
	// TODO : move to xml
	public string dependItem;
	
	// tmp (will be placed in xml)
	
	void Awake()
	{
		base.Awake();
		
		Action act = new Action(this, 0, 0);
		
		act.addDepend(dependItem, "work");
		addAction("work", act);
	}
	
	public override void onAction()
	{
		doAction("work");
		Level.instance.getObject(dependItem).gameObject.GetComponentInChildren<tk2dAnimatedSprite>().Play("Work");
	}
}
