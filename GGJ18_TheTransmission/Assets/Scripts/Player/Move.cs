using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract Base class for different moves
public abstract class Move : MonoBehaviour {
	// derive and fill

	public abstract void Activate();
	public abstract void Deactivate();

}
