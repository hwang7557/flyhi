using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour {

    float Angle = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float v = Input.GetAxis("Vertical");
        Angle += v;

        Angle = Mathf.Clamp(Angle, 0, 20);

        if (Angle > 0 && Angle < 20)
        {
            Vector3 MyWay = transform.position;
            MyWay.z += 30.0f;

            transform.RotateAround(transform.position, MyWay, v);
        }

    }
}
