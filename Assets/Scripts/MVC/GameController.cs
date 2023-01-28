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
        InitGame += SetEventPiece;
        InitGame += SetNewEventData;
        InteractionEvent += view.InteractionEvenView;
        OneWayEvent += view.OneWayEventView;//���߿� Action���� ����
        ChangeStat += model.ChangeStat;
        Intersection += view.IntersectionEventView;
        ChangeRoad += model.ChangeRoad;
        ChangeRoad += view.ChangeRoadView;



        //������ �о���� : ����
        //�ʱ� ������ ����
    }


    //������
    List<EventClass> EventPiece = new List<EventClass>();


    //����
    [SerializeField] Transform SpawnPosition;
    public float MoveSpeed;
    GameObject EventPrefab;
    EventClass NextEvent;
    public bool TimeStop = false;//�ð� �帧 ���� ���� -> ���� �̺�Ʈ�� �׼����� ���� ���
    float GameTime;//���� ���� �ð� : �б��� ���� �ð�(1��)
    float EventTime;//�̺�Ʈ �߻� ���� �ð� : �̺�Ʈ �߻� ���� �ð�(5~8��)
    float tmp_time;//tmp_time : �ð� �� ����


    // Start is called before the first frame update
    void Start()
    {
        //���� �ʱ�ȭ : ���� ����
        GameTime = 0.0f;
        EventPrefab = Resources.Load<GameObject>("Prefabs/Event");
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeStop)
        {
            if (GameTime >= 60.0f)//�б��� üũ
            {
                Intersection(model.gameData);
                TimeStop = true;
            }
            else if (tmp_time >= EventTime)//�̺�Ʈ �߻�
            {
                Instantiate(EventPrefab, SpawnPosition.position, SpawnPosition.rotation).GetComponent<EventObject>().SetEventClass(NextEvent);//������Ʈ Ǯ������ ����
                SetNewEventData();
            }
            else//�ð� �帧
            {
                tmp_time += Time.deltaTime;
                GameTime += Time.deltaTime;
            }
        }
    }

    private void SetEventPiece()
    {
        EventPiece.Clear();
        foreach (EventClass event_piece in GameManager.EventData)
        {
            if (event_piece.roadID.Equals(model.gameData.road_ID))
            {
                EventPiece.Add(event_piece);
            }
        }
    }
    public void SetNewEventData()
    {
        EventTime = Random.Range(5.0f, 8.0f);
        NextEvent = EventPiece[Random.Range(0, EventPiece.Count)];
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
    public void EventOff() { TimeStop = false; }


    public void OneWayEventOn(EventClass e)
    {
        ChangeStat(e.result1_knowledge, e.result1_strength,
                        e.result1_mental, e.result1_charm);
        OneWayEvent(e);
    }


    public static void IntersectionViewResult(string roadID)
    {
        ChangeRoad(roadID);
    }
}
