using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUp : MonoBehaviour {
  
    public float Tank = 0.0f;
    public float Angle = 0.0f;

    // Use this for initialization
    void Start () {
     
    }

    // Update is called once per frame
    void Update () {

        float v = Input.GetAxis("Horizontal");
        Angle += v;
        
        Angle = Mathf.Clamp(Angle, -30, 30);

        if (Angle > -30 && Angle < 30)
        {
            transform.Rotate(0.0f, v, 0.0f);
        }
    }
}
