using UnityEngine;
using System.Collections;

[System.Serializable]
public class SimpleObjectDesc
{
	public string graphPointName;
	
	[System.Serializable]
	public class Level
	{
		public string atlasName;
		public string animationName;
		public string spriteName;
		
		public Vector2 position;
	}
	
	public Level[] levels;
}

public class CafeObjectsConfigurator : MonoBehaviour 
{
	public SimpleObjectDesc[] simpleObjects;
}
