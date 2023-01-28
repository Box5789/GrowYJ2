using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    public void NewEventView()
    {
        Instantiate(EventPrefab, SpawnPosition.position, SpawnPosition.rotation).GetComponent<EventObject>().SetEventClass(GameController.Instance.NextEvent, speed);//오브젝트 풀링으로 관리
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
        Button option_btn;

        //ViewSetting
        IntersectionPanel = GameObject.Find("Canvas").transform.Find("IntersectionPanel").gameObject;
        IntersectionContent = IntersectionPanel.transform.Find("Content").gameObject;
        IntersectionPanel.SetActive(true);

        //Option Button
        for (int i=0; i < 3; i++)
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
                        Debug.Log(GameManager.RoadData[index].NextRoad[0]);
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
                    option_btn.transform.GetComponentInChildren<TMP_Text>().text += " 잠김";
                }

            }
            else
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