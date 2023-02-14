using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    [Header("- 게임 데이터")]
    [SerializeField] private string 길;
    [Space]
    [SerializeField] private int 지식;
    [SerializeField] private int 체력;
    [SerializeField] private int 정신;
    [SerializeField] private int 매력;

    [Header("- 발생 이벤트")]
    [SerializeField] private string ID;
    [SerializeField] private int 가중치;
    [SerializeField] private string 선택타입;
    [SerializeField] private bool 대화형;

    [Serializable]
    public struct 이벤트
    {
        public string 아이디;
        public int 가중치;
        public string 선택타입;
        public bool 대화형;

        public 이벤트(string i, int w, string s, bool c)
        {
            아이디 = i;
            가중치 = w;
            선택타입 = s;
            대화형 = c;
        }
    }

    [Header("- 이벤트 리스트")]
    public List<이벤트> 이벤트피스;


    private static DebugTest _instance;
    public static DebugTest Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(DebugTest)) as DebugTest;

                if (_instance == null) Debug.Log("싱글톤 오류");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        /*
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        */
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetEventPiece(List<EventClass> list)
    {
        이벤트피스.Clear();
        for(int i=0; i < list.Count; i++)
        {
            이벤트피스.Add(new 이벤트(list[i].EventID, list[i].weight, list[i].select_type, list[i].conversation_type));
        }
    }

    public void PrintGameStat(GameData data)
    {
        지식 = data.stat[0];
        체력 = data.stat[1];
        정신 = data.stat[2];
        매력 = data.stat[3];
    }

    public void EventData(EventClass e)
    {
        ID = e.EventID;
        가중치 = e.weight;
        선택타입 = e.select_type;
        대화형 = e.conversation_type;
    }

    public void SetRoadData(string r) { 길 = r; }
    public string GetRoadData() { return 길; }
}
