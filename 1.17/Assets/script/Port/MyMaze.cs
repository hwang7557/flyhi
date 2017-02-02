using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMaze : MonoBehaviour {

    class MazeArea
    {
        public Vector3 LocalArea;
        public Vector3 PlayArea;

        public Vector3 RoomSize;
        public Vector3 RoomPosition;

        public int ParentNum;
    }

    List<MazeArea> L_Area = new List<MazeArea>();
    Vector3 m_MapSize = new Vector3(300.0f, 0.0f, 300.0f);

    // Use this for initialization
    void Start()
    {
        MazeArea d = new MazeArea();
        d.LocalArea = m_MapSize;
        d.PlayArea = new Vector3(-150, 0, 150);
        d.ParentNum = -1;
        L_Area.Add(d);

        MapMake();
        RoomMake();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MapMake()
    {
        int Count = L_Area.Count;

        for (int i = 0; i < Count; i++)
        {
            int MazeSize = Random.Range(3, 7);

            Vector3 LocalAreaTemp = L_Area[i].LocalArea;
            Vector3 PlayAreaTemp = L_Area[i].PlayArea;

            if (L_Area[i].LocalArea.z >= L_Area[i].LocalArea.x)
            {
                //Z
                L_Area[i].LocalArea.z *= (0.1f * MazeSize);
                L_Area[i].LocalArea.z = Mathf.Round(L_Area[i].LocalArea.z);

                PlayAreaTemp.z -= L_Area[i].LocalArea.z;
                LocalAreaTemp.z -= L_Area[i].LocalArea.z;

                MazeArea AddList = new MazeArea();
                AddList.LocalArea = LocalAreaTemp;
                AddList.PlayArea = PlayAreaTemp;
                AddList.ParentNum = i;

                L_Area.Add(AddList);
            }
            else
            {
                //X
                L_Area[i].LocalArea.x *= (0.1f * MazeSize);
                L_Area[i].LocalArea.x = Mathf.Round(L_Area[i].LocalArea.x);

                PlayAreaTemp.x += L_Area[i].LocalArea.z;
                LocalAreaTemp.x -= L_Area[i].LocalArea.x;
                
                MazeArea AddList = new MazeArea();
                AddList.LocalArea = LocalAreaTemp;
                AddList.PlayArea = PlayAreaTemp;
                AddList.ParentNum = i;

                L_Area.Add(AddList);
            }
        }

        bool check = true;

        for (int i = 0; i < Count; i++)
        {
            if (L_Area[i].LocalArea.x < 30.0f || L_Area[i].LocalArea.z < 30.0f)
            {
                check = false;
            }
        }

        if(check)
        {
            MapMake();
        }
    }

    void RoomMake()
    {
        for(int i = 0; i < L_Area.Count; i++)
        {
            int RoomSizeXCheck = (int)(L_Area[i].LocalArea.x * 0.5f);
            int RoomSizeZCheck = (int)(L_Area[i].LocalArea.z * 0.5f);



        }
    }
}
