using UnityEngine;
using System.Collections;

namespace RefactorDraft
{
	public enum Build
	{
		DEVELOPMENT,
		PRODUCTION
	}
	
	public class SpriteProxy
	{
	}
	
	public class LevelObject : MonoBehaviour
	{
		private Vector2 _pos;
		public Vector2 pos 
		{
			get 
			{
				return _pos;
			}
		}
		
		private SpriteProxy _sprite;
	}
	
	public class LevelManager
	{
		// build // if DEVELOPMENT
		// load
		// save
	}
}