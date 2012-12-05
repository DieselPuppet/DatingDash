using UnityEngine;
using System.Collections;

public class ScaleHelper : MonoBehaviour 
{
	void Awake()
	{
		Vector3 position = gameObject.transform.localPosition;
		position.z = (((position.y-gameObject.GetComponent<tk2dSprite>().GetBounds().size.y/4)*0.001f))+.001f;
		gameObject.transform.localPosition = position;			
	}
}
