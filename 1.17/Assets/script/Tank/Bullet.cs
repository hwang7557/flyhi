using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(BulletRemove());
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * 0.3f);        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BulletRemove()
    {
        yield return new WaitForSeconds(6.0f);

        DestroyImmediate(gameObject);
    }
}
