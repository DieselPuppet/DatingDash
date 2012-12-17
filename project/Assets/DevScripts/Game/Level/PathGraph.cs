using UnityEngine;
using System.Collections;
using System.Xml;

public class GraphNode
{	
	public GraphNode(Vector2 p, string n)
	{
		_pos = p;
		_name = n;
	}
	
	private Vector2 _pos;
	public Vector2 pos
	{
		get 
		{
			return _pos;
		}
	}
	
	private string _name;
	public string name
	{
		get 
		{
			return _name;
		}
	}
}

public class PathGraph : MonoBehaviour
{	
	private static PathGraph _instance;
	public static PathGraph instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(PathGraph)) as PathGraph;
			}
			
			return _instance;
		}
	}	

	const int kNoPath = -1;
	const float kInfinity = 999999999f;
	
	int _arraySize;
	
	GraphNode[] _points;
	float[,] _linksLength;
	float[,] _pathLength;
	int[,] _paths;
	
	public void buildGraph(TextAsset sources)
	{			
		createFromXml(sources.text);		
	}
	
	void createFromXml(string xml)
	{
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(xml);
		
		XmlNode rootNode = doc.SelectSingleNode("root");
		
		float centerX = int.Parse(rootNode.Attributes["center_x"].Value);
		float centerY = int.Parse(rootNode.Attributes["center_y"].Value);
		
		_arraySize = int.Parse(rootNode.Attributes["points_quantity"].Value);
		
		_points = new GraphNode[_arraySize];
		_linksLength = new float[_arraySize, _arraySize];
		_pathLength = new float[_arraySize, _arraySize];
		_paths = new int[_arraySize, _arraySize];
		
		for (int i = 0; i < _arraySize; i++)
		{
			for (int j = 0; j < _arraySize; j++)
			{
				_linksLength[i,j] = kInfinity;
			}
		}
		
		// parse points
		XmlNodeList pointsList = rootNode.SelectSingleNode("points").ChildNodes;
		for (int i = 0; i < _arraySize; i++)
		{	
			float x = centerX+int.Parse(pointsList[i].Attributes["x"].Value);
			float y = centerY-int.Parse(pointsList[i].Attributes["y"].Value);
				
			string name = string.Empty;
			
			if (pointsList[i].ChildNodes.Count > 0)
			{				
				name = pointsList[i].SelectSingleNode("properties").Attributes["name"].Value;
			}				
			
			_points[i] = new GraphNode(new Vector2(x, y), name);		
		}
		
		// parse links
		XmlNodeList links = rootNode.SelectSingleNode("links").ChildNodes;
		for (int i = 0; i < links.Count; i++)
		{
			int p1 = int.Parse(links[i].Attributes["p1"].Value);
			int p2 = int.Parse(links[i].Attributes["p2"].Value);
			
			Vector2 tmpVec = _points[p1].pos - _points[p2].pos;
			_linksLength[p1, p2] = tmpVec.SqrMagnitude();
			_linksLength[p2, p1] = tmpVec.SqrMagnitude();
		}

		// build pathes
		buildPaths();
	}
	
	void buildPaths()
	{
		for (int i = 0; i < _arraySize; i++)
		{
			for (int j = 0; j < _arraySize; j++)
			{
				_pathLength[i,j] = _linksLength[i,j];
				if (_pathLength[i, j] == kInfinity)
				{
					_paths[i,j] = kNoPath;
				}
				else 
				{
					_paths[i,j] = j;
				}
			}
		}
		
		for (int i = 0; i < _arraySize; i++)
		{
			for (int j = 0; j < _arraySize; j++)
			{
				for (int k = 0; k < _arraySize; k++)
				{
					if (i != j && _pathLength[j,i] != kInfinity && i != k && _pathLength[i,k] != kInfinity &&
						(_pathLength[j,k] == kInfinity || _pathLength[j,k] > _pathLength[j,i] + _pathLength[i,k]))
					{
						_paths[j,k] = _paths[j,i];
						_pathLength[j,k] = _pathLength[j,i] + _pathLength[i,k];
					}
				}
			}
		}
	}
	
	public ArrayList getPath(Vector2 vectorFrom, Vector2 vectorTo)
	{
		int fromIndex = 0;
		int toIndex = 0;
		
		for (int i = 0; i < _arraySize; i++)
		{
			if (_points[i].pos == vectorFrom)
				fromIndex = i;
			
			if (_points[i].pos == vectorTo)
				toIndex = i;
		}
						
		ArrayList path = new ArrayList();
		
		int currentIndex = fromIndex;
		
		do
		{
			path.Add(_points[currentIndex].pos);
			currentIndex = _paths[currentIndex, toIndex];
		}
		while ((currentIndex != toIndex) && (currentIndex != kNoPath));
		
		path.Add(vectorTo);
		
		return path;
	}
	
	public Vector2 getPointByName(string name)
	{
		foreach (GraphNode node in _points)
		{
			if (node.name == name)
				return node.pos;
		}
		
		return Vector2.zero;
	}	
}
