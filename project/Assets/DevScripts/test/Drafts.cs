using UnityEngine;
using System.Collections;

namespace RefactorDraft
{
	public enum Build
	{
		DEVELOPMENT,
		PRODUCTION
	}
	
	// TODO : add ZSortHelper stuff
	public class TransformProxy
	{
		public TransformProxy()
		{
		}
	}
	
	public class SpriteProxy
	{
		public SpriteProxy(GameObject ownerGO, string atlasName, string spriteName)
		{
			_sprite = ownerGO.AddComponent<tk2dAnimatedSprite>();
			
			ContentManager.instance.configureObject(_sprite, atlasName, spriteName);
			
			_size = new Vector2(_sprite.GetBounds().size.z, _sprite.GetBounds().size.y);
		}
		
		tk2dAnimatedSprite _sprite;
		
		private Vector2 _size;
		public Vector2 size
		{
			get 
			{
				return _size;
			}
		}
	}
	
	public class ColliderProxy
	{
		public ColliderProxy(GameObject ownerGO, Vector2 size)
		{
			_collider = ownerGO.AddComponent<BoxCollider>();
			_collider.size = size;	
		}
		
		BoxCollider _collider;
	}
	
	public class LevelObject : MonoBehaviour
	{		
		private TransformProxy _transform;
		private ColliderProxy _collider;
		private SpriteProxy _sprite;
		
		public virtual void onTouch() {}
		public virtual void onAction() {}
	}
	
	public class LevelManager : MonoBehaviour
	{
		void Awake()
		{
			StartCoroutine(simulate());
		}
		
		// move to Level class
		IEnumerator simulate()
		{
			// while (isActive) {}
			yield return null;
		}
		
		// build // if DEVELOPMENT
		// load
		// save
	}
}