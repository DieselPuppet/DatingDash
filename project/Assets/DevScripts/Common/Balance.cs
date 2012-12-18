using UnityEngine;
using System.Collections;

// TODO : will be placed in XML
public enum OrderItem
{
	ORANGE,
	ORANGE_PIPE1,
	ORANGE_PIPE2,
	APPLE,
	APPLE_PIPE1,
	APPLE_PIPE2,
	FRUIT_CAKE
}

public class Balance : MonoBehaviour 
{
	public static Balance instance;
	
	void Awake()
	{
		instance = this;
	}
	
	public CustomerDesc[] customers; 
	
	// CustomersFactory
	public CustomerDesc getRandomDesc()
	{
		return customers[Random.Range(0, customers.Length)];
	}
}
