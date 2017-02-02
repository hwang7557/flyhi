using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    { 
       

    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(150, 250, 100, 50), "타이틀로"))
        {
            Fire.Life = 5;
            Fire.kill = 0;
            string s = Application.loadedLevelName;
            GameManager.Instance.ChangeScene(s);
        }
    }
}
