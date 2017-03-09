using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDeco : MonoBehaviour {

    public bool RoomDecoComplete = false;
    public bool ImPlayerRoom = false;
    public bool ImBossRoom = false;

    public Vector3 MapSizeReceive;

    GameObject[] Goblin;

    // Use this for initialization
    void Start () {
        StartCoroutine(MinionMake());
        
        //리소스 로드를 몇번이나 할까~! 그냥 복사해두자!!           
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResourceLoad(GameObject[] obj)
    {
        Goblin = obj;
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

    IEnumerator MinionMake()
    {
        yield return new WaitUntil(() => RoomDecoComplete);

        if(!ImPlayerRoom && !ImBossRoom)
        {
            Vector3 RoomSize = RoomSizeCheck();

            float MultipleRoom = RoomSize.x * RoomSize.z;
            float MultipleMap = MapSizeReceive.x * MapSizeReceive.z;

            MultipleMap = MultipleMap * 0.01f;

            float Value = MultipleRoom / MultipleMap;

            //현재 방에서 특정 위치를 선정해주는 식을 짤 필요가 있음.
            //5, 6, 7패턴을 짜서 랜덤한 위치 주기
            Vector3 TempPosi = gameObject.transform.position;

            TempPosi = new Vector3(TempPosi.x - RoomSize.x * 0.5f, 0, TempPosi.z + RoomSize.z * 0.5f);

            int RND_Value = Random.Range(5, 8);

            Vector3[] V3_RND_Posi = RegenPositon(TempPosi, RoomSize.x, RoomSize.z, RND_Value);

            //string sd = "";

            //for(int i =0; i < V3_RND_Posi.Length; i++)
            //{
            //    sd += V3_RND_Posi[i].ToString() + "\n";
            //}

            //Debug.Log(sd);
            
            if (Value <= 1.0f)
            {
                Debug.Log("나왓음");
                //미니언 한마리 << 기본적인 워리어만 출현
                equipment(Instantiate(Goblin[0], V3_RND_Posi[Random.Range(0, RND_Value)], Quaternion.identity));

            }
            else if(Value > 1.0f && Value <= 2.0f)
            {
                //미니언 한마리 << 워리어,아처 둘중에 하나 출현
                equipment(Instantiate(Goblin[Random.Range(0,2)], V3_RND_Posi[Random.Range(0, RND_Value)], Quaternion.identity));
            }
            else if (Value > 2.0f && Value <= 3.0f)
            {
                //미니언 두마리 << 워리어,아처 2마리 혹은 메이지 1마리 중에 출현
                int MageCheck = Random.Range(0, 3);
                int Spone = Random.Range(0, RND_Value);

                if (MageCheck == 2)
                {
                    equipment(Instantiate(Goblin[2], V3_RND_Posi[Spone], Quaternion.identity));
                }
                else
                {
                    Vector3[] temp = SponePositionCheck(V3_RND_Posi[Spone], 2);

                    equipment(Instantiate(Goblin[0], temp[0], Quaternion.identity));
                    equipment(Instantiate(Goblin[1], temp[1], Quaternion.identity));
                }
            }
            else if (Value > 3.0f && Value <= 4.0f)
            {
                int Spone = Random.Range(0, RND_Value);

                Vector3[] temp = SponePositionCheck(V3_RND_Posi[Spone], 2);

                //미니언 두마리 << 워리어,아처 1마리 메이지 1마리 중에 출현
                equipment(Instantiate(Goblin[Random.Range(0, 2)], temp[0], Quaternion.identity));
                equipment(Instantiate(Goblin[2], temp[1], Quaternion.identity));
            }
            else if (Value > 4.0f && Value <= 5.0f)
            {
                int Spone = Random.Range(0, RND_Value);
                
                //미니언 두~세마리 << 워리어,아처,메이지 또는 메이지, 메이지 둘중에 하나 출현
                int TypeCheck = Random.Range(0, 2);

                if(TypeCheck == 0)
                {
                    Vector3[] temp = SponePositionCheck(V3_RND_Posi[Spone], 3);

                    equipment(Instantiate(Goblin[0], temp[0], Quaternion.identity));
                    equipment(Instantiate(Goblin[1], temp[1], Quaternion.identity));
                    equipment(Instantiate(Goblin[2], temp[2], Quaternion.identity));
                }
                else
                {
                    Vector3[] temp = SponePositionCheck(V3_RND_Posi[Spone], 2);

                    equipment(Instantiate(Goblin[2], temp[0], Quaternion.identity));
                    equipment(Instantiate(Goblin[2], temp[1], Quaternion.identity));
                }
            }
            else if (Value > 5.0f && Value <= 6.0f)
            {
                int Spone = Random.Range(0, RND_Value);
                Vector3[] temp = SponePositionCheck(V3_RND_Posi[Spone], 3);

                //미니언 두~세마리 << 워리어,아처,메이지 또는 워리어,메이지,메이지 둘중에 하나 출현
                int TypeCheck = Random.Range(0, 2);

                if (TypeCheck == 0)
                {
                    equipment(Instantiate(Goblin[0], temp[0], Quaternion.identity));
                    equipment(Instantiate(Goblin[1], temp[1], Quaternion.identity));
                    equipment(Instantiate(Goblin[2], temp[2], Quaternion.identity));
                }
                else
                {
                    equipment(Instantiate(Goblin[0], temp[0], Quaternion.identity));
                    equipment(Instantiate(Goblin[2], temp[1], Quaternion.identity));
                    equipment(Instantiate(Goblin[2], temp[2], Quaternion.identity));
                }
            }
            else if (Value > 6.0f && Value <= 7.0f)
            {
                int Spone1 = Random.Range(0, RND_Value);
                int Spone2 = 0;

                do
                {
                    Spone2 = Random.Range(0, RND_Value);
                    if (Spone1 != Spone2)
                    {
                        break;
                    }
                }
                while (Spone1 == Spone2);

                Vector3[] temp1 = SponePositionCheck(V3_RND_Posi[Spone1], 2);

                //미니언 5~6 마리 << 랜덤하게 출현, 2명 그룹 1개, 3명 그룹 1개 
                equipment(Instantiate(Goblin[0], temp1[0], Quaternion.identity));
                equipment(Instantiate(Goblin[1], temp1[1], Quaternion.identity));
                //Instantiate(Goblin[Random.Range(0, 2)], temp[2], Quaternion.identity);

                Vector3[] temp2 = SponePositionCheck(V3_RND_Posi[Spone2], 3);
                equipment(Instantiate(Goblin[0], temp2[0], Quaternion.identity));
                equipment(Instantiate(Goblin[1], temp2[1], Quaternion.identity));
                equipment(Instantiate(Goblin[2], temp2[2], Quaternion.identity));
            }
            else if (Value > 7.0f && Value <= 8.0f)
            {
                int Spone1 = Random.Range(0, RND_Value);
                int Spone2 = 0;

                do
                {
                    Spone2 = Random.Range(0, RND_Value);
                    if (Spone1 != Spone2)
                    {
                        break;
                    }
                }
                while (Spone1 == Spone2);

                Vector3[] temp1 = SponePositionCheck(V3_RND_Posi[Spone1], 3);

                //미니언 6마리 << 3마리, 2그룹 출현
                equipment(Instantiate(Goblin[0], temp1[0], Quaternion.identity));
                equipment(Instantiate(Goblin[1], temp1[1], Quaternion.identity));
                equipment(Instantiate(Goblin[2], temp1[2], Quaternion.identity));
                //Instantiate(Goblin[Random.Range(0, 2)], temp[2], Quaternion.identity);

                Vector3[] temp2 = SponePositionCheck(V3_RND_Posi[Spone2], 3);
                equipment(Instantiate(Goblin[0], temp2[0], Quaternion.identity));
                equipment(Instantiate(Goblin[1], temp2[1], Quaternion.identity));
                equipment(Instantiate(Goblin[2], temp2[2], Quaternion.identity));  
            }
            else if (Value > 8.0f && Value <= 9.0f)
            {
                //미니언 6마리 << 3마리, 2그룹 출현(대신 메이지 비율이 높을 수 있음)
                int Spone1 = Random.Range(0, RND_Value);
                int Spone2 = 0;

                do
                {
                    Spone2 = Random.Range(0, RND_Value);
                    if (Spone1 != Spone2)
                    {
                        break;
                    }
                }
                while (Spone1 == Spone2);

                Vector3[] temp1 = SponePositionCheck(V3_RND_Posi[Spone1], 3);

                equipment(Instantiate(Goblin[0], temp1[0], Quaternion.identity));
                equipment(Instantiate(Goblin[1], temp1[1], Quaternion.identity));
                equipment(Instantiate(Goblin[2], temp1[2], Quaternion.identity));

                Vector3[] temp2 = SponePositionCheck(V3_RND_Posi[Spone2], 3);
                equipment(Instantiate(Goblin[Random.Range(0, 2)], temp2[0], Quaternion.identity));
                equipment(Instantiate(Goblin[Random.Range(1, 2)], temp2[1], Quaternion.identity));
                equipment(Instantiate(Goblin[Random.Range(1, 2)], temp2[2], Quaternion.identity));
            }
            else if (Value > 9.0f)
            {
                //미니언 6마리 << 3마리, 3메이지 1그룹, 랜덤그룹 1그룹 출현
                int Spone1 = Random.Range(0, RND_Value);
                int Spone2 = 0;
                int Spone3 = 0;

                do
                {
                    Spone2 = Random.Range(0, RND_Value);
                    if (Spone1 != Spone2)
                    {
                        break;
                    }
                }
                while (Spone1 == Spone2);

                do
                {
                    Spone3 = Random.Range(0, RND_Value);
                    if (Spone1 != Spone2 && Spone2 != Spone3 && Spone1 != Spone3)
                    {
                        break;
                    }
                }
                while (Spone1 == Spone2 || Spone2 == Spone3 || Spone1 == Spone3);

                Vector3[] temp1 = SponePositionCheck(V3_RND_Posi[Spone1], 3);

                equipment(Instantiate(Goblin[0], temp1[0], Quaternion.identity));
                equipment(Instantiate(Goblin[1], temp1[1], Quaternion.identity));
                equipment(Instantiate(Goblin[2], temp1[2], Quaternion.identity));

                Vector3[] temp2 = SponePositionCheck(V3_RND_Posi[Spone2], 3);

                equipment(Instantiate(Goblin[Random.Range(0, 2)], temp2[0], Quaternion.identity));
                equipment(Instantiate(Goblin[Random.Range(0, 2)], temp2[1], Quaternion.identity));
                equipment(Instantiate(Goblin[Random.Range(0, 2)], temp2[2], Quaternion.identity));

                Vector3[] temp3 = SponePositionCheck(V3_RND_Posi[Spone3], 3);

                equipment(Instantiate(Goblin[2], temp3[0], Quaternion.identity));
                equipment(Instantiate(Goblin[2], temp3[1], Quaternion.identity));
                equipment(Instantiate(Goblin[2], temp3[2], Quaternion.identity));
            }
        }
    }

    Vector3 RoomSizeCheck()
    {
        Vector3 RoomSize = gameObject.transform.localScale;

        return new Vector3(RoomSize.x * 5.0f, 0, RoomSize.y * 5.0f);
    }


    Vector3[] RegenPositon(Vector3 start, float Width, float Height, int rndNumber)
    {
        if(Width > Height)
        {
            if (rndNumber == 5)
            {
                Vector3[] Posi = {new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.5f), 0, start.z - (Height * 0.5f)),
                new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.75f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.75f))
            };

                return Posi;
            }
            else if (rndNumber == 6)
            {
                Vector3[] Posi = {new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.5f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.75f)),
                new Vector3(start.x + (Width * 0.5f), 0, start.z - (Height * 0.75f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.75f))
            };
                return Posi;
            }
            else if (rndNumber == 7)
            {
                Vector3[] Posi = {new Vector3(start.x + (Width * 0.2f), 0, start.z - (Height * 0.5f)),
                new Vector3(start.x + (Width * 0.35f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.35f), 0, start.z - (Height * 0.75f)),
                new Vector3(start.x + (Width * 0.5f), 0, start.z - (Height * 0.5f)),
                new Vector3(start.x + (Width * 0.65f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.65f), 0, start.z - (Height * 0.75f)),
                new Vector3(start.x + (Width * 0.8f), 0, start.z - (Height * 0.5f))
            };
                return Posi;
            }
            else
                return null;
        }
        else
        {
            if (rndNumber == 5)
            {
                Vector3[] Posi = {new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.5f), 0, start.z - (Height * 0.5f)),
                new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.75f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.75f))
            };

                return Posi;
            }
            else if (rndNumber == 6)
            {
                Vector3[] Posi = {new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.5f)),
                new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.75f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.25f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.5f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.75f))
            };
                return Posi;
            }
            else if (rndNumber == 7)
            {
                Vector3[] Posi = {new Vector3(start.x + (Width * 0.5f), 0, start.z - (Height * 0.2f)),
                new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.35f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.35f)),
                new Vector3(start.x + (Width * 0.5f), 0, start.z - (Height * 0.5f)),
                new Vector3(start.x + (Width * 0.25f), 0, start.z - (Height * 0.65f)),
                new Vector3(start.x + (Width * 0.75f), 0, start.z - (Height * 0.65f)),
                new Vector3(start.x + (Width * 0.5f), 0, start.z - (Height * 0.8f))
            };
                return Posi;
            }
            else
                return null;
        }
    }

    Vector3[] SponePositionCheck(Vector3 Spone, int Count)
    {
        if (Count == 2)
        {
            Vector3[] Spone_Position =
            { new Vector3(Spone.x - 0.5f, Spone.y, Spone.z),
            new Vector3(Spone.x + 0.5f, Spone.y, Spone.z) };

            return Spone_Position;
        }
        else if(Count == 3)
        {
            Vector3[] Spone_Position =
            { new Vector3(Spone.x, Spone.y, Spone.z + 0.5f),
            new Vector3(Spone.x - 0.5f, Spone.y, Spone.z - 0.5f),
            new Vector3(Spone.x + 0.5f, Spone.y, Spone.z - 0.5f) };

            return Spone_Position;
        }
        else
            return null;
    }


    void equipment(GameObject obj)
    {
        List<Transform> Temp = new List<Transform>();
   
        Transform[] tempTransforms = obj.GetComponentsInChildren<Transform>();


        if(tempTransforms[1].name.Contains("goblin_warrior"))
        {
            string[] bootName = { "goblin warrior boot A", "goblin warrior boot B" };
            int RND_boot = Random.Range(0, 2);

            foreach (Transform child in tempTransforms)
            {
                if (child.name.Contains(bootName[RND_boot]))
                {
                    Temp.Add(child);
                }
            }

            string[] WeaponName = {"goblin_arch", "goblin_quiver", "goblin_war_axe",
             "goblin_war_club","goblin_war_spear","goblin_war_sword" };

            int RND_W = Random.Range(2, 6);

            foreach (Transform child in tempTransforms)
            {
                for (int i = 0; i < WeaponName.Length; i++)
                {
                    if (RND_W != i)
                    {
                        if (child.name.Contains(WeaponName[i]))
                        {
                            Temp.Add(child);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < Temp.Count; i++)
            {
                Destroy(Temp[i].gameObject);
            }
        }
        else if (tempTransforms[1].name.Contains("goblin_ranger"))
        {
            string[] bootName = { "goblin ranger boot A", "goblin ranger boot B" };
            int RND_boot = Random.Range(0, 2);

            foreach (Transform child in tempTransforms)
            {
                if (child.name.Contains(bootName[RND_boot]))
                {
                    Temp.Add(child);
                }
            }

            string[] WeaponName = {"goblin_arch", "goblin_quiver", "goblin_ran_spear",
             "goblin_ran_shield", "goblin_ran_club","goblin_ran_axe", "goblin_ran_sword"};

            int RND_W = Random.Range(4, 8);

            foreach (Transform child in tempTransforms)
            {
                for (int i = 0; i < WeaponName.Length; i++)
                {
                    if (RND_W != i)
                    {
                        if (child.name.Contains(WeaponName[i]))
                        {
                            Temp.Add(child);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < Temp.Count; i++)
            {
                Destroy(Temp[i].gameObject);
            }
        }
        else if (tempTransforms[1].name.Contains("goblin_mage"))
        {
            string[] bootName = { "goblin mage boot A", "goblin mage boot B" };
            int RND_boot = Random.Range(0, 2);

            foreach (Transform child in tempTransforms)
            {
                if (child.name.Contains(bootName[RND_boot]))
                {
                    Temp.Add(child);
                }
            }

            string[] WeaponName = {"goblin mage staff A", "goblin mage staff B",
            "goblin mage staff C"};

            int RND_W = Random.Range(0, 3);

            foreach (Transform child in tempTransforms)
            {
                for (int i = 0; i < WeaponName.Length; i++)
                {
                    if (RND_W != i)
                    {
                        if (child.name.Contains(WeaponName[i]))
                        {
                            Temp.Add(child);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < Temp.Count; i++)
            {
                Destroy(Temp[i].gameObject);
            }
        }
    }

}
