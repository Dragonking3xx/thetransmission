using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAction : Action {

	// Use this for initialization
	void Start () {
		targetTag = "dialogue";
	}

	public override void DoAction()
	{
		Debug.Log("Dialogue action?");
		if (targetInRange != null)
		{
			Debug.Log("Dialogue action!");

			Dialogue dialogue = targetInRange.GetComponent<Dialogue>();
            if (dialogue.D != null)
            {
                dialogue.D.DialogueID = dialogue.nextDialogueName;
            }

			GameController.Instance.ShowDialog(dialogue.DialogueID);
		}
	}
}
