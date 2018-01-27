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
            if (instance == null)
            {
                instance = new NormalTextBox();
            }

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

    public TextAsset TextA;
    private Xml2CSharp.Dialogs dialogs;

    public String GuyName = "";
    private String TextId = "0";
    private Xml2CSharp.Options OptionList;

    private GameObject go;




    // Use this for initialization
    void Start()
    {

		go = gameObject;
        XmlSerializer serializer = new XmlSerializer(typeof(Xml2CSharp.Dialogs));
        using (StringReader reader = new StringReader(TextA.text))
        {
            dialogs = serializer.Deserialize(reader) as Xml2CSharp.Dialogs;
        }


		if (Select01Text == null ||
			Select02Text == null ||
			Select03Text == null)
			Debug.Log("NormalTextBox: Load Buttons Text Error!!!");

		// TEST
		loadText("room3-1");
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void loadText(String GuyName, String TextId="0")
    {
        List<Xml2CSharp.Dialog> dList = dialogs.Dialog;
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
                Select02Text.GetComponent<Text>().text = OptionList.Option[1].Text;
                Select03Text.GetComponent<Text>().text = OptionList.Option[2].Text;

            }

        }

    }
    



    public void Text01()
    {
        if(OptionList.Option[0].Action != null)
        {
            Action(OptionList.Option[0].Action);
            return;
        }
        loadText(GuyName, OptionList.Option[0].TogetPage);

    }
    public void Text02()
    {
        if (OptionList.Option[1].Action != null)
        {
            Action(OptionList.Option[1].Action);
            return;
        }
        loadText(GuyName, OptionList.Option[1].TogetPage);
    }
    public void Text03()
    {
        if (OptionList.Option[2].Action != null)
        {
            Action(OptionList.Option[2].Action);
            return;
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
        GameObject deathGo = GameObject.Find("DeathText");
        deathGo.GetComponent<Text>().text = deathText;
        GameObject.Find("GUI").SetActive(true);
    }


}
