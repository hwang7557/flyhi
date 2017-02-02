using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossControl : MonoBehaviour {

    public GameObject Boss;
  
    // Use this for initialization
    void Start () {
        GameObject BossMake = Resources.Load("Prefebs/ImBoss") as GameObject;
        

        Boss = Instantiate(BossMake, transform.localPosition, transform.localRotation);
      
	}
	
	// Update is called once per frame
	void Update () {

      
	}

   
}
