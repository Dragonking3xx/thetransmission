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
		base.Activate();

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
		// TODO handle indirectly via InputController or something
		//if(Input.GetKeyDown(KeyCode.LeftArrow))
		//{
		//	inputVector.x = -1;
		//}
		//else if(Input.GetKeyUp(KeyCode.LeftArrow))
		//{
		//	if (!Input.GetKey(KeyCode.RightArrow))
		//	{
		//		inputVector.x -= Time.deltaTime;
		//	}
		//}
		//else if (Input.GetKeyDown(KeyCode.RightArrow))
		//{
		//	inputVector.x = 1;
		//}
		//else if (Input.GetKeyUp(KeyCode.RightArrow))
		//{
		//	if (!Input.GetKey(KeyCode.LeftArrow))
		//	{
		//		inputVector.x -= Time.deltaTime;
		//	}
		//}

		inputVector.x = Input.GetAxis("Horizontal"); // TODO smooth this? retriggered?

		Vector2 pos = transform.position;
		float direction;

		pos += (inputVector * Speed * Time.deltaTime);

		transform.position = pos;

		if (inputVector.magnitude > 0.01)
		{
			GetComponent<Animator>().SetFloat("speed", inputVector.magnitude);
			if(inputVector.x > 0)
			{
				direction = 1;
			}
			else
			{
				direction = -1;
			}
		}
		else
		{
			GetComponent<Animator>().SetFloat("speed", 0);
			direction = 0;
		}

		// transform.rotation = Quaternion.AngleAxis(180 - 90 * direction, Vector3.up); // TODO target, turn over time
		// BUG: this makes human fall through the floor, why?
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
				GetComponent<Animator>().SetTrigger("transmit");

				GameController.Instance.Player = transmission;

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

				GetComponent<Animator>().SetTrigger("receive");

				GameController.Instance.Player = gameObject;

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
