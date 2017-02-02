using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    float AutoMatic = 0.0f;

	// Use this for initialization
	void Start () {
	    	
	}

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        Vector3 moveVec = Vector3.zero;
      
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVec.x += 0.01f;
            AutoMatic += 0.01f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVec.x -= 0.01f;
            AutoMatic += 0.01f;
        }

        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            AutoMatic = 0.0f;
        }

        transform.Translate(moveVec);
    }
}
