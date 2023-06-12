using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //DataPath
    const string event_filepath = "/Resources/Data/Event.csv";
    const string road_filepath = "/Resources/Data/Road.csv";
    const string save_filename = "GameDataFile.json";
    //const string endingnodeposition_filename = "EndingNodePosition.json";


    //DataSheet
    //Dictionary<string, InteractionEventData> InteractionData = new Dictionary<string, InteractionEventData>();
    //Dictionary<string, OneSideEventData> OneSideData = new Dictionary<string, OneSideEventData>();
    public static List<EventClass> EventData = new List<EventClass>();
    public static Dictionary<string, RoadEventData> RoadData = new Dictionary<string, RoadEventData>();



    //Game State
    public bool Pause = false;
    public int Point;


    //Stat
    public enum stat_name 
    { 
        Knowledge = 0,
        Strength = 1,
        Mental = 2,
        Charm = 3
    }
    public readonly int StatCount = System.Enum.GetValues(typeof(stat_name)).Length;



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
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        //Read Game Data
        ReadEventSheet();
        ReadRoadSheet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Read Data Sheet - ÃßÈÄ ¼öÁ¤
    void ReadEventSheet()
    {
        StreamReader sr = new StreamReader(Application.dataPath + event_filepath);

        bool first = true;

        while (true)
        {
            string data_String = sr.ReadLine();

            if (data_String == null)
            {
                break;
            }
            else if (first)
            {
                first= false;
            }
            else
            {
                EventData.Add(new EventClass(data_String.Split('/')));
            }
        }
    }
    void ReadRoadSheet()
    {
        StreamReader sr = new StreamReader(Application.dataPath + road_filepath);

        bool first = true;

        while (true)
        {
            string data_String = sr.ReadLine();

            if (data_String == null)
            {
                break;
            }
            else if (first)
            {
                first = false;
            }
            else
            {
                var data_values = data_String.Split('/');

                RoadData.Add(data_values[0], new RoadEventData(data_values));
            }
        }
    }

    void SaveData()
    {

    }


    //Pause
    public void OnPause()
    {
        Pause = true;
    }


    //SceneManager
    public void GotoMain() { SceneManager.LoadScene("Main"); }
}
