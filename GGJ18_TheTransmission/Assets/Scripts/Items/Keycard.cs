using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : Item {

	void PickUp()
	{
		GetComponent<Renderer>().enabled = false;
	}
}
