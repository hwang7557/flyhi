using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpStart : MonoBehaviour {

    public GameObject PopUp = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnPopupStart()
    {
        PopUp.SetActive(true);
    }
}
