using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private enum State { TITLE, INGAME, DIALOGUE, DEATH };
	private State currentState = State.INGAME;

	private static GameController instance;
	public static GameController Instance
	{
		get
		{
			return instance;
		}

		private set
		{
			instance = Instance;
		}
	}

	void Awake()
	{
		if(instance)
		{
			Debug.Log("Only one GameController allowed!");
			GameObject.DestroyImmediate(gameObject);
		}
		else
		{
			instance = this;
		}
	}

    public GameObject gui;

//	private InputController inputcontroller;

	public GameObject Player; // target for input, this can change (disembodied transmission, different overtakeable characters)
	private List<Item> items;
	// TODO Enemies etc.


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject); // stay persistent -- needed? On Die/Respawn everything should be reset

		ResetCurrentLevel();
	}
	
	void ResetCurrentLevel()
	{
		Player = GameObject.FindGameObjectWithTag("transmission");
		FollowCam followCam = Camera.main.gameObject.AddComponent<FollowCam>();
		followCam.FollowTarget = Player;

		NormalTextBox.Instance.gameObject.SetActive(false);
	}

	public void ShowDialog(string dialog)
	{
		if(gui != null)
		{
			if (!gui.activeInHierarchy)
			{
				gui.SetActive(true);
			}
			
			// HACK
			if(NormalTextBox.Instance == null)
			{
				GameObject box = gui.transform.Find("Textbox").gameObject;
				box.SetActive(true);
			}

			if (NormalTextBox.Instance != null)
			{
				SetPlayerInteractionEnabled(false);
				NormalTextBox.Instance.loadText(dialog);
			}
		}
	}

	public void DialogEnd()
    {
		SetPlayerInteractionEnabled(true);
    }

    public void Death(string DeathText)
    {
		SetState(State.DEATH);

        NormalTextBox.Instance.Death(DeathText);
    }

	private void SetState(State newState)
	{
		switch(newState)
		{
			case State.TITLE:
				// TODO
				break;
			case State.INGAME:
				SetPlayerInteractionEnabled(true);
				break;
			case State.DIALOGUE:
				SetPlayerInteractionEnabled(false);
				break;
			case State.DEATH:
				SetPlayerInteractionEnabled(false);
				break;
		}

		currentState = newState;
	}

	public void SetPlayerInteractionEnabled(bool enabled)
	{
		if(Player != null)
		{
			Player.GetComponent<Move>().enabled = enabled;
			foreach(Action action in Player.GetComponents<Action>()) // TODO not found via base class?
			{
				action.enabled = enabled;
			}

			// HACK
			DialogueAction da = Player.GetComponent<DialogueAction>();
			if(da != null)
			{
				da.enabled = enabled;
			}

			ElevatorAction ea = Player.GetComponent<ElevatorAction>();
			if (ea != null)
			{
				ea.enabled = enabled;
			}
		}
	}

    public void Restart()
    {
		SceneManager.LoadScene("01_Tutorial");
		SetState(State.INGAME);

		ResetCurrentLevel();
    }
}
