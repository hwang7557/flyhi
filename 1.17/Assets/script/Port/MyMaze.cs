using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMaze : MonoBehaviour
{

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
   
    Dictionary<int, List<PathArea>> L_PathList = new Dictionary<int, List<PathArea>>();

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
        PathOverLapCheck();
        PathInstantiate();
    }

    // Update is called once per frame
    void Update()
    {
        int b = 0;
        if (b == 0)
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

        if (check)
        {
            MazeMakeCount++;
            MapMake();
        }
    }

    void RoomMake()
    {
        for (int i = 0; i < L_Area.Count; i++)
        {
            //게임에 적용될 룸 사이즈를 랜덤하게 출력
            int MakeRoomSizeX = Random.Range((int)(Mathf.Round(L_Area[i].LocalArea.x * 0.5f)), (int)(L_Area[i].LocalArea.x - 1.0f));
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
            if (L_Area[i].ParentNum != -1)
            {

                //콤바인 버전2
                if (L_Area[i].DivideNum == 0)
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
                if (L_Area[i].CombinePlayArea.x == L_Area[L_Area[i].ParentNum].PlayArea.x)
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
                        PathTemp.PathPlayArea = new Vector3(TempCombinePlayArea.x + (PathTemp.PathSize.x * AreaCheckX) - 2.0f, 0,
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
                    if (AreaCheckX == 0)
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



                List<PathArea> p = new List<PathArea>();
                p.Add(PathTemp);

                L_PathList.Add(L_PathList.Count, p);
                

                

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


    void PathOverLapCheck()
    {
        for (int i = L_Area.Count - 1; i > 0; i--)
        {
            Rect MyRoom = new Rect(L_Area[i].RoomPosition.x - L_Area[i].RoomSize.x * 0.5f,
                L_Area[i].RoomPosition.z - L_Area[i].RoomSize.z * 0.5f,
                L_Area[i].RoomSize.x,
                L_Area[i].RoomSize.z);

            Rect ParentRoom = new Rect(L_Area[L_Area[i].ParentNum].RoomPosition.x - L_Area[L_Area[i].ParentNum].RoomSize.x * 0.5f,
                L_Area[L_Area[i].ParentNum].RoomPosition.z - L_Area[L_Area[i].ParentNum].RoomSize.z * 0.5f,
                L_Area[L_Area[i].ParentNum].RoomSize.x,
                L_Area[L_Area[i].ParentNum].RoomSize.z);




            for (int j = 0; j < L_PathList.Count; j++)
            {
                for(int k = 0; k < L_PathList[j].Count; k++)
                {
                    Rect Path = new Rect(L_PathList[j][k].PathPlayArea.x - L_PathList[j][k].PathSize.x * 0.5f,
                   L_PathList[j][k].PathPlayArea.z - L_PathList[j][k].PathSize.z * 0.5f,
                   L_PathList[j][k].PathSize.x,
                   L_PathList[j][k].PathSize.z);



                    //MyRoom에 길이 어느정도 겹치는지 확인한다.
                    if (MyRoom.x < Path.xMax &&
                       MyRoom.xMax > Path.x &&
                       MyRoom.y > Path.y - Path.height &&
                       MyRoom.y - MyRoom.height < Path.y)
                    {
                        //이제 충돌을 했다면 어디로 충돌했는지 확인하며,
                        //해당 충돌이 길에서 겹치는 만큼 길을 제거해주며,
                        //현재 [i]에 입구를 나타내는 구역을 설정해줌.
                        float Left = MyRoom.x > Path.x ? MyRoom.x : Path.x;
                        float Right = MyRoom.xMax < Path.xMax ? MyRoom.xMax : Path.xMax;
                        float top = Mathf.Min(MyRoom.y, Path.y);
                        float Bottom = Mathf.Max(MyRoom.y - MyRoom.height, Path.y - Path.height);

                        int sdf = 123;
                    }
                    else
                    {
                        Rect MyPlayArea = new Rect(L_Area[i].PlayArea.x - L_Area[i].PlayArea.x * 0.5f,
                        L_Area[i].PlayArea.z - L_Area[i].PlayArea.z * 0.5f,
                        L_Area[i].LocalArea.x,
                        L_Area[i].LocalArea.z);

                        //다만 이것은 경우 충분히 근처에있다는 전제가 작동해야함.
                        if (MyPlayArea.x < Path.xMax &&
                       MyPlayArea.xMax > Path.x &&
                       MyPlayArea.y > Path.y - Path.height &&
                       MyPlayArea.y - MyPlayArea.height < Path.y)
                        {
                            RoomAndPathDirection(MyRoom, Path);
                        }
                    }

                    //MyRoom에 길이 어느정도 겹치는지 확인한다.
                    if (ParentRoom.x < Path.xMax &&
                       ParentRoom.xMax > Path.x &&
                       ParentRoom.y > Path.y - Path.height &&
                       ParentRoom.y - ParentRoom.height < Path.y)
                    {
                        //기준.Left > 대상.Left (기준 레프트가 대상 레프트보다 크다는건 -> + 방향기준으로 보면
                        //대상의 오른쪽에 있다는 말이다.
                        //           ---
                        //        --|-  |
                        //        |  |--
                        //        ---


                        float Left = ParentRoom.x > Path.x ? ParentRoom.x : Path.x;
                        float Right = ParentRoom.xMax < Path.xMax ? ParentRoom.xMax : Path.xMax;
                        float top = Mathf.Min(ParentRoom.y, Path.y);
                        float Bottom = Mathf.Max(ParentRoom.y - ParentRoom.height, Path.y - Path.height);
                        //1늘리고0.5 이동시킴
                        int d = 123;
                    }
                    else
                    {
                        Rect ParentPlayArea = new Rect(L_Area[L_Area[i].ParentNum].PlayArea.x - L_Area[L_Area[i].ParentNum].PlayArea.x * 0.5f,
                            L_Area[L_Area[i].ParentNum].PlayArea.z - L_Area[L_Area[i].ParentNum].PlayArea.z * 0.5f,
                            L_Area[L_Area[i].ParentNum].LocalArea.x,
                            L_Area[L_Area[i].ParentNum].LocalArea.z);


                        //다만 이것은 경우 충분히 근처에있다는 전제가 작동해야함.
                        if (ParentPlayArea.x < Path.xMax &&
                       ParentPlayArea.xMax > Path.x &&
                       ParentPlayArea.y > Path.y - Path.height &&
                       ParentPlayArea.y - ParentPlayArea.height < Path.y)
                        {
                            //1. 부모렉트와 내 렉트를 비교하여 현재 패스와 부모의 위치를 파악해본다.
                            //2. 기준을 잡을 때는 함수(부모렉트, 패스)를 기준으로 잡는다.
                            //3. 패스의 모양을 체크 후 부모와의 연결 가능성을 감안함.
                            //4. 패스가 길어지는 것만으로도 연결될 수 있다면 연결하고 새로운 딕셔너리 방식의 길을 추가해야함.

                            RoomAndPathDirection(ParentRoom, Path);

                            int sdf = 123;
                        }
                    }
                }

               


            }
        }
    }


    void PathInstantiate()
    {
        //맵 땅
        for (int i = 0; i < L_Area.Count; i++)
        {
            GameObject PathScaleChange = PathPrefeb;

            PathScaleChange.transform.localScale = new Vector3(L_Area[i].RoomSize.x * 0.2f, L_Area[i].RoomSize.z * 0.2f, 1);
            Instantiate(PathScaleChange, L_Area[i].RoomPosition, PathScaleChange.transform.rotation);
        }

        //맵 길
        for (int i = 0; i < L_PathList.Count; i++)
        {
            for(int j = 0; j < L_PathList[j].Count; j++)
            {
                GameObject PathScaleChange = PathPrefeb;

                PathScaleChange.transform.localScale = new Vector3(L_PathList[i][j].PathSize.x * 0.2f, L_PathList[i][j].PathSize.z * 0.2f, 1);
                Instantiate(PathScaleChange, L_PathList[i][j].PathPlayArea, PathScaleChange.transform.rotation);
            }
        }
    }

    int SmallAndBigCheckUD(float a, float b)
    {
        if (a > b)
        {
            //오른쪽, 위
            return 1;
        }
        else if (a < b)
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


    Vector4 RoomAndPathDirection(Rect Room, Rect Path)
    {
        float DistanceLeft = Room.x - Path.x;
        float DistanceRight = Room.xMax - Path.xMax;
        float DistanceTop = -(Room.y - Path.y);
        float DistanceBottom = -((Room.y - Room.height) - (Path.y - Path.height));

        Vector4 TempVector = new Vector4(DistanceLeft, DistanceRight, DistanceTop, DistanceBottom);

        return TempVector;
    }
}
