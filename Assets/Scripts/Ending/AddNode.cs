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

        //TODO : 판넬 데이터 셋팅
        IDIF.text = "n" + (EEM.Index + 1).ToString();
        NameIF.text = "";

        OkBtn.onClick.AddListener(delegate
        {
            if (!IDIF.text.Equals("") && !NameIF.text.Equals(""))//공백 확인
            {
                string id, name;
                id = IDIF.text;
                name = NameIF.text;

                // 노드 ID 확인
                if (id[0].Equals("n") && id.Substring(1).All(char.IsDigit) && EEM.Instance.TraversalKeyCheck(EEM.Instance.Root, id))
                {
                    // 노드 Name 확인
                    if (EEM.Instance.TraversalNameCheck(EEM.Instance.Root, name))
                    {
                        //노드 셋팅
                        GameObject node = Instantiate(Resources.Load<GameObject>("Prefabs/EndingNode"));
                        node.transform.parent = GameObject.Find("EndingNodeGroup").gameObject.transform;

                        //노드 데이터 셋팅
                        TestEndingData data = new TestEndingData();
                        data.SetData(id, name, Vector3.zero, null);
                        node.AddComponent<TestEndingNode>().data = data;

                        EEM.Index++;

                        //노드 추가 종료
                        gameObject.SetActive(false);
                    }
                    else //이름 중복일 경우 이름 입력란에 포커스
                    {
                        Debug.Log("Name 중복");
                        NameIF.ActivateInputField();
                        NameIF.MoveTextEnd(false);
                    }
                }
                else//ID 중복일 경우 ID 입력란에 포커스
                {
                    Debug.Log("ID 중복 or ID 형태 인식 불가");
                    IDIF.ActivateInputField();
                    IDIF.MoveTextEnd(false);
                }
            }
        });
    }

}
