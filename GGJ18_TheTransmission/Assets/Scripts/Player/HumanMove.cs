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

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
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
		
		if (Input.GetKeyDown(KeyCode.T))
		{
			Transmit();
		}
	}

	void Walk() {
		inputVector.x = Input.GetAxis("Horizontal"); // TODO smooth this? retriggered?

		Vector2 pos = transform.position;
		float direction;

		pos += (inputVector * Speed * Time.deltaTime);

		rb.velocity = Speed * inputVector;
		if (rb.velocity.magnitude > 0.01)
		{
			GetComponent<Animator>().SetFloat("speed", rb.velocity.magnitude);
			if (rb.velocity.x > 0)
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
			Debug.Log("speed 0");
			GetComponent<Animator>().SetFloat("speed", 0);
			direction = 0;
		}

		Quaternion targetRotation = Quaternion.AngleAxis(180 - 89.9f * direction, Vector3.up); // TODO target, turn over time
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.8f);
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

	public void ReceivedTransmission()
	{
		GetComponent<Animator>().SetTrigger("receive");
		//Activate();
	}
}
