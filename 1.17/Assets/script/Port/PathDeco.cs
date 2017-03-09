using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDeco : MonoBehaviour
{
    public bool PathDecoComplete = false;

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

    public void WallMakeX(float sizeX, float sizeY)
    {
        if(sizeX != 0)
        {
            GameObject g = Resources.Load("Prefebs/Wall01_c") as GameObject;

            for (int i = 1; i <= sizeX; i++)
            {
                GameObject d = Instantiate(g, gameObject.transform.localPosition, gameObject.transform.localRotation);
                d.transform.localScale = new Vector3(0.2f, 0.4f, 0.3f);
                d.transform.parent = gameObject.transform;
                d.transform.localPosition = new Vector3(((sizeX * 0.5f / gameObject.transform.localScale.x) / (sizeX * 0.5f) * i)
                    - (sizeX * 0.5f / gameObject.transform.localScale.x), -(sizeY * 0.5f / gameObject.transform.localScale.y), 0);

            }

            for (int i = 1; i <= sizeX; i++)
            {
                GameObject d = Instantiate(g, gameObject.transform.localPosition, gameObject.transform.localRotation);
                d.transform.localScale = new Vector3(0.2f, 0.4f, 0.3f);
                d.transform.parent = gameObject.transform;
                d.transform.localPosition = new Vector3(((sizeX * 0.5f / gameObject.transform.localScale.x) / (sizeX * 0.5f) * i)
                    - (sizeX * 0.5f / gameObject.transform.localScale.x), (sizeY * 0.5f / gameObject.transform.localScale.y), 0);

            }

            for (int i = 1; i <= sizeY; i++)
            {
                GameObject d = Instantiate(g, gameObject.transform.localPosition, gameObject.transform.localRotation);
                d.transform.localScale = new Vector3(0.2f, 0.4f, 0.3f);
                d.transform.Rotate(0, 0, 90.0f);
                d.transform.parent = gameObject.transform;
                d.transform.localPosition = new Vector3(-(sizeX * 0.5f / gameObject.transform.localScale.x), ((sizeY * 0.5f / gameObject.transform.localScale.y) / (sizeY * 0.5f) * i)
                  - (sizeY * 0.5f / gameObject.transform.localScale.y), 0);
            }

            for (int i = 1; i <= sizeY; i++)
            {
                GameObject d = Instantiate(g, gameObject.transform.localPosition, gameObject.transform.localRotation);
                d.transform.localScale = new Vector3(0.2f, 0.4f, 0.3f);
                d.transform.Rotate(0, 0, 90.0f);
                d.transform.parent = gameObject.transform;
                d.transform.localPosition = new Vector3((sizeX * 0.5f / gameObject.transform.localScale.x), ((sizeY * 0.5f / gameObject.transform.localScale.y) / (sizeY * 0.5f) * i)
                  - (sizeY * 0.5f / gameObject.transform.localScale.y), 0);
            }
        }
    }

    public void WallMakeY(float sizeX, float sizeY)
    {
        if (sizeY != 0)
        {
            GameObject g = Resources.Load("Prefebs/Wall01_c") as GameObject;

            for (int i = 1; i <= sizeX; i++)
            {
                GameObject d = Instantiate(g, gameObject.transform.localPosition, gameObject.transform.localRotation);
                d.transform.localScale = new Vector3(0.2f, 0.4f, 0.3f);
                d.transform.parent = gameObject.transform;
                d.transform.localPosition = new Vector3(((sizeX * 0.5f / gameObject.transform.localScale.x) / (sizeX * 0.5f) * i)
                    - (sizeX * 0.5f / gameObject.transform.localScale.x), -(sizeY * 0.5f / gameObject.transform.localScale.y), 0);

            }

            for (int i = 1; i <= sizeX; i++)
            {
                GameObject d = Instantiate(g, gameObject.transform.localPosition, gameObject.transform.localRotation);
                d.transform.localScale = new Vector3(0.2f, 0.4f, 0.3f);
                d.transform.parent = gameObject.transform;
                d.transform.localPosition = new Vector3(((sizeX * 0.5f / gameObject.transform.localScale.x) / (sizeX * 0.5f) * i)
                    - (sizeX * 0.5f / gameObject.transform.localScale.x), (sizeY * 0.5f / gameObject.transform.localScale.y), 0);

            }

            for (int i = 1; i <= sizeY; i++)
            {
                GameObject d = Instantiate(g, gameObject.transform.localPosition, gameObject.transform.localRotation);
                d.transform.localScale = new Vector3(0.2f, 0.4f, 0.3f);
                d.transform.Rotate(0, 0, 90.0f);
                d.transform.parent = gameObject.transform;
                d.transform.localPosition = new Vector3(-(sizeX * 0.5f / gameObject.transform.localScale.x), ((sizeY * 0.5f / gameObject.transform.localScale.y) / (sizeY * 0.5f) * i)
                  - (sizeY * 0.5f / gameObject.transform.localScale.y), 0);
            }

            for (int i = 1; i <= sizeY; i++)
            {
                GameObject d = Instantiate(g, gameObject.transform.localPosition, gameObject.transform.localRotation);
                d.transform.localScale = new Vector3(0.2f, 0.4f, 0.3f);
                d.transform.Rotate(0, 0, 90.0f);
                d.transform.parent = gameObject.transform;
                d.transform.localPosition = new Vector3((sizeX * 0.5f / gameObject.transform.localScale.x), ((sizeY * 0.5f / gameObject.transform.localScale.y) / (sizeY * 0.5f) * i)
                  - (sizeY * 0.5f / gameObject.transform.localScale.y), 0);
            }
        }
    }

    public void WallDelete(float x, float y, float Width, float Height)
    {
        Transform[] GG = gameObject.GetComponentsInChildren<Transform>();

        for(int i =1; i < GG.Length; i++)
        {
            if (x + 0.4f <= GG[i].position.x &&
               x + Width - 0.4f >= GG[i].position.x &&
               y - 0.5f >= GG[i].position.z &&
               y - Height + 0.5f <= GG[i].position.z)
            {
                Destroy(GG[i].gameObject);
            }
        }
    }
}
