using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	public GameObject FollowTarget;

	public float OffsetY = 10;
	public float SmoothTime = 1.0f;

	// TODO contraints?

	// Use this for initialization
	void Start () {
		if(FollowTarget)
		{
			// start: snap to target
			Vector3 pos = FollowTarget.transform.position;
			pos.y += OffsetY;
			pos.z = transform.position.z;

			transform.position = pos;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (FollowTarget != null)
		{
			Vector3 pos = transform.position;
			Vector3 followPos = ClampToFloor(FollowTarget.transform.position);
			followPos.z = pos.z; // stay on camera plane

			// smooth follow
			pos = Vector3.Lerp(pos, followPos, SmoothTime * Time.deltaTime);

			transform.position = pos;
			//transform.LookAt(FollowTarget.transform.position);
		}
	}

	Vector3 ClampToFloor(Vector3 position)
	{
		// TODO
		return position + Vector3.up * OffsetY;
	}
}
