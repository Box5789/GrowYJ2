using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] GameObject InteractionPanel;
    [SerializeField] TMP_Text Question_txt;
    [SerializeField] Button Answer1_btn, Answer2_btn;

    public void 이벤트셋팅()
    {
        //이벤트에 함수 달기
    }

    // Start is called before the first frame update
    void Start()
    {
        //오브젝트 연결
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractionEvenView(EventClass Event_occured)
    {
        //View Setting
        InteractionPanel.SetActive(true);
        Question_txt.text = Event_occured.question;
        Answer1_btn.GetComponent<TMP_Text>().text = Event_occured.answer1;
        Answer2_btn.GetComponent<TMP_Text>().text = Event_occured.answer2;

        //on Click Setting
        Answer1_btn.onClick.AddListener(delegate
        {
            GameController.Instance.InteractionResult(Event_occured.result1_knowledge, 
                Event_occured.result1_strength, Event_occured.result1_mental, 
                Event_occured.result1_charm);
        });
        Answer2_btn.onClick.AddListener(delegate
        {
            GameController.Instance.InteractionResult(Event_occured.result2_knowledge,
                Event_occured.result2_strength, Event_occured.result2_mental,
                Event_occured.result2_charm);
        });
    }

    public void OneWayEventView(EventClass Event_occured) 
    { 
        //View Setting - Animation
    }
}
