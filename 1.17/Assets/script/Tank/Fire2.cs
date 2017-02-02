using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire2 : MonoBehaviour
{
    public class BulletStatus
    {
        public GameObject Bullet;
        public Transform trans;
        public float time;
    }
    GameObject Prefeb = null;
    bool OneShot = false;
    int fireCount = 0;


    List<BulletStatus> m_Lbullet = new List<BulletStatus>();

    // Use this for initialization
    void Start()
    {
        Prefeb = Resources.Load("Prefebs/Bullet") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!OneShot && Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OneShot = true;
                StartCoroutine(FireRoution());
            }
        }

        for (int i = 0; i < m_Lbullet.Count; i++)
        {
            m_Lbullet[i].time += 0.01f;
            m_Lbullet[i].Bullet.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
            if (m_Lbullet[i].time > 4.0f)
            {
                DestroyImmediate(m_Lbullet[i].Bullet);
                m_Lbullet.Remove(m_Lbullet[i]);
            }
        }
        OneShot = false;
    }

    IEnumerator FireRoution()
    {
        yield return null;

        BulletStatus bullet = new BulletStatus();
        bullet.Bullet = Instantiate(Prefeb, transform.parent.parent.position, transform.rotation) as GameObject;
        bullet.time = 0.0f;

        m_Lbullet.Add(bullet);
        fireCount++;
    }
}
