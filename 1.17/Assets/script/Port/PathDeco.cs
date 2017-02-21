using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDeco : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PathTilingScaleChange(float TileSIzeX, float TileSizeY)
    {
        Material ResourcesTemp = Resources.Load("Prefebs/Ground10", typeof(Material)) as Material;
        Material ChangeMaterial = Instantiate(ResourcesTemp) as Material;
        ChangeMaterial.mainTextureScale = new Vector2(TileSIzeX * 0.2f, TileSizeY * 0.2f);
        gameObject.GetComponent<Renderer>().material = ChangeMaterial;
    }
}
