using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using static GameController;

public class GameController : MonoBehaviour
{
    GameModel model;
    GameView view;

    public delegate void VoidFunc();
    public delegate void EventFunc(EventClass e);
    public delegate void ChangeStatFunc(int k, int s, int m, int c);
    public delegate void RoadEvent(GameData gameData);
    public delegate void StrFunc(string str);

    public static event VoidFunc InitGame;
    public static event EventFunc NewEvent;
    public static event EventFunc InteractionEvent;
    public static event EventFunc OneWayEvent;
    public static event ChangeStatFunc ChangeStat;
    public static event RoadEvent Intersection;
    public static event StrFunc ChangeRoad;


    //Singleton
    private static GameController _intstance;
    public static GameController Instance { get { return _intstance; } }


    private void Awake()
    {
        //Singleton
        if (_intstance == null)
            _intstance = this;
        else if (_intstance != this)
            Destroy(this.gameObject);

        //MVC
        model = new GameModel();
        view = gameObject.GetComponent<GameView>();

        //Connect Event
        InitGame += model.EventSet;
        InitGame += SetNewEventData;

        NewEvent += view.NewEventView;

        InteractionEvent += view.InteractionEvenView;
        OneWayEvent += view.OneWayEventView;//나중에 Action으로 수정

        ChangeStat += model.ChangeStat;

        Intersection += view.IntersectionEventView;

        ChangeRoad += model.ChangeRoad;
        ChangeRoad += view.ChangeRoadView;
        ChangeRoad += NewRoadResult;


        //데이터 읽어오기 : 나중
        //초기 데이터 셋팅
        
    }

    [Header("게임 조정 변수들")]
    //변수
    [HideInInspector] public EventClass NextEvent;
    [HideInInspector] public bool TimeStop = false;//시간 흐름 제어 변수 -> 추후 이벤트나 액션으로 조정 고려
    float GameTime;//게임 진행 시간 : 분기점 측정 시간(1분)
    float EventTime;//이벤트 발생 간격 시간 : 이벤트 발생 간격 시간(5~8초)
    float tmp_time;//tmp_time : 시간 비교 변수
    [SerializeField] float IntersectionTime;


    // Start is called before the first frame update
    void Start()
    {
        InitGame();
        //변수 초기화 : 추후 수정
        GameTime = 0.0f;
        view.ChangeTime(GameTime);
        view.ChangeStatView(model.gameData);
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeStop)
        {
            if (GameTime >= IntersectionTime)//분기점 체크
            {
                Intersection(model.gameData);
                TimeStop = true;
            }
            else if (tmp_time >= EventTime)//이벤트 발생
            {
                NewEvent(model.NewEventNum());
                SetNewEventData();
            }
            else//시간 흐름
            {
                tmp_time += Time.deltaTime;
                GameTime += Time.deltaTime;
                view.ChangeTime(GameTime);
            }
        }
    }

    public void SetNewEventData()
    {
        EventTime = Random.Range(5.0f, 8.0f);
        tmp_time = 0.0f;
        TimeStop = false;
    }



    public void InteractinoEventOn(EventClass e)
    {
        TimeStop = true;
        InteractionEvent(e);
    }
    public void InteractionViewResult(int k, int s, int m, int c)
    {
        ChangeStat(k, s, m, c);
    }
    public void EventOff(GameData data) 
    {
        view.ChangeStatView(data);
        TimeStop = false; 
    }


    public void OneWayEventOn(EventClass e)
    {
        ChangeStat(e.result1_knowledge, e.result1_strength, e.result1_mental, e.result1_charm);
        OneWayEvent(e);
    }


    public static void IntersectionViewResult(string roadID)
    {
        ChangeRoad(roadID);
    }
    void NewRoadResult(string id)
    {
        TimeStop = false;
        GameTime = 0.0f;

        InitGame();
    }


}
