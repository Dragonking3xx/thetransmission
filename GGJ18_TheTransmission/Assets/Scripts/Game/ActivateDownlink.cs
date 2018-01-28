using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDownlink : MonoBehaviour {

	public GameObject StartElevator;
	public GameObject GoalElevator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate()
	{
		Elevator start = StartElevator.GetComponent<Elevator>();
	}
}
