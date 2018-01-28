using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTiggerScript : MonoBehaviour {

    public string DeathName = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("human"))
        {
            NormalTextBox.Instance.Death(DeathName);
        }
    }
}
