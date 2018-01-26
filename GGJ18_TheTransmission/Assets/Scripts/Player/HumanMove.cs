using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *	Basic moves, just walk
 */
public class HumanMove : MonoBehaviour {

	public float Speed = 5;

	private Vector2 inputVector;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// TODO handle indirectly via InputController
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			inputVector.x = -1;
		}
		else if(Input.GetKeyUp(KeyCode.LeftArrow))
		{
			if (!Input.GetKey(KeyCode.RightArrow))
			{
				inputVector.x = 0;
			}
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			inputVector.x = 1;
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			if (!Input.GetKey(KeyCode.LeftArrow))
			{
				inputVector.x = 0;
			}
		}

		Vector2 pos = transform.position;

		pos += (inputVector * Speed * Time.deltaTime);

		transform.position = pos;
	}
}
