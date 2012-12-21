using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContentManager : MonoBehaviour 
{	
	private static ContentManager _instance;
	public static ContentManager instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(ContentManager)) as ContentManager;
			}
			
			return _instance;
		}
	}		
	
	private string _atlasPath = "Atlases/";
	private string _animAtlasPath = "Atlases/Animations/";
	
	private Dictionary<string, tk2dSpriteCollection> _spriteCollections = new Dictionary<string, tk2dSpriteCollection>();
	private Dictionary<string, tk2dSpriteAnimation> _spriteAnimations = new Dictionary<string, tk2dSpriteAnimation>();
	
	public void configureObject(tk2dSprite sprite, string resGroup, string spriteName)
	{
		if (!_spriteCollections.ContainsKey(resGroup))
			loadSpriteCollection(resGroup);
		
		sprite.SwitchCollectionAndSprite(_spriteCollections[resGroup].spriteCollection, _spriteCollections[resGroup].spriteCollection.GetSpriteIdByName(spriteName));
	}
	
	public void precacheAnimation(tk2dAnimatedSprite sprite, string animations)
	{		
		if (!_spriteAnimations.ContainsKey(animations))
			loadSpriteAnimation(animations);
		
		
		sprite.anim = _spriteAnimations[animations];
	}
	
	void loadSpriteAnimation(string animation)
	{
		GameObject anim = (GameObject)Resources.Load(_animAtlasPath+animation);
		
		if (anim == null)
			Logger.message(LogLevel.LOG_ERROR, "Animation group "+animation+" not found!");
		
		_spriteAnimations.Add(animation, anim.GetComponent<tk2dSpriteAnimation>());
	}	
	
	void loadSpriteCollection(string resGroup)
	{
		GameObject resourcesGroup = (GameObject)Resources.Load(_atlasPath+resGroup);
		
		if (resourcesGroup == null)
			Logger.message(LogLevel.LOG_ERROR, "Resource group "+resGroup+" not found!");
		
		_spriteCollections.Add(resGroup, resourcesGroup.GetComponent<tk2dSpriteCollection>());
	}
	
	public TextAsset getTextAsset(string filename)
	{
		return new UnityEngine.TextAsset();
	}
}
