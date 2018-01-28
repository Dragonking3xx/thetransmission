using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {

	public string targetTag;

	protected GameObject targetInRange = null;

	public virtual void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			DoAction();
		}
	}

	public virtual void DoAction()
	{
		// override
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == targetTag)
		{
			targetInRange = other.gameObject;
			Debug.Log("Action " + targetTag + " target in range: " + other);
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject == targetInRange)
		{
			Debug.Log("Action " + targetTag + " target out of range: " + other);
			targetInRange = null;
		}
	}
}
