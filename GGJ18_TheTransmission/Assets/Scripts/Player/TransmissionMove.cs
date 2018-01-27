using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionMove : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed = 10;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical);

        rb.AddForce(movement * speed);

    }

}
