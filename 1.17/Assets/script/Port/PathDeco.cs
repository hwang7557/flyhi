using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDeco : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PathTilingScaleChange(float TileSIzeX, float TileSizeY)
    {
        Material ResourcesTemp = Resources.Load("Prefebs/Ground10", typeof(Material)) as Material;
        Material ChangeMaterial = Instantiate(ResourcesTemp) as Material;
        ChangeMaterial.mainTextureScale = new Vector2(TileSIzeX, TileSizeY);
        gameObject.GetComponent<Renderer>().material = ChangeMaterial;
    }

    public void WallMake(float TileSIzeX, float TileSizeY)
    {
        GameObject WallPreFabs = Resources.Load("Prefebs/Wall01_c") as GameObject;

        for (float i = 0; i < gameObject.transform.localScale.x; i += 0.1f)
        {
            for (float j = 0; j < gameObject.transform.localScale.y; j += 0.1f)
            {
                if(i == 0 || j == 0 || i == gameObject.transform.localScale.x - 0.1f || j == gameObject.transform.localScale.z)
                {
                    GameObject WallMove = Instantiate(WallPreFabs, gameObject.transform);
                    WallMove.transform.position = new Vector3(-(gameObject.transform.localScale.x * 0.5f) + i, 0,
                        -(gameObject.transform.localScale.z * 0.5f) + j);
                    WallMove.transform.localScale = new Vector3(1.0f * TileSIzeX / gameObject.transform.localScale.x, 1.0f * TileSizeY / gameObject.transform.localScale.y, 1);
                    WallMove.transform.localRotation = Quaternion.identity;
                }
            }
        }
    }
}
