using UnityEngine;
using System.Collections;

public class PathNavigator : MonoBehaviour 
{
	PlayerBehaviour _player;
	
	Transform _characterTransform;
	
	// navigation stuff
	ArrayList _pathArray;	
	float speed = 100;
	Vector2 nextWP;		
	int nextWPIndex = 0;
	
	public float spriteHeight;
	
	void Awake()
	{
		_player = gameObject.GetComponent<PlayerBehaviour>();
		_characterTransform = gameObject.transform;
	}
	
	public void placeIn(Vector3 pos)
	{
		_characterTransform.localPosition = pos;
	}
	
	public void startRoute(LevelItem target)
	{
		Vector2 targetVec = new Vector2(_characterTransform.localPosition.x, _characterTransform.localPosition.y);
		setPath(PathGraph.instance.getPath(targetVec, PathGraph.instance.getPointByName(target.pointName)));		
	}
	
	void setPath(ArrayList pathArray)
	{
		_pathArray = pathArray;
		
		Vector2 v = (Vector2)_pathArray[0];
		_characterTransform.position = new Vector3(v.x, v.y, _characterTransform.position.z);
		
		nextWPIndex = 1;
		nextWP = (Vector2)_pathArray[nextWPIndex];
		
		//_sprite.Play("run_down");
		
		StartCoroutine(moveCharacterCor());
	}	
	
	IEnumerator moveCharacterCor()
    {
		Vector2 v1 = new Vector2(_characterTransform.position.x, _characterTransform.position.y);
		Vector2 v2 = new Vector2(nextWP.x, nextWP.y);
		
        while ((v1 - v2).magnitude > 5F)
        {        			
			Vector3 pos = _characterTransform.position;
			pos.z = (((_characterTransform.position.y-spriteHeight/4)*0.001f))+.001f;
			_characterTransform.position = pos;
			
			_characterTransform.localPosition = Vector3.MoveTowards(_characterTransform.localPosition, new Vector3(nextWP.x, nextWP.y, pos.z), Time.deltaTime*speed);
			
			v1 = new Vector2(_characterTransform.position.x, _characterTransform.position.y);
			v2 = new Vector2(nextWP.x, nextWP.y);		

			
            yield return null;
        }
		
		chooseTarget();
    }		
	
	void chooseTarget()
	{
		if (nextWPIndex < _pathArray.Count-1)
		{
			checkDirection(nextWP, (Vector2)_pathArray[nextWPIndex+1]);
			
			nextWPIndex++;
			nextWP = (Vector2)_pathArray[nextWPIndex];
			
			StartCoroutine(moveCharacterCor());
		}
		else 
		{
			_characterTransform.localPosition = new Vector3(nextWP.x, nextWP.y, _characterTransform.localPosition.z);
			_player.onRouteFinish();
		}
	}
	
	void checkDirection(Vector2 cur, Vector2 next)
	{
		float angle = Mathf.Atan2(cur.y - next.y, cur.x - next.x) * Mathf.Rad2Deg;
		
		//Debug.Log("angle = "+angle);
		
		if (angle > 70 && angle < 110)
		{
			//_sprite.Play("run_down");
		}
		else if (angle >= 110 && angle < 155)
		{
			//_sprite.Play("run_down_right");
		}
		else if (angle >= 155 && angle < -155 )
		{
			//_sprite.Play("run_right");
		}
		else if (angle > -155 && angle < -110)
		{
			//_sprite.Play("run_up_right");
		}
		else 
		{
			//_sprite.Stop();
		}
	}	
}
