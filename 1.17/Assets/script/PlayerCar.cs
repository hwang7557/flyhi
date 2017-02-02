using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour {
    float CarMoveSpeed = 3.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float v = Input.GetAxis("Horizontal");

        

        if(v < 0 && CarMoveSpeed < 5.0f)
            CarMoveSpeed -= v;

        if (v > 0 && CarMoveSpeed >= 0)
            CarMoveSpeed -= v;

        float ResultMoveSpeed = CarMoveSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * ResultMoveSpeed);
    }
}
