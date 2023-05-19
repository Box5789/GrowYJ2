using System.Collections;
using System.Collections.Generic;
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

        OkBtn.onClick.AddListener(delegate
        {
            
            if (!IDIF.text.Equals("") && !NameIF.text.Equals(""))
            {
                //TODO : 노드 키 중복 확인

                GameObject node = Instantiate(Resources.Load<GameObject>("Prefabs/EndingNode"));
                node.transform.parent = GameObject.Find("EndingNodeGroup").gameObject.transform;
                
                string id, name;
                id = IDIF.text; 
                name = NameIF.text; 

                TestEndingData data = new TestEndingData();
                data.SetData(id, name, Vector3.zero, null);

                node.AddComponent<TestEndingNode>().data = data;

                IDIF.text = "n" + (++EEM.Instance.Index).ToString();
                NameIF.text = "";

                gameObject.SetActive(false);
            }
        });
    }

}
