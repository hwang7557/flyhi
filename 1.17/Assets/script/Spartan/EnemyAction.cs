using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour {

    enum Action
    {
        D_IDLE,
        D_DEATH,
        D_HARDDEATH,
    }


    bool Diego = false;

    Animation spartanKing;
    CharacterController pcControl;

    // Use this for initialization
    void Start () {
        spartanKing = gameObject.GetComponentInChildren<Animation>();
        pcControl = gameObject.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Diego)
        {
            DeleteObj();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            StartCoroutine(DeathMotion());
        }
    }

    IEnumerator DeathMotion()
    {
        spartanKing.wrapMode = WrapMode.Once;
        spartanKing.CrossFade("die", 0.3f);

        Diego = true;

        yield return null;
    }

    public void DeleteObj()
    {
        if (!spartanKing.IsPlaying("die"))
        {
            Destroy(gameObject);
        }
    }
}
