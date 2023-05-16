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

    [Header("관리")]
    public TestEndingNode DragNode = null;
    public TestEndingNode StartDrag = null;
    public TestEndingNode EndDrag = null;

    [Header("노드")]
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
        //프리팹에 스크립트 없음.

        if (Root == null)
        {
            Root = new TestEndingData();

            //아무것도 없을 때 : root 노드 생성
            temp = Instantiate(prefab);
            temp.transform.parent = nodegroup.transform;

            temp.name = "길";
            Root.SetData("n1", "길", Vector3.zero, null);
            temp.AddComponent<TestEndingNode>().data = Root;

            RootNode = temp.GetComponent<TestEndingNode>();
        }
        else
        {
            //있을 때 : 1) RootData만 있음 / 2) DataDictionqry 있음
            // 1) RootData만 있을 때 : Root에 Data 담겨있음
            Traversal(Root);

            // 2) Data Dictionary에 다 담겨있을 때

        }
    }

    private void Traversal(TestEndingData data)
    {
        if(data != null)
        {
            //노드 추가
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
