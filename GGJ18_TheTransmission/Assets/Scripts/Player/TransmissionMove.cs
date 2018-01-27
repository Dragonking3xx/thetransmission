using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionMove : Move {

    private Rigidbody2D rb;
    public float speed = 10;

	private GameObject humanInRange = null;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	

	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "human")
		{
			Debug.Log("human in range: " + other.name);
			humanInRange = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "human" && humanInRange == other.gameObject)
		{
			Debug.Log("human out of range: " + other.name);
			humanInRange = null;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T) || Input.GetButton("Jump"))
		{
			TryReceive();
		}
	}

	void TryReceive()
	{
		if(humanInRange)
		{
			humanInRange.SendMessage("Receive", gameObject);
		}
	}

	public override void Activate()
	{
		this.enabled = true;

		GameObject charactersNode = GameObject.Find("Characters");
		if (charactersNode != null)
		{
			transform.parent = charactersNode.transform;
		}
		else
		{
			Debug.Log("Could not find the level's Characters Node!");
			transform.parent = null; // global root?
		}

		// TODO activation animation
		GetComponent<Renderer>().enabled = true;
	}

	public override void Deactivate()
	{
		this.enabled = false;

		if (humanInRange != null) {
			transform.parent = humanInRange.transform;
		}
		else
		{
			Debug.Log("Nu humanInRange found, can't set parent!");
		}

		// TODO deactivating animation
		GetComponent<Renderer>().enabled = false;
	}
}
