using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour {
    public GameObject Minion;
    bool Move = true;
    int MaxEnemy = 5;
    public static int EnemyCount = 0;
    bool delay = true;
    int hp = 20;

    // Use this for initialization
    void Start () {
        Minion = Resources.Load("Prefebs/Enemy") as GameObject;

        
    }
	
	// Update is called once per frame
	void Update () {

        if(delay)
        {
            delay = false;
            StartCoroutine(MinionMake());
        }
        

        if (Move)
        {
            transform.Translate(Vector3.forward * 0.1f);
        }
        else
        {
            transform.Translate(Vector3.back * 0.1f);
        }

        if (Move && transform.position.z >= 3.0f)
        {
            Move = false;
        }
        else if (!Move && transform.position.z <= -3.0f)
        {
            Move = true;
        }
    }

    public IEnumerator MinionMake()
    {
        yield return new WaitForSeconds(2.0f);

        if (MaxEnemy > EnemyCount)
        {
            Vector3 c = transform.position;
            c.y += 1.0f;
            GameObject EnemyGo = Instantiate(Minion, c, transform.rotation);
            EnemyCount += 1;
        }
        delay = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            hp -= 1;
            if(hp <= 0)
            {
                Destroy(gameObject);
                string s = Application.loadedLevelName;
                GameManager.Instance.ChangeScene(s);
            }
        }
    }
}
