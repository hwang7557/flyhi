using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    delegate void BulletRemove();

    GameObject Prefeb = null;
    bool OneShot = false;
    static public int Life = 50;
    static public int kill = 0;
    // Use this for initialization
    void Start () {
        Prefeb = Resources.Load("Prefebs/Bullet") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (!OneShot && Input.GetKeyDown(KeyCode.Space))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                OneShot = true;
                StartCoroutine(FireRoution());
                Vector3 d = transform.parent.parent.parent.position;
                d.x += 2.0f;

                Quaternion tt = transform.parent.parent.parent.rotation * gameObject.transform.parent.parent.rotation;
                //Quaternion tt = transform.parent.rotation;

                GameObject bullet = Instantiate(Prefeb, d, tt);
            }
        }
        OneShot = false;
        if (Life <= 0)
        {
            string s = Application.loadedLevelName;
            GameManager.Instance.ChangeScene(s);
        }
    }

    private void OnGUI()
    {
        string s = string.Format("라이프: {0}", Life);

        GUI.TextField(new Rect(0, 0, 100, 30), s);

        s = string.Format("죽인 횟수 : {0}", kill);

        GUI.TextField(new Rect(150, 0, 100, 30), s);
    }

    


    IEnumerator FireRoution()
    {
        yield return null;

     
    }
}
