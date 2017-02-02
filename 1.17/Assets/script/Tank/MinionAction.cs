using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAction : MonoBehaviour {
    float time = 0.0f;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        time += 0.1f;
        transform.Translate(Vector3.left * 0.1f);
        if (time > 20.0f)
        {
            DestroyImmediate(gameObject);
            BossAction.EnemyCount -= 1;
            Fire.Life -= 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Destroy(gameObject);
            BossAction.EnemyCount -= 1;
            Fire.kill += 1;
        }
    }
}
