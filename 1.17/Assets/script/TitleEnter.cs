using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEnter : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //string s = Application.loadedLevelName;
            //GameManager.Instance.ChangeScene(s);
        }
    }
}
