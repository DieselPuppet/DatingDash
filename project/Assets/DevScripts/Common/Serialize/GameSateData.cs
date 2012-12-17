using UnityEngine;
using System.Collections;

public class GameSateData : MonoBehaviour {

   public ItemDesc myData; 
   private string _data; 


   void Awake () 
	{ 
	  myData=new ItemDesc("atlas", "something");
    }
	
	public string Data
	{
		get{return _data;}
		set{_data = value;}
	}
	public void SaveData()
	{
		 // Time to creat our XML! 
	     _data = GameStateXML.SerializeObject(myData,"ManagerData"); 
	     // This is the final resulting XML from the serialization process 
	     GameStateXML.CreateXML("SaveData.xml",_data);
		Debug.Log(_data);
	}

	public void LoadData()
	{
		_data = GameStateXML.LoadXML("SaveData.xml");
		if(_data.ToString() != "") 
	    {
	      myData = (ItemDesc)GameStateXML.DeserializeObject(_data,"ManagerData");
	    } 
		
		Debug.Log(_data);
	}
	
	void OnGUI()
	{
		if(GUILayout.Button("Load"))
		{
			LoadData();
		}
		
		if(GUILayout.Button("Save"))
		{
			SaveData();
		}
	}
}
