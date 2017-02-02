using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[System.Serializable]
public class SaveInfomation
{
    public string name;
    public float posX;
    public float posY;
    public float posZ;
}


public class SavePoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveInfomation setInfo = new SaveInfomation();
            setInfo.name = "SaveInfo";
            setInfo.posX = 0.0f;
            setInfo.posY = 4.5f;
            setInfo.posZ = 5.5f;

            Debug.Log(setInfo); //"SaveInfo"
            Debug.Log(setInfo.name);

            BinaryFormatter formmater = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();

            formmater.Serialize(memStream, setInfo);
            byte[] bytes = memStream.GetBuffer();
            string memstr = Convert.ToBase64String(bytes);

            PlayerPrefs.SetString("SaveInfomation", memstr);
            Debug.Log(memstr);


            string getInfo = PlayerPrefs.GetString("SaveInfomation");
            Debug.Log(getInfo);

            byte[] getBytes = Convert.FromBase64String(getInfo);
            MemoryStream getMemStream = new MemoryStream(getBytes);

            BinaryFormatter formatter2 = new BinaryFormatter();

            SaveInfomation getInfomation = (SaveInfomation)formatter2.Deserialize(getMemStream);

            Debug.Log(getInfomation);
            Debug.Log(getInfomation.name);

        }

        
    }

    void funtionkey()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (PlayerPrefs.HasKey("ID"))
            {
                string getID = PlayerPrefs.GetString("ID");
                Debug.Log(string.Format("ID : {0}", getID));
            }
            else
            {
                Debug.Log("ID 없슴");
            }
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!PlayerPrefs.HasKey("ID"))
            {
                Debug.Log("이미 아이디가 존재함");
            }
            else
            {
                string setID = "PlayerID";
                PlayerPrefs.SetString("ID", setID);
                Debug.Log("Save : " + setID);
            }

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.SetInt("INT", 33);
            PlayerPrefs.SetFloat("FLOAT", 44.4f);

            int getInteger = PlayerPrefs.GetInt("INT");
            float getFloat = PlayerPrefs.GetFloat("FLOAT");

            Debug.Log(getInteger.ToString());
            Debug.Log(getFloat.ToString());
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //디폴트 키가 없으면 66을 가져와라
            int getInteger = PlayerPrefs.GetInt("INT2", 66);
            float getFloat = PlayerPrefs.GetFloat("FLOAT2", 77.7f);
            string getString = PlayerPrefs.GetString("ID2", "NONE");

            Debug.Log(getInteger.ToString());
            Debug.Log(getFloat.ToString());
            Debug.Log(getString.ToString());
        }

        if (Input.GetMouseButtonDown(1))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Data Delete");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (PlayerPrefs.HasKey("ID"))
            {
                PlayerPrefs.DeleteKey("ID");
                Debug.Log("ID key Delete");
            }
        }

        PlayerPrefs.Save();
    }
}
