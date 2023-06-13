using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddNode : MonoBehaviour
{
    Button OkBtn;
    TMP_InputField IDIF, NameIF;
    
    // Start is called before the first frame update
    void Start()
    {   
        OkBtn = transform.Find("OKBtn").gameObject.GetComponent<Button>();
        IDIF = transform.Find("IDInputField").gameObject.GetComponent<TMP_InputField>();
        NameIF = transform.Find("NameInputField").gameObject.GetComponent<TMP_InputField>();

        //TODO : �ǳ� ������ ����
        IDIF.text = "n" + (EEM.Instance.Index).ToString();
        NameIF.text = "";

        OkBtn.onClick.AddListener(delegate
        {
            if (!IDIF.text.Equals("") && !NameIF.text.Equals(""))//���� Ȯ��
            {
                string id, name;
                id = IDIF.text;
                name = NameIF.text;

                // ��� ID Ȯ��
                if (id[0].Equals('n') && id.Substring(1).All(char.IsDigit) && EEM.Instance.TraversalKeyCheck(EEM.Instance.Root, id))
                {
                    // ��� Name Ȯ��
                    if (EEM.Instance.TraversalNameCheck(EEM.Instance.Root, name))
                    {
                        //��� ����
                        GameObject node = Instantiate(Resources.Load<GameObject>("Prefabs/TestEndingNode"));
                        node.transform.position = Vector3.zero;
                        node.transform.parent = GameObject.Find("EndingNodeGroup").gameObject.transform;

                        //��� ������ ����
                        TestEndingData data = new TestEndingData();
                        data.SetData(id, name, Vector3.zero);
                        node.AddComponent<TestEndingNode>().SetData(data);
                        node.name = data.GetID() + ", " + data.GetName();

                        EEM.Instance.Index++;

                        //��� �߰� ����
                        IDIF.text = "n" + (EEM.Instance.Index).ToString();
                        NameIF.text = "";
                        gameObject.SetActive(false);
                    }
                    else //�̸� �ߺ��� ��� �̸� �Է¶��� ��Ŀ��
                    {
                        Debug.Log("Name �ߺ�");
                        NameIF.ActivateInputField();
                        NameIF.MoveTextEnd(false);
                    }
                }
                else//ID �ߺ��� ��� ID �Է¶��� ��Ŀ��
                {
                    Debug.Log("ID �ߺ� or ID ���� �ν� �Ұ� : " + id);
                    IDIF.ActivateInputField();
                    IDIF.MoveTextEnd(false);
                }
            }
        });
    }

}