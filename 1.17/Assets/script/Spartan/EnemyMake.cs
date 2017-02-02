using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMake : MonoBehaviour {

    GameObject ResourceTemp;
    Vector3[] Position = { new Vector3(0, 0, 2), new Vector3(2, 0, 0), new Vector3(0, 0, -2), new Vector3(-2, 0, 0) };
    Quaternion[] Direction = { Quaternion.LookRotation(Vector3.back), Quaternion.LookRotation(Vector3.left), Quaternion.LookRotation(Vector3.forward), Quaternion.LookRotation(Vector3.right) };

    // Use this for initialization
    void Start () {
        ResourceTemp = Resources.Load("Prefebs/EnemySpartan") as GameObject;

        for(int i =0; i < 4; i++)
        {
            GameObject EnemyGo = Instantiate(ResourceTemp, Position[i], Direction[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
