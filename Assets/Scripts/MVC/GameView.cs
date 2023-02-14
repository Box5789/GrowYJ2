using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    //UI
    GameObject InteractionPanel, IntersectionPanel, IntersectionContent;
    TMP_Text Question_txt;
    Button Answer1_btn, Answer2_btn;


    [Header("게임 조정 변수들")]

    //Timer
    Slider Timer_slide;

    //Status
    RectTransform[] Stat_Mask;
    float StatMaskHeight;
    float[] stat_y;

    //BackGround
    [SerializeField] float speed;
    List<GameObject[]> BackGroundPositionGroup = new List<GameObject[]>();
    float destroyPosX = 0f;
    float spawnPosX = 0f;
    int xScreenHalfSize;
    int road_changed = 0;
    Sprite[] next_road;

    //Event
    Transform SpawnPosition;
    GameObject EventPrefab;
    


    void Start()
    {
        //Prefab
        EventPrefab = Resources.Load<GameObject>("Prefabs/Event");
        GameObject[] background_prefabs = Resources.LoadAll<GameObject>("BackGround");


        //Object
        SpawnPosition = GameObject.Find("EventSpawnPosition").transform;
        Timer_slide = GameObject.Find("Timer").gameObject.GetComponent<Slider>();

        Stat_Mask = new RectTransform[GameManager.Instance.StatCount];
        for (int i=0; i < GameManager.Instance.StatCount; i++)
        {
            Stat_Mask[i] = GameObject.Find((((GameManager.stat_name)i).ToString())).transform.Find("Mask").GetComponent<RectTransform>();
        }

        //BackGround
        xScreenHalfSize = (int)(Camera.main.orthographicSize * Camera.main.aspect) * 2; //Debug.Log(xScreenHalfSize);//10.5

        for (int i=0; i <  GameObject.Find("BackGroundGroup").transform.childCount; i++)
        {
            GameObject[] list = new GameObject[3];
            for(int j=0; j < list.Length; j++)
            {
                list[j] = Instantiate(background_prefabs[i], GameObject.Find("BackGroundGroup").transform.GetChild(i).transform);
                list[j].transform.position = new Vector3(xScreenHalfSize * j, 0, -i);
            }
            BackGroundPositionGroup.Add(list);
        }

        //Value
        stat_y = new float[GameManager.Instance.StatCount];
        StatMaskHeight = GameObject.Find("Knowledge").gameObject.GetComponent<RectTransform>().sizeDelta.y;

        destroyPosX = -xScreenHalfSize;
        spawnPosX = xScreenHalfSize * (BackGroundPositionGroup[0].Length - 1);
    }

    private void FixedUpdate()
    {
        if (!GameController.Instance.TimeStop)
        {
            for(int i = 0; i < BackGroundPositionGroup.Count; i++)
            {
                for(int j = 0; j < BackGroundPositionGroup[i].Length; j++)
                {
                    BackGroundPositionGroup[i][j].transform.position += new Vector3(-speed, 0, 0) * Time.fixedDeltaTime;

                    if (BackGroundPositionGroup[i][j].transform.position.x < destroyPosX)
                    {
                        BackGroundPositionGroup[i][j].transform.position = new Vector3(spawnPosX, 0, -i);

                    }
                    /* 배경 바꾸기
                    if (road_changed > 0 && BackGroundPositionGroup[i][j].transform.position.x <= xScreenHalfSize)
                    {
                        BackGroundPositionGroup[i][j].GetComponent<SpriteRenderer>().sprite = next_road[i];
                        road_changed--;
                    }
                    */
                }
            }
        }
    }

    public void ChangeTime(float time)
    {
        Timer_slide.value = time;
    }

    public void ChangeStatView(GameData data)
    {
        for(int i=0; i < GameManager.Instance.StatCount; i++)
        {
            stat_y[i] = StatMaskHeight * (data.stat[i] / 100f);
            Stat_Mask[i].sizeDelta = new Vector2(Stat_Mask[i].sizeDelta.x, stat_y[i]);
            Stat_Mask[i].anchoredPosition = new Vector3(Stat_Mask[i].anchoredPosition.x, (StatMaskHeight - stat_y[i]) / -2f, 0);
        }
    }


    public void NewEventView(EventClass data)
    {
        //오브젝트 풀링
        var newEventObject = EventObjectPool.getEventObject(data, speed);
        newEventObject.transform.position = SpawnPosition.position;
    }


    public void InteractionEvenView(EventClass Event_occured)
    {
        InteractionPanel = GameObject.Find("Canvas").transform.Find("InteractionPanel").gameObject;
        Question_txt = InteractionPanel.transform.Find("Q_bg").transform.Find("Question_txt").GetComponent<TMP_Text>();
        Answer1_btn = InteractionPanel.transform.Find("Answer1_btn").GetComponent<Button>();
        Answer2_btn = InteractionPanel.transform.Find("Answer2_btn").GetComponent<Button>();


        //View Setting
        InteractionPanel.SetActive(true);
        Question_txt.text = Event_occured.question;
        Answer1_btn.transform.GetComponentInChildren<TMP_Text>().text = Event_occured.answer1;
        Answer2_btn.transform.GetComponentInChildren<TMP_Text>().text = Event_occured.answer2;

        //on Click Setting
        Answer1_btn.onClick.RemoveAllListeners();
        Answer1_btn.onClick.AddListener(delegate
        {
            InteractionPanel.SetActive(false);
            GameController.Instance.InteractionViewResult(Event_occured.result1);
        });

        Answer2_btn.onClick.RemoveAllListeners();
        Answer2_btn.onClick.AddListener(delegate
        {
            InteractionPanel.SetActive(false);
            GameController.Instance.InteractionViewResult(Event_occured.result2);
        });
    }
    public void OneWayEventView(EventClass Event_occured) 
    { 
        //View Setting - Animation
    }


    public void IntersectionEventView(GameData gameData)
    {
        string index;
        Button option_btn;

        //ViewSetting
        IntersectionPanel = GameObject.Find("Canvas").transform.Find("IntersectionPanel").gameObject;
        IntersectionContent = IntersectionPanel.transform.Find("Content").gameObject;
        IntersectionPanel.SetActive(true);

        //Option Button
        for (int i=0; i < 3; i++)//Max Option Button Num = 3;
        {
            option_btn = IntersectionContent.transform.GetChild(i).GetComponent<Button>();

            
            if (i < GameManager.RoadData[gameData.road_ID].NextRoad.Length)
            {
                option_btn.gameObject.SetActive(true);
                index = GameManager.RoadData[gameData.road_ID].NextRoad[i];
                option_btn.onClick.RemoveAllListeners();

                if (GameManager.RoadData[index].CheckRoad(gameData))
                {
                    option_btn.onClick.AddListener(delegate
                    {
                        IntersectionPanel.SetActive(false);

                        if (GameManager.RoadData[index].NextRoad[0].Equals(""))
                            SceneManager.LoadScene("Home");
                        else
                            GameController.IntersectionViewResult(index);
                    });
                    option_btn.GetComponentInChildren<TMP_Text>().text = GameManager.RoadData[index].Name;
                }
                else
                {
                    //버튼 이미지 잠금 코드
                    option_btn.transform.GetComponentInChildren<TMP_Text>().text = GameManager.RoadData[index].Name + " 잠김";
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
        road_changed = BackGroundPositionGroup[0].Length * BackGroundPositionGroup.Count;

        //나중에 이미지 생기면
        //next_road = Resources.LoadAll<Sprite>("BackGround/" + id);
    }
}