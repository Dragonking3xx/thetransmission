using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAction : Action {

	public bool playerInsideElevator = false;
	public bool playerInteractionEnabled = true;

	public float Depth = 10;

	public override void Update()
	{
		if (!playerInteractionEnabled) return;

		if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
		{
			DoAction();
		}

		if (targetInRange != null && targetInRange.tag == "elevator")
		{
			Elevator elevator = targetInRange.GetComponent<Elevator>();

			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				if (elevator != null && elevator.UpLink != null)
				{
					Debug.Log("foo");
					StartCoroutine(ChangeFloor(elevator.UpLink));
				}
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				if (elevator != null && elevator.Downlink != null)
				{
					StartCoroutine(ChangeFloor(elevator.Downlink));
				}
			}
		}
	}

	public override void DoAction()
	{
		if (targetInRange != null)
		{
			Elevator elevator = targetInRange.GetComponent<Elevator>();
			if (elevator != null)
			{
				if (!playerInsideElevator)
				{
					StepIntoElevator();
					playerInsideElevator = true;
				}
				else
				{
					StepOutOfElevator();
					playerInsideElevator = false;
				}
			}
		}
	}

	private IEnumerator ChangeFloor(GameObject goal)
	{
		Debug.Log("ChangeFloor");

		Elevator elevator = goal.GetComponent<Elevator>();

		GameObject player = gameObject; // Script is on moving human
		if (player.tag == "human")
		{
			float targetDirection = (goal.transform.position.y > player.transform.position.y ? 1 : -1);
			GetComponent<Collider2D>().enabled = false; // Pass through floors
			GetComponent<Rigidbody2D>().simulated = false;

			Vector3 playerPosition;

			// move?
			while ((targetDirection > 0 && player.transform.position.y < goal.transform.position.y) || (targetDirection < 0 && player.transform.position.y > goal.transform.position.y))
			{
				playerPosition = player.transform.position;
				playerPosition.y += (targetDirection * elevator.Speed * Time.deltaTime);
				Debug.Log("Player y: " + playerPosition.y);
				player.transform.position = playerPosition;
				yield return null;
			}

			// arrived
			Debug.Log("Arrived!");

			playerPosition = player.transform.position;
			playerPosition.y = goal.transform.position.y; // TODO offset?
			player.transform.position = playerPosition;

			GetComponent<Collider2D>().enabled = true;
			GetComponent<Rigidbody2D>().simulated = true;
		}
		else
		{
			Debug.Log("Player does not have human tag");
		}
	}

	private void StepOutOfElevator()
	{
		// TODO play anim
		//transform.Translate(Vector3.back);
		StartCoroutine(MoveZ(-Depth, 0.5f));

		GetComponent<Move>().enabled = true;
	}

	private void StepIntoElevator()
	{
		// TODO play anim
		//transform.Translate(Vector3.forward);
		StartCoroutine(MoveZ(Depth, 0.5f));

		GetComponent<Move>().enabled = false;
	}

	IEnumerator MoveZ(float z, float duration)
	{
		float targetZ = transform.position.z + z;

		bool simple = true;
		if (!simple)
		{	
			float direction = z > 0 ? 1 : -1;
			while ((direction > 0 && transform.position.z < targetZ) || (direction < 0 && transform.position.z > targetZ))
			{
				transform.Translate(Vector3.forward * direction * Time.deltaTime / duration);
				Debug.Log("MoveZ: " + transform.position.z);
				yield return null;
			}
		}

		Vector3 pos = transform.position;
		pos.z = targetZ;
		transform.position = pos;
	}

}
