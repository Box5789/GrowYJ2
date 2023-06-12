using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public int Index = 1;//next node index
    public TestEndingData Root;

    GameObject temp;
    GameObject nodegroup;
    GameObject prefab;

    Button SaveBtn, BackBtn;
    SaveNode save;


    private void SetTestEndingNode(TestEndingData savedata)
    {
        nodegroup = GameObject.Find("EndingNodeGroup").gameObject;

        if (savedata == null)
        {
            Root = new TestEndingData();

            temp = Instantiate(prefab);
            temp.transform.parent = nodegroup.transform;

            temp.name = "n1, 길";
            Root.SetData("n1", "길", Vector3.zero);
            temp.AddComponent<TestEndingNode>().SetData(Root);

            Index++;
        }
        else
        {
            Root = savedata;
            TraversalView(Root);
        }
    }

    private void TraversalView(TestEndingData data)
    {
        if(data != null)
        {
            Index++;
            temp = Instantiate(prefab);
            temp.name = data.GetID() + ", " + data.GetName();
            temp.transform.position = data.GetPosition();
            temp.transform.parent = nodegroup.transform;
            temp.AddComponent<TestEndingNode>().SetData(data);

            //TODO : Parent & Child GameObject Setting


            foreach (TestEndingData child in data.GetChildrensData())
            {
                TraversalView(child);
            }
        }
    }

    public bool TraversalKeyCheck(TestEndingData data, string key)
    {
        if (data.GetID().Equals(key))
        {
            Debug.Log(data.GetID() + " 노드 있음 : " + key);
            return false;
        }
        else if (data.GetChildrensData() != null && data.GetChildrensData().Count != 0)
        {
            foreach(TestEndingData child in data.GetChildrensData())
            {
                if (!TraversalKeyCheck(child, key))
                    return false;
            }
        }
        
        return true;
    }

    public bool TraversalNameCheck(TestEndingData data, string name)
    {
        if (data.GetName().Equals(name))
            return false;
        else if (data.GetChildrensData() != null && data.GetChildrensData().Count != 0)
        {
            foreach (TestEndingData child in data.GetChildrensData())
                if (!TraversalNameCheck(child, name)) return false;
        }

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        nodegroup = GameObject.Find("EndingNodeGroup").gameObject;
        prefab = Resources.Load<GameObject>("Prefabs/TestEndingNode");
        save = new SaveNode();

        SetTestEndingNode(save.LoadData());

        SaveBtn = GameObject.Find("SaveBtn").gameObject.GetComponent<Button>();
        SaveBtn.onClick.AddListener(delegate
        {
            TestEndingData temp_test = Root;
            save.SaveData(temp_test);
            SceneManager.LoadScene("Home");
        });

        BackBtn = GameObject.Find("BackBtn").gameObject.GetComponent<Button>();
        BackBtn.onClick.AddListener(() => { SceneManager.LoadScene("Home"); });
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
