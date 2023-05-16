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

        CancelBtn.onClick.AddListener(delegate
        {

        });

        OkBtn.onClick.AddListener(delegate
        {
             
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
