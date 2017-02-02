using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        //물리계산은 여기서 하는 거

        //interplate 이전 프레임의 위치
        //Collision Destection 계산을 더 정확하게 할지 말지 고른다.
        //프리즈 포지션 쓸데없이 돌지않도록 포지션을 고정 시킴
        //angleDrag 굴러가는 회전속도 조절
    }
}
