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
    private Text PCText;

    // Use this for initialization
    void Start () {
        PCText = transform.Find("PCText").gameObject.GetComponent<Text>();
        GameObject.Find("PrevPage").SetActive(false);
        GameObject.Find("NextPage").SetActive(false);
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
    }

    public void SetText(string text)
    {
        PCText.text = text;
    }
}
