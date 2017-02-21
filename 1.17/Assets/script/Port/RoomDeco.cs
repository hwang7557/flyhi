using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDeco : MonoBehaviour {
    
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void floorTilingScaleChange(float TileSIzeX, float TileSizeY)
    {
        Material ResourcesTemp = Resources.Load("Prefebs/Ground04", typeof(Material)) as Material;
        Material ChangeMaterial = Instantiate(ResourcesTemp) as Material;
        ChangeMaterial.mainTextureScale = new Vector2(TileSIzeX * 0.2f, TileSizeY * 0.2f);
        gameObject.GetComponent<Renderer>().material = ChangeMaterial;
    }
}
