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


    public delegate void EventFunc(EventClass e);
    public delegate void ChangeStatFunc(int k, int s, int m, int c);
    public static event ChangeStatFunc ChangeStat;
    public static event EventFunc InteractionEvent;
    public static event EventFunc OneWayEvent;


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
        InteractionEvent += view.InteractionEvenView;
        OneWayEvent += view.OneWayEventView;//���߿� Action���� ����
        ChangeStat += model.ChangeStat;



        //������ �о���� : ����
        //�ʱ� ������ ����
    }

    //Not yet
    public delegate void VoidFunc();
    public static event VoidFunc SelectRoad;


    //����
    [SerializeField] Transform SpawnPosition;
    public float MoveSpeed;
    GameObject BackPrefab, EventPrefab;
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
        BackPrefab = Resources.Load<GameObject>("Prefabs/BackGround");
        EventPrefab = Resources.Load<GameObject>("Prefabs/Event");
        SetNewEventData();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeStop)
        {
            if (GameTime >= 60.0f)//�б��� üũ
            {
                SelectRoad();
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

    public void SetNewEventData()
    {
        EventTime = Random.Range(5.0f, 8.0f);
        NextEvent = GameManager.EventData[Random.Range(0, GameManager.EventData.Count)];
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
}
