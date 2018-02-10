using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionMove : Move {

	public Vector3 headOffset = Vector3.up * 6; // this should really be a child of human

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
		if (Input.GetKeyDown(KeyCode.T))
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
			transform.parent = charactersNode.transform; // TODO make sure it's starting from parent human? Sometimes the position is wrong
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

			StartCoroutine(AnimateDeactivation());
		}
		else
		{
			Debug.Log("No humanInRange found, can't set parent!");
		}
	}

	IEnumerator AnimateActivation()
	{
		r.enabled = true;
		Vector3 pos = transform.position;
		pos = humanInRange.transform.position + headOffset;
		pos.z = -1;
		transform.position = pos;

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
		//rb.AddForce(((humanInRange.transform.position + headOffset) - transform.position)*100);

		float transmissionZ = -1;
		float interZ = -10;
		float humanZ = 1;

		Vector3 pos;
		Vector3 startPos = transform.position;
		Vector3 targetPos = humanInRange.transform.position + headOffset;

		// t < 1: spiral out
		// t < 2: home in
		float t = 0;
		while (t < 2)
		{
			pos = transform.position;

			// TODO this is linear, make t quadratic? calc correct vel/acc?
			pos.z = transmissionZ + (1 - Mathf.Abs(t - 1))*(t < 1 ? (interZ - transmissionZ) : (interZ - transmissionZ - humanZ));
			float dist = Mathf.Max(0, pos.z - transmissionZ);

			// spiral - TODO: contract earlier?
			float r = (t < 1 ? 1 - (t - 1) * (t - 1) : (t - 2) * (t - 2));
			pos.x = Mathf.Lerp(startPos.x, targetPos.x, Mathf.Clamp01(t)) + Mathf.Sin(t * 20) * r; // reduce if too spinny
			pos.y = Mathf.Lerp(startPos.y, targetPos.y, Mathf.Clamp01(t)) + Mathf.Cos(t * 20) * r;

			transform.position = pos;

			t += (Time.deltaTime); // NOTE: for debugging, we can add less here to slow it down
			yield return null;
		}

		// fade out
		//while (r.material.color.a > 0)
		//{
		//	c = r.material.color;
		//	c.a -= (Time.deltaTime / FadeDuration);
		//	r.material.color = c;

		//	yield return null;
		//}

		c = r.material.color;
		c.a = 0;
		r.material.color = c;

		r.enabled = false;

		rb.velocity = Vector2.zero; // TODO smooth to human head?

		humanInRange.SendMessage("ReceivedTransmission");
	}
}
