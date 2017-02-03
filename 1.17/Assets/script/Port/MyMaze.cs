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
        PathMake();
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
            //게임에 적용될 룸 사이즈를 랜덤하게 출력
            int MakeRoomSizeX = Random.Range((int)(Mathf.Round(L_Area[i].LocalArea.x * 0.5f)),(int)(L_Area[i].LocalArea.x - 1.0f));
            int MakeRoomSizeZ = Random.Range((int)(Mathf.Round(L_Area[i].LocalArea.z * 0.5f)), (int)(L_Area[i].LocalArea.z - 1.0f));


            //룸 사이즈 출력 후 해당 사이즈를 전체 사이즈에서 빼고
            float ResulteMakeRoomSizeX = L_Area[i].LocalArea.x - MakeRoomSizeX;
            float ResulteMakeRoomSizeZ = L_Area[i].LocalArea.z - MakeRoomSizeZ;

            //ResulteMakeRoomSize를 랜덤최대값으로 선택하여 다시 랜덤 돌리기
            int EmptySizeX = Random.Range(0, (int)ResulteMakeRoomSizeX);
            int EmptySizeZ = Random.Range(0, (int)ResulteMakeRoomSizeZ);

            //Empty사이즈가 정해졌으면, 이제 나온 사이즈만큼 비워놓고 실제 위치에 더해준다.
            L_Area[i].RoomPosition = L_Area[i].PlayArea + new Vector3(EmptySizeX, 0, -EmptySizeZ);

            //이제 룸 사이즈를 내 리스트안에 넣어둔다.
            L_Area[i].RoomSize = new Vector3(MakeRoomSizeX, 0, MakeRoomSizeZ);
        }
    }

    void PathMake()
    {
        //패스를 만든다.
        //우선 마지막 패스부터 차근차근 연결한다.

        for (int i = L_Area.Count; i <= 0; i++)
        {
            if(L_Area[i].ParentNum != -1)
            {
                //합칠 때 새로운 합쳐진 사이즈를 저장해야하기 때문에 함수 2개쯤 더 추가해야함.
                //if(L_Area[i].RoomPosition 
                L_Area[i].RoomPosition.x = L_Area[i].RoomPosition.x + 1.0f;
            }
        }
    }
}
