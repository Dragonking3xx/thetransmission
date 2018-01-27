using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract Base class for different moves
public class Move : MonoBehaviour {
	// derive and fill

	public virtual void Activate() {
		// TODO does this belong here?
		FollowCam followCam = Camera.main.GetComponent<FollowCam>();
		if(followCam != null)
		{
			followCam.FollowTarget = gameObject;
		}
	}

	public virtual void Deactivate() { }
}
