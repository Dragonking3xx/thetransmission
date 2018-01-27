using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *	Basic moves, just walk
 */
public class HumanMove : Move {

	public float Speed = 5;

	private Vector2 inputVector;

	// Use this for initialization
	void Start () {
		
	}

	public override void Activate()
	{
		this.enabled = true;

		// TODO activation animation
	}

	public override void Deactivate()
	{
		this.enabled = false;

		// TODO deactivating animation
	}

	// Update is called once per frame
	void Update() {
		Walk();
		
		if (Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Jump"))
		{
			Transmit();
		}
	}

	void Walk() {
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

	void Transmit()
	{
		GameObject transmission = GameObject.FindGameObjectWithTag("transmission");
		if (transmission != null)
		{
			Move transmissionMove = transmission.GetComponent<Move>();
			if(transmissionMove != null)
			{
				transmissionMove.Activate();

				Deactivate();
			}
			else
			{
				Debug.Log("No Move script found on transmission object!");
			}
		}
		else
		{
			Debug.Log("Object with tag transmission not found!");
		}
	}

	void Receive(GameObject transmission)
	{
		if (transmission != null)
		{
			Move transmissionMove = transmission.GetComponent<Move>();
			if (transmissionMove != null)
			{
				transmissionMove.Deactivate();

				Activate();
			}
			else
			{
				Debug.Log("No Move script found on transmission object!");
			}
		}
		else
		{
			Debug.Log("transmission object not received!");
		}
	}
}
