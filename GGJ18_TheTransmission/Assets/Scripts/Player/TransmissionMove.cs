using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionMove : Move {

	public Vector3 headOffset = Vector3.up * 6;

	public float Speed = 10;
	public float MaxSpeed = 10;

	public float FadeDuration = 2f;

	private Rigidbody2D rb;
	private Renderer r;

	private GameObject humanInRange;

	private float originalAlpha;

	// Use this for initialization
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		r = GetComponent<Renderer>();

		originalAlpha = r.material.color.a;
	}

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical);

        rb.AddForce(movement * Speed);
		rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, 0, MaxSpeed);
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
		base.Activate();

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

		StartCoroutine(AnimateActivation());
	}

	public override void Deactivate()
	{
		this.enabled = false;

		if (humanInRange != null) {
			transform.parent = humanInRange.transform;
		}
		else
		{
			Debug.Log("No humanInRange found, can't set parent!");
		}

		StartCoroutine(AnimateDeactivation());
	}

	IEnumerator AnimateActivation()
	{
		r.enabled = true;

		rb.AddForce(Vector2.up * 300);

		Color c;

		while (r.material.color.a < originalAlpha)
		{
			c = r.material.color;
			c.a += (Time.deltaTime / FadeDuration);
			r.material.color = c;

			yield return null;
		}

		c = r.material.color;
		c.a = originalAlpha;
		r.material.color = c;
	}

	IEnumerator AnimateDeactivation()
	{
		Color c;

		rb.velocity = Vector2.zero;
		rb.AddForce(((humanInRange.transform.position + headOffset) - transform.position)*100);

		while (r.material.color.a > 0)
		{
			c = r.material.color;
			c.a -= (Time.deltaTime / FadeDuration);
			r.material.color = c;

			yield return null;
		}

		c = r.material.color;
		c.a = 0;
		r.material.color = c;

		r.enabled = false;

		rb.velocity = Vector2.zero; // TODO smooth to human head?
	}
}
