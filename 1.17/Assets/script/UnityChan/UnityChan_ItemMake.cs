using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_ItemMake : MonoBehaviour {

    public static int Life = 5;

    public static int kill = 0;

    public class itemMake
    {
        public bool visible;
        public Vector3 where;
        public UnityChan_ItemOBJ obj;
    }

    Vector3[] Point = {new Vector3(-16.0f, 0, 17.0f), new Vector3(20.0f, 0, 17.0f), new Vector3(-15.0f, 0, 11.0f), new Vector3(-15.0f, 0, -6.0f)
    , new Vector3(8.0f, 2.0f, 8.0f)};

    


    public List<itemMake> item_MangeMent = new List<itemMake>();

	// Use this for initialization
	void Start () {
        GameObject BossMake = Resources.Load("Prefebs/Item") as GameObject;

        

        for (int i =0; i < 5; i++)
        {
            GameObject temp = Instantiate(BossMake, Point[i], Quaternion.identity);
            itemMake d = new itemMake();
            d.visible = false;
            d.where = Point[i];
            d.obj = temp.GetComponent<UnityChan_ItemOBJ>();
           
            item_MangeMent.Add(d);
        }


        startItemShow();



    }
	
	// Update is called once per frame
	void Update () {

        int Count = 0;
        
        for (int i = 0; i < item_MangeMent.Count; i++)
        {
            if (!(item_MangeMent[i].obj.gameObject.activeSelf))
            {
                Count++;
            }
            if(Count == 5)
            {
                item_MangeMent[Random.Range(0, 5)].obj.gameObject.SetActive(true);
            }
        }


    }

    void startItemShow()
    {
        for(int i =0; i < item_MangeMent.Count; i++)
        {
            item_MangeMent[i].obj.gameObject.SetActive(false);
        }
    }

    private void OnGUI()
    {
        string s = string.Format("라이프: {0}", Life);

        GUI.TextField(new Rect(0, 0, 100, 30), s);

        s = string.Format("점수 : {0}", kill);

        GUI.TextField(new Rect(150, 0, 100, 30), s);
    }
}
