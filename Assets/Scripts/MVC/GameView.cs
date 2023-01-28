using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] GameObject InteractionPanel, IntersectionPanel, IntersectionContent;
    [SerializeField] TMP_Text Question_txt, Answer1_txt, Answer2_txt;
    [SerializeField] Button Answer1_btn, Answer2_btn, Option_btn;

    GameObject[] BackGrounds = new GameObject[4];
    GameObject[] Roads = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        Option_btn = Resources.Load<Button>("/Prefabs/Option_btn");
        //���̶� ��� ������ �����ͼ� �����ϱ�.
        //������Ʈ ����
    }

    private void FixedUpdate()
    {
        if (!GameController.Instance.TimeStop)
        {
            //����̶� �� ������ ���⼭ ����
        }
    }

    public void InteractionEvenView(EventClass Event_occured)
    {
        //View Setting
        InteractionPanel.SetActive(true);
        Question_txt.text = Event_occured.question;
        Answer1_txt.text = Event_occured.answer1;
        Answer2_txt.text = Event_occured.answer2;

        //on Click Setting
        Answer1_btn.onClick.AddListener(delegate
        {
            InteractionPanel.SetActive(false);
            GameController.Instance.InteractionViewResult(Event_occured.result1_knowledge, 
                Event_occured.result1_strength, Event_occured.result1_mental, 
                Event_occured.result1_charm);
            Answer1_btn.onClick.RemoveAllListeners();
        });
        Answer2_btn.onClick.AddListener(delegate
        {
            InteractionPanel.SetActive(false);
            GameController.Instance.InteractionViewResult(Event_occured.result2_knowledge,
                Event_occured.result2_strength, Event_occured.result2_mental,
                Event_occured.result2_charm);
            Answer2_btn.onClick.RemoveAllListeners();
        });
    }
    public void OneWayEventView(EventClass Event_occured) 
    { 
        //View Setting - Animation
    }


    public void IntersectionEventView(GameData gameData)
    {
        string index;

        //ViewSetting
        IntersectionPanel.SetActive(true);
        //Option Button
        for (int i=0; i < GameManager.RoadData[gameData.road_ID].NextRoad.Length; i++)
        {
            index = GameManager.RoadData[gameData.road_ID].NextRoad[i];
            Option_btn.onClick.RemoveAllListeners();

            if (GameManager.RoadData[index].CheckRoad(gameData))
            {
                Option_btn.onClick.AddListener(delegate
                {
                    GameController.IntersectionViewResult(index);
                });
            }
            else
            {
                //��ư �̹��� ��� �ڵ� or ��ư Ŭ�� ���ϰ�
            }
            Option_btn.GetComponentInChildren<TMP_Text>().text = GameManager.RoadData[index].Name;
            Instantiate(Option_btn, IntersectionContent.transform);
        }

    }
    public void ChangeRoadView(string id)
    {
        //���̶� �̹��� ��� ���� �ڵ�

    }
}
