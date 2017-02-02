using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager sInstance;
    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObject = new GameObject("_GameManager");
                sInstance = newGameObject.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }

    public int changeScene = 0;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

    }
    public void ChangeScene(string str)
    {
        if(str == "Title")
        {
            SceneManager.LoadScene("Tank");
        }        
        else if(str == "Tank")
        {
            SceneManager.LoadScene("End");
        }
        else if(str == "End")
        {
            SceneManager.LoadScene("Title");
        }
    }
}
