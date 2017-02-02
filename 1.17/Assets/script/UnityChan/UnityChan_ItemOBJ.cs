using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_ItemOBJ : MonoBehaviour {

  

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ControlPlayer")
        {
            UnityChan_ItemMake.kill += 1;
            gameObject.SetActive(false);
            
        }
    }

    

}
