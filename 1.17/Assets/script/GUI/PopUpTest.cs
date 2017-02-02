using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTest : MonoBehaviour {

    private Text itsMe;
    private Text[] itsMe2;
    public InputField desc;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        //itsMe = Text.FindObjectOfType<>
        itsMe = GetComponentInChildren<UnityEngine.UI.Text>();
        //itsMe2 = GetComponentsInChildren<UnityEngine.UI.Text>();

        //for(int i =0; i < itsMe2.Length; i++)
        //{
        //    string s = itsMe2[i].text;
        //    int c = i;
        //}

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnClickCancel()
    {
        Debug.Log("OnClickCancel");
    }

    void OnClickOk()
    {
        Debug.Log("OnClickOK");
    }

    void OnClickExit()
    {
        gameObject.SetActive(false);
    }

    void OnPopUpTextChange()
    {
        itsMe.color = new Color(1.0f, 1.0f, 1.0f);
        itsMe.text = desc.text;
    }
}
