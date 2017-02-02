using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour {

    Camera mainCamera;
    Vector3 defaultPosition;
    Quaternion defaultRotation;
    float defaultZoom;

    // Use this for initialization
    void Start () {
        //d = GetComponent<Fire>();
        mainCamera = GetComponent<Camera>();


        defaultPosition = mainCamera.transform.position;
        defaultRotation = mainCamera.transform.rotation;
        defaultZoom = mainCamera.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            transform.Translate(Input.GetAxisRaw("Mouse X") / 10.0f, Input.GetAxisRaw("Mouse Y") / 10.0f, 0.0f);
        }
        else if(Input.GetMouseButton(1))
        {
            transform.Rotate(Input.GetAxisRaw("Mouse Y") * 10.0f, Input.GetAxisRaw("Mouse X") * 10.0f, 0.0f);
        }
   
        {
            //줌 인 아웃
            mainCamera.fieldOfView -= (20.0f * Input.GetAxis("Mouse ScrollWheel"));
            if (mainCamera.fieldOfView < -10)
                mainCamera.fieldOfView = 10;
        }

        {
            if(Input.GetMouseButton(2))
            {
                mainCamera.transform.position = defaultPosition;
                mainCamera.transform.rotation = defaultRotation;
                mainCamera.fieldOfView = defaultZoom;
            }
        }

        //setActive
	}
}
