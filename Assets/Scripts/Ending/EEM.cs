using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EEM : MonoBehaviour
{
    private static EEM instance;
    public static EEM Instance
    {
        get
        {
            if(instance == null)
                return null;
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [Header("����")]
    public TestEndingNode DragNode = null;
    public TestEndingNode StartDrag = null;
    public TestEndingNode EndDrag = null;

    [Header("���")]
    GameObject temp;
    [SerializeField] GameObject nodegroup;
    [SerializeField] GameObject prefab;

    public TestEndingData Root = null;
    public TestEndingNode RootNode = null;

    Button SaveBtn;
    SaveNode save;

    //public Dictionary<string, TestEndingData> END = new Dictionary<string, TestEndingData>();

    private void SetTestEndingNode()
    {
        nodegroup = GameObject.Find("EndingNodeGroup").gameObject;
        //�����տ� ��ũ��Ʈ ����.

        if (Root == null)
        {
            Root = new TestEndingData();

            //�ƹ��͵� ���� �� : root ��� ����
            temp = Instantiate(prefab);
            temp.transform.parent = nodegroup.transform;

            temp.name = "��";
            Root.SetData("n1", "��", Vector3.zero, null);
            temp.AddComponent<TestEndingNode>().data = Root;

            RootNode = temp.GetComponent<TestEndingNode>();
        }
        else
        {
            //���� �� : 1) RootData�� ���� / 2) DataDictionqry ����
            // 1) RootData�� ���� �� : Root�� Data �������
            Traversal(Root);

            // 2) Data Dictionary�� �� ������� ��

        }
    }

    private void Traversal(TestEndingData data)
    {
        if(data != null)
        {
            //��� �߰�
            temp = Instantiate(prefab, nodegroup.transform.position, nodegroup.transform.rotation);

            temp.name = data.GetName();
            temp.transform.position = data.GetPosition();
            temp.AddComponent<TestEndingNode>().data = data;

            foreach (TestEndingData child in data.GetChildrens())
            {
                Traversal(child);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nodegroup = GameObject.Find("EndingNodeGroup").gameObject;
        prefab = Resources.Load<GameObject>("Prefabs/EndingNode");
        save = new SaveNode();

        Root = save.LoadData();
        SetTestEndingNode();

        SaveBtn = GameObject.Find("Canvas").transform.Find("SaveBtn").gameObject.GetComponent<Button>();
        SaveBtn.onClick.AddListener(delegate
        {
            var temp = Root;
            save.SaveData(temp);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
