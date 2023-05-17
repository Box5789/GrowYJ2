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
    public GameObject DragNode = null;
    public GameObject StartDrag = null;
    public GameObject EndDrag = null;

    [Header("노드")]
    GameObject temp;
    [SerializeField] GameObject nodegroup;
    [SerializeField] GameObject prefab;

    public TestEndingData Root = null;
    public TestEndingNode RootNode = null;

    Button SaveBtn;
    SaveNode save;


    private void SetTestEndingNode()
    {
        nodegroup = GameObject.Find("EndingNodeGroup").gameObject;
        //프리팹에 스크립트 없음.

        if (Root == null)
        {
            Root = new TestEndingData();

            temp = Instantiate(prefab);
            temp.transform.parent = nodegroup.transform;

            temp.name = "길";
            Root.SetData("n1", "길", Vector3.zero, null);
            temp.AddComponent<TestEndingNode>().data = Root;

            RootNode = temp.GetComponent<TestEndingNode>();
        }
        else
        {
            Traversal(Root);
        }
    }

    private void Traversal(TestEndingData data)
    {
        if(data != null)
        {
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
