using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    //UI
    [SerializeField] GameObject InteractionPanel, IntersectionPanel, IntersectionContent;
    [SerializeField] TMP_Text Question_txt;
    [SerializeField] Button Answer1_btn, Answer2_btn;


    [Header("게임 조정 변수들")]

    //Timer
    Slider Timer_slide;

    //Status
    RectTransform[] Stat_Mask;
    float StatMaskHeight;
    float[] stat_y;

    //BackGround
    [SerializeField] float speed;
    [SerializeField] float Objspeed;
    Image[] BG;
    int road_changed = 0;
    Sprite[] next_road;

    //Event
    Transform SpawnPosition;
    GameObject EventPrefab;
    


    void Start()
    {
        //Prefab
        GameObject[] background_prefabs = Resources.LoadAll<GameObject>("BackGround");


        //Object
        SpawnPosition = GameObject.Find("SpawnPosition").transform;
        Timer_slide = GameObject.Find("Timer").gameObject.GetComponent<Slider>();

        Stat_Mask = new RectTransform[(int)Name.Stat.Count];
        for (int i=0; i < (int)Name.Stat.Count; i++)
        {
            Stat_Mask[i] = GameObject.Find((Enum.GetName(typeof(Name.Stat), i)).ToString()).transform.Find("Mask").GetComponent<RectTransform>();
        }

        //BackGround
        BG = GameObject.Find("BG").transform.GetComponentsInChildren<Image>();
        for(int i=0; i < BG.Length; i++)
        {
            BG[i].gameObject.AddComponent<MoveBG>().Init(speed);
            //BG[i].sprite = GameManager.Instance.Resource.BG[GameManager.Instance.gameData.road_id];
        }


        //Value
        stat_y = new float[(int)Name.Stat.Count];
        StatMaskHeight = GameObject.Find("Knowledge").gameObject.GetComponent<RectTransform>().sizeDelta.y;
    }


    public void ChangeTime(float time)
    {
        Timer_slide.value = time;
    }

    public void ChangeStatView(GameData data)
    {
        for(int i=0; i < (int)Name.Stat.Count; i++)
        {
            stat_y[i] = StatMaskHeight * (data.stat[i] / 100f);
            Stat_Mask[i].sizeDelta = new Vector2(Stat_Mask[i].sizeDelta.x, stat_y[i]);
            Stat_Mask[i].anchoredPosition = new Vector3(Stat_Mask[i].anchoredPosition.x, (StatMaskHeight - stat_y[i]) / -2f, 0);
        }
    }


    public void NewEventView(string key)//이벤트 생성
    {
        EventClass obj = GameManager.Instance.Data.objectData[key];

        if (GameController.Instance.Objects.ContainsKey(key))
        {
            GameController.Instance.Objects[key].GetComponent<EventObject>().Reuse();
        }
        else
        {
            EventPrefab = Instantiate(GameManager.Instance.Resource.Objects[obj.image]);
            EventPrefab.transform.parent = SpawnPosition;
            EventPrefab.GetComponent<EventObject>().SetEventClass(SpawnPosition.position.x, Objspeed, GameManager.Instance.Data.objectData[key]);
            GameController.Instance.Objects.Add(key, EventPrefab);
            GameController.Instance.ConectionObjectKey = key;
        }
        
    }


    public void InteractionEvenView(EventClass Event_occured)
    {
        //View Setting
        InteractionPanel.SetActive(true);
        Answer1_btn.GetComponent<AnswerBtn>().DataSet(1, Event_occured.EventID);
        Answer2_btn.GetComponent<AnswerBtn>().DataSet(2, Event_occured.EventID);
        Question_txt.text = Event_occured.question;
    }
    public void OneWayEventView(EventClass Event_occured) 
    { 
        //View Setting - Animation
    }


    public void IntersectionEventView(GameData gameData)
    {
        //길변경

        string index;
        Button option_btn;

        //ViewSetting
        IntersectionPanel.SetActive(true);

        //Option Button
        for (int i=0; i < 3; i++)//Max Option Button Num = 3;
        {
            option_btn = IntersectionContent.transform.GetChild(i).GetComponent<Button>();

            
            if (i < GameManager.Instance.Data.roadData[gameData.road_id].NextRoad.Length)
            {
                option_btn.gameObject.SetActive(true);
                index = GameManager.Instance.Data.roadData[gameData.road_id].NextRoad[i];
                option_btn.onClick.RemoveAllListeners();

                if (GameManager.Instance.Data.roadData[index].CheckRoad(gameData))
                {
                    option_btn.onClick.AddListener(delegate
                    {
                        IntersectionPanel.SetActive(false);

                        if (GameManager.Instance.Data.roadData[index].NextRoad[0].Equals(""))
                        {
                            
                            SceneManager.LoadScene("Home");
                        }
                        else
                            GameController.Instance.IntersectionViewResult(index);
                    });
                    option_btn.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.Data.roadData[index].RoadName;
                }
                else
                {
                    //버튼 이미지 잠금 코드
                    option_btn.transform.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.Data.roadData[index].RoadName + " 잠김";
                }
            }
            else//Button Hide
            {
                option_btn.gameObject.SetActive(false);
            }
        }
    }
    public void ChangeRoadView(string id)
    {
        for(int i=0; i < BG.Length; i++)
        {
            BG[i].GetComponent<MoveBG>().ChangeBG();
        }
        //road_changed = BG.Length * BackGroundPositionGroup.Count;

        //나중에 이미지 생기면
        //next_road = Resources.LoadAll<Sprite>("BackGround/" + id);
    }
}