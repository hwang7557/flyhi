using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour {

    public float MoveSpeed = 10.0f;

	// Use this for initialization
	void Start () {
        MoveSpeed = 20.0f;
	}
	
    //스태틱이나 싱글톤된거 실행할 때 씀 이를바 이미지 로딩 등?
    void Awake()
    {
    }

	// Update is called once per frame
	void Update () {
        float moveDelta = this.MoveSpeed * Time.deltaTime;

        transform.Rotate(0.0f, moveDelta, 0.0f);
	}
}
