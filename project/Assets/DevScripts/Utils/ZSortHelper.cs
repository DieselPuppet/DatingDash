using UnityEngine;
using System.Collections;

public class ZSortHelper : MonoBehaviour 
{
	void Awake()
	{
		Vector3 position = gameObject.transform.localPosition;
		position.z = (((position.y-gameObject.GetComponent<tk2dSprite>().GetBounds().size.y/2)*0.01f))+.01f;
		gameObject.transform.localPosition = position;			
	}
}
