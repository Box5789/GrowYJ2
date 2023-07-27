using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

public class GameController : MonoBehaviour
{
    GameModel model;
    GameView view;

    /*
    public delegate void VoidFunc();
    public delegate void EventFunc(EventClass e);
    public delegate void ChangeStatFunc(int[] stat);
    public delegate void RoadEvent(GameData gameData);
    public delegate void StrFunc(string str);

    public static event VoidFunc InitGame;
    public static event EventFunc NewEvent;
    public static event EventFunc InteractionEvent;
    public static event EventFunc OneWayEvent;
    public static event ChangeStatFunc ChangeStat;
    public static event RoadEvent Intersection;
    public static event StrFunc ChangeRoad;
    */

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


        //Connect Event
        /*
        InitGame += model.EventSet;
        InitGame += SetNewEventData;

        NewEvent += view.NewEventView;

        InteractionEvent += view.InteractionEvenView;
        OneWayEvent += view.OneWayEventView;//���߿� Action���� ����

        ChangeStat += model.ChangeStat;

        Intersection += view.IntersectionEventView;

        ChangeRoad += model.ChangeRoad;
        ChangeRoad += view.ChangeRoadView;
        ChangeRoad += NewRoadResult;
        */
    }

    [Header("���� ���� ������")]
    [HideInInspector] public EventClass NextEvent;
    [HideInInspector] public bool TimeStop = false;//�ð� �帧 ���� ���� -> ���� �̺�Ʈ�� �׼����� ���� ���
    float GameTime;//���� ���� �ð� : �б��� ���� �ð�(1��)
    float EventTime;//�̺�Ʈ �߻� ���� �ð� : �̺�Ʈ �߻� ���� �ð�(5~8��)
    float tmp_time;//tmp_time : �ð� �� ����
    [SerializeField] float IntersectionTime;

    public Dictionary<string, GameObject> Objects;
    public string ConectionObjectKey;

    void Start()
    {
        Objects = new Dictionary<string, GameObject>();

        model = new GameModel();
        view = gameObject.GetComponent<GameView>();

        InitGame();
    }

    void InitGame()
    {
        model.EventSet();
        SetNewEventData();
        GameTime = 0.0f;
        view.ChangeTime(GameTime);
        view.ChangeStatView(GameManager.Instance.gameData);
    }
    // Update is called once per frame
    void Update()
    {
        if (!TimeStop && !GameManager.Instance.Pause)
        {
            if (GameTime >= IntersectionTime)//�б��� üũ
            {
                view.IntersectionEventView(GameManager.Instance.gameData);
                TimeStop = true;
            }
            else if (tmp_time >= EventTime)//�̺�Ʈ �߻�
            {
                view.NewEventView(model.NewEventNum());
                SetNewEventData();
            }
            else//�ð� �帧
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
        view.InteractionEvenView(e);

        DebugTest.Instance.EventData(e);
    }
    public void InteractionViewResult(int[] stat)
    {
        model.ChangeStat(stat);
    }
    public void EventOff(GameData data) 
    {
        view.ChangeStatView(data);
        TimeStop = false; 
    }


    public void OneWayEventOn(EventClass e)
    {
        model.ChangeStat(e.result1);
        view.OneWayEventView(e);

        DebugTest.Instance.EventData(e);
    }


    public void IntersectionViewResult(string roadID)
    {
        model.ChangeRoad(roadID);
        view.ChangeRoadView(roadID);
        NewRoadResult(roadID);
    }
    void NewRoadResult(string id)
    {
        TimeStop = false;
        GameTime = 0.0f;

        InitGame();
    }
}
