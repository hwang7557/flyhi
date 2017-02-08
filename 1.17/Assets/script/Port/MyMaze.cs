using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMaze : MonoBehaviour {

    int MazeMakeCount = 0;

    class MazeArea
    {
        public Vector3 LocalArea; // 300, 0, 300
        public Vector3 PlayArea; //-150, 0, 150

        public Vector3 RoomSize;
        public Vector3 RoomPosition;

        public Vector3 CombineLocalArea;
        public Vector3 CombinePlayArea;

        public int ParentNum;
        public int DivideNum;
    }

    class PathArea
    {
        public Vector3 PathSize;
        public Vector3 PathPlayArea; //패스의 실제 위치
    }

    List<MazeArea> L_Area = new List<MazeArea>();
    List<PathArea> L_Path = new List<PathArea>();

    //Dictionary<int, List<PathArea>> L_PathList = new Dictionary<int, List<PathArea>>();

    Vector3 m_MapSize = new Vector3(300.0f, 0.0f, 300.0f);

    GameObject PathPrefeb = null;

    // Use this for initialization
    void Start()
    {
        PathPrefeb = Resources.Load("Prefebs/TestPath") as GameObject;

        MazeArea d = new MazeArea();
        d.LocalArea = m_MapSize;
        d.PlayArea = new Vector3(-150, 0, 150);
        d.ParentNum = -1;
        d.CombineLocalArea = m_MapSize;
        d.CombinePlayArea = new Vector3(-150, 0, 150);



        L_Area.Add(d);

        MapMake();
        RoomMake();
        PathMake();
        PathInstantiate();
    }

    // Update is called once per frame
    void Update()
    {
        int b = 0;
        if(b == 0)
        {
            int dd = 0;
        }
    }


    //---------------------------콤바인 사이즈 and 위치셋팅이 필요함.
    void MapMake()
    {
        int Count = L_Area.Count;

        for (int i = 0; i < Count; i++)
        {
            int MazeSize = Random.Range(3, 7);
            //int MazeSize = 5;

            Vector3 LocalAreaTemp = L_Area[i].LocalArea;
            Vector3 PlayAreaTemp = L_Area[i].PlayArea;
            Vector3 CombinePlayAreaTemp = L_Area[i].CombinePlayArea;
            int TempDividNumber = -1;

            if (L_Area[i].LocalArea.z >= L_Area[i].LocalArea.x)
            {
                //Z
                L_Area[i].LocalArea.z *= (0.1f * MazeSize);
                L_Area[i].LocalArea.z = Mathf.Round(L_Area[i].LocalArea.z);
                L_Area[i].CombineLocalArea.z *= (0.1f * MazeSize);
                L_Area[i].CombineLocalArea.z = Mathf.Round(L_Area[i].CombineLocalArea.z);
                TempDividNumber = 0;

                PlayAreaTemp.z -= L_Area[i].LocalArea.z;
                LocalAreaTemp.z -= L_Area[i].LocalArea.z;

                CombinePlayAreaTemp.z -= L_Area[i].LocalArea.z;


            }
            else
            {
                //X
                L_Area[i].LocalArea.x *= (0.1f * MazeSize);
                L_Area[i].LocalArea.x = Mathf.Round(L_Area[i].LocalArea.x);
                L_Area[i].CombineLocalArea.x *= (0.1f * MazeSize);
                L_Area[i].CombineLocalArea.x = Mathf.Round(L_Area[i].CombineLocalArea.x);
                TempDividNumber = 1;

                PlayAreaTemp.x += L_Area[i].LocalArea.x;
                LocalAreaTemp.x -= L_Area[i].LocalArea.x;

                CombinePlayAreaTemp.z -= L_Area[i].LocalArea.x;
            }

            MazeArea AddList = new MazeArea();
            AddList.LocalArea = LocalAreaTemp;
            AddList.PlayArea = PlayAreaTemp;
            AddList.ParentNum = i;
            AddList.DivideNum = TempDividNumber;

            AddList.CombineLocalArea = LocalAreaTemp;
            AddList.CombinePlayArea = PlayAreaTemp;


            L_Area.Add(AddList);
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

            //이제 룸 사이즈를 내 리스트안에 넣어둔다.
            L_Area[i].RoomSize = new Vector3(MakeRoomSizeX, 0, MakeRoomSizeZ);

            //Empty사이즈가 정해졌으면, 이제 나온 사이즈만큼 비워놓고 실제 위치에 더해준다.
            //----------------------여기 에러의 기운이 느껴집눼다.
            L_Area[i].RoomPosition = L_Area[i].PlayArea + new Vector3(EmptySizeX, 0, -EmptySizeZ);

            //이제 여기서 추가적으로 내 룸 사이즈의 1/2씩 +(오른쪽), -(아래)로 이동시켜서 룸 포지션을 다시 잡아줌
            L_Area[i].RoomPosition = L_Area[i].RoomPosition + new Vector3(MakeRoomSizeX * 0.5f, 0, -MakeRoomSizeZ * 0.5f);



        }
    }

    //방향값 하나 넣어주자. 현재 어디로 향하는지
    void PathMake()
    {
        //패스를 만든다.
        //우선 마지막 패스부터 차근차근 연결한다.
        for (int i = L_Area.Count - 1; i > 0; i--)
        {
            if(L_Area[i].ParentNum != -1)
            {

                //콤바인 버전2
                if(L_Area[i].DivideNum == 0)
                {
                    L_Area[L_Area[i].ParentNum].CombineLocalArea = L_Area[i].CombineLocalArea + new Vector3(0, 0, L_Area[L_Area[i].ParentNum].CombineLocalArea.z);
                    L_Area[L_Area[i].ParentNum].CombinePlayArea = L_Area[L_Area[i].ParentNum].PlayArea;
                }
                else if (L_Area[i].DivideNum == 1)
                {   
                    L_Area[L_Area[i].ParentNum].CombineLocalArea = L_Area[i].CombineLocalArea + new Vector3(L_Area[L_Area[i].ParentNum].CombineLocalArea.x, 0, 0);
                    L_Area[L_Area[i].ParentNum].CombinePlayArea = L_Area[L_Area[i].ParentNum].PlayArea;
                }



                //합쳐질 에어리어의 위치를 확인해야 함. 오른쪽인지 왼쪽인지 위쪽인지 아래인지
                int AreaCheckX = SmallAndBigCheckLR(L_Area[i].CombinePlayArea.x, L_Area[L_Area[i].ParentNum].PlayArea.x);
                int AreaCheckZ = SmallAndBigCheckLR(L_Area[i].CombinePlayArea.z, L_Area[L_Area[i].ParentNum].PlayArea.z);

                //위치를 확인했다면 이제 나(i)를 중심으로 센터를 잡자
                float ChildrenCenterX = Mathf.Round(L_Area[i].CombinePlayArea.x + L_Area[i].CombineLocalArea.x * 0.5f);
                float ChildrenCenterZ = Mathf.Round(L_Area[i].CombinePlayArea.z - L_Area[i].CombineLocalArea.z * 0.5f);

                //위치를 확인했다면 이제 부모를 중심으로 센터를 잡자
                float ParentCenterX = Mathf.Round(L_Area[L_Area[i].ParentNum].PlayArea.x + L_Area[L_Area[i].ParentNum].LocalArea.x * 0.5f);
                float ParentCenterZ = Mathf.Round(L_Area[L_Area[i].ParentNum].PlayArea.z - L_Area[L_Area[i].ParentNum].LocalArea.z * 0.5f);

                //패스 연결 사이즈 체크
                PathArea PathTemp = new PathArea();
                PathTemp.PathSize = new Vector3(Mathf.Abs(ChildrenCenterX - ParentCenterX), 
                    0.0f, Mathf.Abs(ChildrenCenterZ - ParentCenterZ));

                //패스 사이즈가 0인 경우 사이즈를 5.0f까지 증가 시킴
                if(L_Area[i].CombinePlayArea.x == L_Area[L_Area[i].ParentNum].PlayArea.x)
                {
                    PathTemp.PathSize.x = 5.0f;
                }
                else if (L_Area[i].CombinePlayArea.z == L_Area[L_Area[i].ParentNum].PlayArea.z)
                {
                    PathTemp.PathSize.z = 5.0f;
                }

                //패스의 실제 위치를 찾는다.
                //센터를 잡아주는 코드를 작성해야함.

                Vector3 TempCombinePlayArea = new Vector3(ChildrenCenterX, 0, ChildrenCenterZ);


                //치명적인 문제 발생 왼쪽인 경우에는 이동시켜줘야하지만, 오른쪽인 경우에는 이동시킬 필요가 없슴.

                //마찬가지로 위인 경우에는 위로 옮기고 아래인 경우에는 이동시킬 필요가 없슴.
                //이걸 수치로 표현하자면, -1(왼쪽,아래), 1(오른쪽,위)                
                if (AreaCheckX == -1 || AreaCheckZ == 1)
                {
                    if (AreaCheckX == 0)
                    {
                        PathTemp.PathPlayArea = new Vector3(TempCombinePlayArea.x+ (PathTemp.PathSize.x * AreaCheckX) - 2.0f, 0,
                   TempCombinePlayArea.z + (PathTemp.PathSize.z * AreaCheckZ));
                    }
                    else
                    {
                        PathTemp.PathPlayArea = new Vector3(TempCombinePlayArea.x + (PathTemp.PathSize.x * AreaCheckX), 0,
                   TempCombinePlayArea.z - (PathTemp.PathSize.z * AreaCheckZ) - 2.0f);
                    }
               
                }
                else
                {
                    if(AreaCheckX == 0)
                    {
                        PathTemp.PathPlayArea = new Vector3(TempCombinePlayArea.x, 0, TempCombinePlayArea.z - 2.0f);
                    }
                    else
                    {
                        PathTemp.PathPlayArea = new Vector3(TempCombinePlayArea.x - 2.0f, 0, TempCombinePlayArea.z);
                    }
                    
                }


                PathTemp.PathPlayArea = new Vector3(PathTemp.PathPlayArea.x + (PathTemp.PathSize.x * 0.5f), 0,
                    PathTemp.PathPlayArea.z - (PathTemp.PathSize.z * 0.5f));




                L_Path.Add(PathTemp);
                
                ////내가 방금 잡은 센터가 룸 에어리어에 포함되어 있는지 확인?? 지금 생각해보니 불필요할 지도
                //Rect CenterInPlayArea = new Rect(L_Area[i].RoomPosition.x, L_Area[i].RoomPosition.z, L_Area[i].RoomSize.x, L_Area[i].RoomSize.z);
                //bool InCheck = CenterInPlayArea.Contains(new Vector2(ChildrenCenterX, ChildrenCenterZ));
                   
            }
        }
    }

    //이번엔 연결된 길의 위치랑 방포지션이 겹치는지 확인
    //확인하는 경우 1. 방이 겹쳤을 때 불필요한 길 부분 제거
    //2. 방이 겹치지 않았을 때 연결
    //3. 


    void PathInstantiate()
    {
        //맵 땅
        for (int i =0; i < L_Area.Count; i++)
        {
            GameObject PathScaleChange = PathPrefeb;

            PathScaleChange.transform.localScale = new Vector3(L_Area[i].RoomSize.x * 0.2f, L_Area[i].RoomSize.z * 0.2f, 1);
            Instantiate(PathScaleChange, L_Area[i].RoomPosition, PathScaleChange.transform.rotation);
        }

        //맵 길
        for (int i = 0; i < L_Path.Count; i++)
        {
            GameObject PathScaleChange = PathPrefeb;

            PathScaleChange.transform.localScale = new Vector3(L_Path[i].PathSize.x * 0.2f, L_Path[i].PathSize.z * 0.2f, 1);
            Instantiate(PathScaleChange, L_Path[i].PathPlayArea, PathScaleChange.transform.rotation);
        }
    }

    int SmallAndBigCheckUD(float a, float b)
    {
        if(a > b)
        {
            //오른쪽, 위
            return 1;
        }
        else if(a < b)
        {
            //왼쪽, 아래
            return -1;
        }
        else
            return 0;   //같다
    }


    int SmallAndBigCheckLR(float a, float b)
    {
        if (a < b)
        {
            //오른쪽, 위
            return 1;
        }
        else if (a > b)
        {
            //왼쪽, 아래
            return -1;
        }
        else
            return 0;   //같다
    }
}
