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

    public void WallMake(float sizeX, float sizeY)
    {
        GameObject g = Resources.Load("Prefebs/Wall01_a") as GameObject;

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

    public void WallDelete(float x, float y, float Width, float Height)
    {
        Transform[] GG = gameObject.GetComponentsInChildren<Transform>();

        for (int i = 1; i < GG.Length; i++)
        {
            if (x <= GG[i].position.x &&
               x + Width >= GG[i].position.x &&
               y >= GG[i].position.z &&
               y - Height <= GG[i].position.z)
            {
                Destroy(GG[i].gameObject);
            }
        }
    }
}
