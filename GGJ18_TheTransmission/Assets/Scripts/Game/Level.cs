using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public const int Preparing = -1;
	public const int StartRoom = 0;
	public const int Room2 = 1;

	public static int[] Steps;
	public int currentStep = -1;

	public GameObject transmission;
	public GameObject satDish;

	// Room 1
	public GameObject Human_01;

	

	void Start()
	{
		// Room 1
		transmission = GameObject.FindGameObjectWithTag("transmission");
		satDish = GameObject.Find("Sat");

		Human_01 = GameObject.Find("Human_01");

		// TODO: After some seconds, fly to first Human (who perhaps presses a button?)
	}

	void Update()
	{
		switch(currentStep)
		{
			case Preparing:
				// Start: fly in
				if (transmission.GetComponent<Rigidbody2D>())
				{
					Vector3 dist = (satDish.transform.position - transmission.transform.position);
					transmission.GetComponent<Rigidbody2D>().velocity = dist * 500;
					currentStep++;
				}
				break;

			case StartRoom:

				break;
			case Room2:
				// TODO elevator enter starts drifting, why?
				break;
				//...
		}
	}

	public void Reset()
	{
		currentStep = 0;
	}
}
