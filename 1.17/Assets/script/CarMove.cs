using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour {

    float CarMoveSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float ResultMoveSpeed = CarMoveSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward* ResultMoveSpeed);


	}
}
