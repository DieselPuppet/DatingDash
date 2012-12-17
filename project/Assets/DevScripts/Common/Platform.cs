using UnityEngine;
using System.Collections;

public enum PlatformType
{
	PLATFORM_IPAD2,
	PLATFORM_IPAD3,
	PLATFORM_WIN,
	PLATFORM_MACOS,
	PLATFORM_UNKNOWN
}

public enum VideoMode
{
}

public class HardwareConfig
{
	public HardwareConfig()
	{
		
	}
	
	
}

public class Platform : MonoBehaviour 
{
	private static Platform _instance;
	public static Platform instance
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(Platform)) as Platform;
			}
			
			return _instance;
		}
	}		
	
	private PlatformType _platform;
	public PlatformType platform
	{
		get
		{
			return _platform;
		}
	}
		
	
	private bool _isEditor;
	public bool isEditor
	{
		get 
		{
			return _isEditor;
		}
	}
	
	private string _osType;
	public string osType
	{
		get 
		{
			return _osType;
		}
	}
	
	private int _cpuCount;
	public int cpuCount
	{
		get 
		{
			return _cpuCount;
		}
	}
	
	public void init()
	{		
		_platform = PlatformType.PLATFORM_UNKNOWN;
		
		_osType = SystemInfo.operatingSystem;
		_cpuCount = SystemInfo.processorCount;
		_isEditor = Application.isEditor;
		
		Logger.message(LogLevel.LOG_INFO, "Platform : "+platform.ToString()+". isEditor = "+_isEditor.ToString());
	}
}
