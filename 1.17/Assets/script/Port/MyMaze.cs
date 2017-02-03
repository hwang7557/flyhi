using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMaze : MonoBehaviour {

    int MazeMakeCount = 0;

    class MazeArea
    {
        public Vector3 LocalArea;
        public Vector3 PlayArea;

        public Vector3 RoomSize;
        public Vector3 RoomPosition;

        public Vector3 CombineArea;

        public int ParentNum;
    }

    class PathArea
    {
        public Vector3 LocalArea;
        public Vector3 PlayArea;

        public int ParentNum;
    }

    List<MazeArea> L_Area = new List<MazeArea>();
    List<PathArea> L_Path = new List<PathArea>();

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
            if (L_Area[i].LocalArea.x < 30.0f || L_Area[i].LocalArea.z < 30.0f || MazeMakeCount == 3)
            {
                check = false;
            }
        }

        if(check)
        {
            MazeMakeCount++;
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
        for (int i = L_Area.Count - 1; i > 0; i--)
        {
            if(L_Area[i].ParentNum != -1)
            {
                //합칠 때 새로운 합쳐진 사이즈를 저장해야하기 때문에 CombineArea에 저장할 예정
                L_Area[L_Area[i].ParentNum].CombineArea = L_Area[i].LocalArea + L_Area[L_Area[i].ParentNum].LocalArea;

                //합쳐질 에어리어의 위치를 확인해야 함. 오른쪽인지 왼쪽인지 위쪽인지 아래인지
                int AreaCheckX = SmallAndBigCheck(L_Area[i].PlayArea.x, L_Area[L_Area[i].ParentNum].PlayArea.x);
                int AreaCheckY = SmallAndBigCheck(L_Area[i].PlayArea.y, L_Area[L_Area[i].ParentNum].PlayArea.y);

                //위치를 확인했다면 이제 나(i)를 중심으로 센터를 잡자
                float ChildrenCenterX = Mathf.Round(L_Area[i].PlayArea.x * 0.5f);
                float ChildrenCenterZ = Mathf.Round(L_Area[i].PlayArea.z * 0.5f);

                //위치를 확인했다면 이제 나(i)를 중심으로 센터를 잡자
                float ParentCenterX = Mathf.Round(L_Area[L_Area[i].ParentNum].PlayArea.x * 0.5f);
                float ParentCenterZ = Mathf.Round(L_Area[L_Area[i].ParentNum].PlayArea.z * 0.5f);

                //패스 연결
                Vector3 PathMakeSize = new Vector3();

                ////내가 방금 잡은 센터가 룸 에어리어에 포함되어 있는지 확인?? 지금 생각해보니 불필요할 지도
                //Rect CenterInPlayArea = new Rect(L_Area[i].RoomPosition.x, L_Area[i].RoomPosition.z, L_Area[i].RoomSize.x, L_Area[i].RoomSize.z);
                //bool InCheck = CenterInPlayArea.Contains(new Vector2(ChildrenCenterX, ChildrenCenterZ));








            }
        }
    }
    int SmallAndBigCheck(float a, float b)
    {
        if(a > b)
        {
            //오른쪽
            return 1;
        }
        else if(a < b)
        {
            //왼쪽
            return -1;
        }
        else
            return 0;   //같다
    }
}
