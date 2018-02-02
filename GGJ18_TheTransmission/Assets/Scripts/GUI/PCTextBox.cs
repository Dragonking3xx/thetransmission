using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCTextBox : MonoBehaviour {

    private static PCTextBox instance;
    public static PCTextBox Instance
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
    public GameObject PCText;
	public GameObject PCUI;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextPage()
    {

    }

    public void PrevPage()
    {

    }
    public void ExitPC()
    {
        GameController.Instance.DialogEnd();
		PCUI.SetActive(false);

	}

    public void SetText(string text)
    {
		PCText.GetComponent<Text>().text = text;
    }
}
