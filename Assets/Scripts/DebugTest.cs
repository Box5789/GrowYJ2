using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    [Header("- ���� ������")]
    [SerializeField] private string ��;
    [Space]
    [SerializeField] private int ����;
    [SerializeField] private int ü��;
    [SerializeField] private int ����;
    [SerializeField] private int �ŷ�;

    [Header("- �߻� �̺�Ʈ")]
    [SerializeField] private string ID;
    [SerializeField] private int ����ġ;
    [SerializeField] private string ����Ÿ��;
    [SerializeField] private bool ��ȭ��;

    [Serializable]
    public struct �̺�Ʈ
    {
        public string ���̵�;
        public int ����ġ;
        public string ����Ÿ��;
        public bool ��ȭ��;

        public �̺�Ʈ(string i, int w, string s, bool c)
        {
            ���̵� = i;
            ����ġ = w;
            ����Ÿ�� = s;
            ��ȭ�� = c;
        }
    }

    [Header("- �̺�Ʈ ����Ʈ")]
    public List<�̺�Ʈ> �̺�Ʈ�ǽ�;


    private static DebugTest _instance;
    public static DebugTest Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(DebugTest)) as DebugTest;

                if (_instance == null) Debug.Log("�̱��� ����");
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
        �̺�Ʈ�ǽ�.Clear();
        for(int i=0; i < list.Count; i++)
        {
            �̺�Ʈ�ǽ�.Add(new �̺�Ʈ(list[i].EventID, list[i].weight, list[i].select_type, list[i].conversation_type));
        }
    }

    public void PrintGameStat(GameData data)
    {
        ���� = data.stat[0];
        ü�� = data.stat[1];
        ���� = data.stat[2];
        �ŷ� = data.stat[3];
    }

    public void EventData(EventClass e)
    {
        ID = e.EventID;
        ����ġ = e.weight;
        ����Ÿ�� = e.select_type;
        ��ȭ�� = e.conversation_type;
    }

    public void SetRoadData(string r) { �� = r; }
    public string GetRoadData() { return ��; }
}
