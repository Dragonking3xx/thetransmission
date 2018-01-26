using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	public GameObject FollowTarget;

	// TODO smoothing speed etc.
	// TODO contraints?

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (FollowTarget != null)
		{
			Vector3 pos = transform.position;

			pos.x = FollowTarget.transform.position.x;
			pos.y = FollowTarget.transform.position.y;
			// TODO smooth

			transform.position = pos;
		}
	}
}
