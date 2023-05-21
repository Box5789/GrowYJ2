using System.Collections;
using System.Collections.Generic;
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
        IDIF.text = "n" + (EEM.Index + 1).ToString();
        NameIF.text = "";

        OkBtn.onClick.AddListener(delegate
        {
            if (!IDIF.text.Equals("") && !NameIF.text.Equals(""))//���� Ȯ��
            {
                string id, name;
                id = IDIF.text;
                name = NameIF.text;

                // TODO : IF�� ���� ����
                if (id[0].Equals("n") && id.Substring(1).All(char.IsDigit))//ID Ȯ��
                {
                    // ��� ID �ߺ� Ȯ��
                    if (EEM.Instance.TraversalKeyCheck(EEM.Instance.Root, id) )
                    {   // ��� Name �ߺ� Ȯ��
                        if (EEM.Instance.TraversalNameCheck(EEM.Instance.Root, name))
                        {
                            //��� ����
                            GameObject node = Instantiate(Resources.Load<GameObject>("Prefabs/EndingNode"));
                            node.transform.parent = GameObject.Find("EndingNodeGroup").gameObject.transform;

                            //��� ������ ����
                            TestEndingData data = new TestEndingData();
                            data.SetData(id, name, Vector3.zero, null);
                            node.AddComponent<TestEndingNode>().data = data;

                            EEM.Index++;

                            //��� �߰� ����
                            gameObject.SetActive(false);
                        }
                        else //�̸� �ߺ��� ��� �̸� �Է¶��� ��Ŀ��
                        {
                            NameIF.ActivateInputField();
                        }
                    }
                    else //ID �ߺ��� ��� ID �Է¶��� ��Ŀ��
                    {
                        IDIF.ActivateInputField();
                        IDIF.MoveTextEnd(false);
                    }
                }
                else
                {

                }
            }
        });
    }

}
