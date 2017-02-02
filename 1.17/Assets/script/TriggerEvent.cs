using UnityEngine;

public class TriggerEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject hitObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;
    }
}
