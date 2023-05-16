using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddNode : MonoBehaviour
{
    InputField IDIF, NameIF;
    Button CancelBtn, OkBtn;

    // Start is called before the first frame update
    void Start()
    {
        IDIF = transform.Find("IDInputField").gameObject.GetComponent<InputField>();
        NameIF = transform.Find("NameInputField").gameObject.GetComponent<InputField>();
        CancelBtn = transform.Find("CancelBtn").gameObject.GetComponent<Button>();
        OkBtn = transform.Find("OKBtn").gameObject.GetComponent<Button>();

        OkBtn.onClick.AddListener(delegate
        {
             if(!IDIF.text.Equals("") && !NameIF.text.Equals(""))
            {
                //정보 이용해서 노드 추가
                //기본 위치는 0,0,0
                //NodeGroup Object 필요
                //Prefab 필요
                //Prefab에 들어갈 스크립트 필요
                //스크립트에 들어갈 데이터 셋팅 필요
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
