using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null) Debug.Log("½Ì±ÛÅæ ¿À·ù");
            }

            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public ResourceManager Resource { get; set; }
    public DataManager Data { get; set; }
    public GameData gameData { get; set; }


    //Game State
    public bool Pause = false;
    JSONManager jsonManager;


    void Start()
    {
        Resource = new ResourceManager();
        Data = new DataManager();

        Resource.Init();
        Data.Init();

        jsonManager = new JSONManager();
        gameData = jsonManager.LoadGameData();
        gameData.init();
        if (gameData == null || gameData.road_id == null)
        {
            gameData.init();
        }
    }

    //DataPath
    //const string endingnodeposition_filename = "EndingNodePosition.json";


    //Pause
    public void OnPause()
    {
        Pause = true;
    }


}
