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

    public void WallMake(float sizeX, float sizeY)
    {
        GameObject g = Resources.Load("Prefebs/Wall01_c") as GameObject;
        GameObject d = Instantiate(g, gameObject.transform.localPosition, gameObject.transform.localRotation);
        d.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        d.transform.parent = gameObject.transform;
        //d.transform.localScale = new Vector3(0.001616f, 0.16f, 1);


        //d.transform.localPosition = new Vector3(0, 0, 0);

        //if(gameObject.transform.localScale.x != 0 && gameObject.transform.localScale.y != 0)
        //{
        //    d.transform.localScale = new Vector3(1.0f / (gameObject.transform.localScale.x * sizeX), 1.0f / (gameObject.transform.localScale.y * sizeY), 1);
        //}
        //else
        //{
        //    if(gameObject.transform.localScale.x == 0)
        //    {
        //        d.transform.localScale = new Vector3(0.0f, 1.0f / (gameObject.transform.localScale.y * sizeY), 1);
        //    }
        //    else
        //    {
        //        d.transform.localScale = new Vector3(1.0f / (gameObject.transform.localScale.x * sizeX), 0.0f, 1);
        //    }
        //}


        if (gameObject.transform.localScale.x > gameObject.transform.localScale.y)
        {
            d.transform.Rotate(0, 0, 0);
        }
        else
        {
            d.transform.Rotate(0, 0, 90.0f);
        }

        int dfs = 3;

    }
}
