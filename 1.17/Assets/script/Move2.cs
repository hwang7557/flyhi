using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TagetInfo
{
    public GameObject target;
    public float moveSpeed;
}

public class Move2 : MonoBehaviour {

    public TagetInfo[] targetInfos;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveVec = Vector3.zero;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x -= 1.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x += 1.0f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVec.z += 1.0f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVec.z -= 1.0f;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            //Instantiate()
        }

        moveVec = moveVec.normalized;

        for(int i =0; i < targetInfos.Length; i++)
        {
            Vector3 move = moveVec * targetInfos[i].moveSpeed * Time.deltaTime;

            targetInfos[i].target.transform.Translate(move);
        }
    }
}
