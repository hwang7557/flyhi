using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingCamera : MonoBehaviour {

    public bool AttackNow = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!AttackNow)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
	}
}
