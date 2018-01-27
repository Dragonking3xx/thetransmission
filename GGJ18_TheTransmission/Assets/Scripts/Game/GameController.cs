using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private static GameController instance;
	public static GameController Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameController();
			}

			return instance;
		}

		private set
		{
			instance = Instance;
		}
	}


    public GameObject gui;

//	private InputController inputcontroller;

	public GameObject Player; // target for input, this can change (disembodied transmission, different overtakeable characters)
	private List<Item> items;
	// TODO Enemies etc.


	// Use this for initialization
	void Start () {

        Player = GameObject.FindGameObjectWithTag("transmission");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DialogEnd()
    {

    }
    public void Death(string DeathText)
    {


        NormalTextBox.Instance.Death(DeathText);
    }
}
