using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;

public class NormalTextBox : MonoBehaviour {

    private static NormalTextBox instance;
    public static NormalTextBox Instance
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

    public GameObject TextBoxtext;
    public GameObject Select01Text;
    public GameObject Select02Text;
    public GameObject Select03Text;

    public GameObject Select01;
    public GameObject Select02;
    public GameObject Select03;

	public GameObject DeathGO;
	public GameObject DeathText;

	public TextAsset TextADialog;
    public TextAsset TextAFluff;
    public TextAsset TextADeath;

    private Xml2CSharp.Dialogs Dialogs;
    private FluffClass.Dialogs FluffDialogs;
    private FluffClass.Dialogs DeathDialogs;

    public String GuyName = "";
    private String TextId = "0";
    private Xml2CSharp.Options OptionList;

    private GameObject go;

	void Awake()
	{
		if (instance)
		{
			Debug.Log("Only one NormalTextBox allowed!");
			GameObject.DestroyImmediate(gameObject);
		}
		else
		{
			instance = this;
		}
	}

	// Use this for initialization
	void Start()
    {
        Select01 = GameObject.Find("Select01");
        Select02 = GameObject.Find("Select02");
        Select03 = GameObject.Find("Select03");


        go = gameObject;
        XmlSerializer serializer = new XmlSerializer(typeof(FluffClass.Dialogs));
        using (StringReader reader = new StringReader(TextAFluff.text))
        {
            FluffDialogs = serializer.Deserialize(reader) as FluffClass.Dialogs;
        }

        serializer = new XmlSerializer(typeof(Xml2CSharp.Dialogs));
        using (StringReader reader = new StringReader(TextADialog.text))
        {
            Dialogs = serializer.Deserialize(reader) as Xml2CSharp.Dialogs;
        }

        serializer = new XmlSerializer(typeof(FluffClass.Dialogs));
        using (StringReader reader = new StringReader(TextADeath.text))
        {
            DeathDialogs = serializer.Deserialize(reader) as FluffClass.Dialogs;
        }


        if (Select01Text == null ||
			Select02Text == null ||
			Select03Text == null)
			Debug.Log("NormalTextBox: Load Buttons Text Error!!!");

		// TEST
		loadText(GuyName);
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void loadText(String DialogGuy, String TextId="0")
    {
		GuyName = DialogGuy;

		if(!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		} 

        Select01.SetActive(true);
        Select02.SetActive(true);
        Select03.SetActive(true);
        List<Xml2CSharp.Dialog> dList = Dialogs.Dialog;
        foreach (Xml2CSharp.Dialog d in dList)
        {
            if (!d.Id.Equals(GuyName))
                continue;


            foreach (Xml2CSharp.Page p in d.Page)
            {
                if (!p.Id.Equals(TextId))
                    continue;

                OptionList = p.Options;

                TextBoxtext.GetComponent<Text>().text = p.Text;
                Select01Text.GetComponent<Text>().text = OptionList.Option[0].Text;

                if (OptionList.Option.Count > 1) {
                    Select02Text.GetComponent<Text>().text = OptionList.Option[1].Text;
                } else
                {
                    Select02.SetActive(false);
                }
                if (OptionList.Option.Count > 2)
                {
                    Select03Text.GetComponent<Text>().text = OptionList.Option[2].Text;
                } else {
                    Select03.SetActive(false);
                }



            }

        }

    }
    
    public void LoadFluffText(String TextId)
    {
        Select01.SetActive(false);
        Select02.SetActive(false);
        Select03.SetActive(false);

        List<FluffClass.Dialog> fluffDialogList = FluffDialogs.Dialog;
        foreach(FluffClass.Dialog Dialog in fluffDialogList)
        {
            if (Dialog.Id.Equals(TextId))
            {
                TextBoxtext.GetComponent<Text>().text = Dialog.Text;
                break;
            }
           
        }

    }
    public void LoadLogText(String TextId)
    {
        List<FluffClass.Dialog> fluffDialogList = FluffDialogs.Dialog;
        foreach (FluffClass.Dialog Dialog in fluffDialogList)
        {
            if (Dialog.Id.Equals(TextId))
            {
                GameObject.Find("PCTextbox").SetActive(true);
                PCTextBox.Instance.SetText(Dialog.Text);
                break;
            }

        }


    }



    public void Text01()
    {
        if(OptionList != null)
        {
            if(OptionList.Option[1].Action != null)
            {
                Action(OptionList.Option[0].Action);
                return;
            }
            
        }
        loadText(GuyName, OptionList.Option[0].TogetPage);

    }
    public void Text02()
    {
        if (OptionList != null)
        {
            if (OptionList.Option[1].Action != null)
            {
                Action(OptionList.Option[1].Action);
                return;
            }
        }
        loadText(GuyName, OptionList.Option[1].TogetPage);
    }
    public void Text03()
    {
        if (OptionList != null)
        {
            if (OptionList.Option[2].Action != null)
            {
                Action(OptionList.Option[2].Action);
                return;
            }
        }
        loadText(GuyName, OptionList.Option[2].TogetPage);
    }

    public void Action(String ActionString)
    {
        if (ActionString.Equals("end dialog"))
        {
            go.SetActive(false);
            GameController.Instance.DialogEnd();
            return;
        }
        if (ActionString.Equals("death-r5-1"))
        {
            go.SetActive(false);
            GameController.Instance.Death("Commander Jones draws a pistol and shoots you twice. Maybe you should have learned more about this world? Put it down to experience. Oh, wait.");
            return;
        }
        if (ActionString.Equals("tranferal-death"))
        {
            go.SetActive(false);
            GameController.Instance.Death("The air itself seems to eat away at you, and you struggle to get away - but there is nothing. You succumb to the poisonous atmosphere. Maybe a transfer wasn´t the best idea.");
            return;
        }
        if (ActionString.Equals("death-r13-1"))
        {
            go.SetActive(false);
            GameController.Instance.Death("Talking isn’t your strong point, is it? He shoots you twice, and as the life drains from your host body, you also disintegrate.");
            return;
        }


    }

    public void Death(String deathText)
    {
        foreach(FluffClass.Dialog d in DeathDialogs.Dialog)
        {
            if(d.Id.Equals(deathText))
            {
                DeathText.GetComponent<Text>().text = deathText;
                break;
            }
        }
    }


}
