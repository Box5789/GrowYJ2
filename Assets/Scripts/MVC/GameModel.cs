using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameModel
{
    private GameData _gameData;
    public GameData gameData { get { return _gameData; } }

    private List<EventClass> EventPiece = new List<EventClass>();


    public GameModel()
    {
        _gameData = new GameData();
        _gameData.init();
    }

    public void InitDataSet()
    {
        //저장된 게임데이터 읽어오기 -> 뷰 이벤트 실행
    }

    public void ChangeStat(int k, int s, int m, int c)
    {
        gameData.knowledge += k;
        gameData.strength += s;
        gameData.mental += m;
        gameData.charm += c;

        Debug.Log("[ 스탯조정 ]\n" + gameData.knowledge.ToString() + " / " + gameData.strength.ToString() + " / " + gameData.mental.ToString() + " / " + gameData.charm.ToString());

        GameController.Instance.EventOff();
    }

    public void ChangeRoad(string id) 
    { 
        gameData.road_ID = id;
    }

    public void EventSet()
    {
        Debug.Log("Road : " + gameData.road_ID + " / " + GameManager.RoadData[gameData.road_ID].Name);
        EventPiece.Clear();

        for (int i = 0; i < GameManager.EventData.Count; i++)
        {
            if (GameManager.EventData[i].roadID[0].Equals(""))
            {
                Debug.Log("Event Add : " + GameManager.EventData[i].EventID);
                EventPiece.Add(GameManager.EventData[i]);
            }
            else
            {
                for (int j = 0; j < GameManager.EventData[i].roadID.Length; j++)
                {
                    if (GameManager.EventData[i].roadID[j].Equals(gameData.road_ID))
                    {
                        Debug.Log("Event Add : " + GameManager.EventData[i].EventID);
                        EventPiece.Add(GameManager.EventData[i]);
                        break;
                    }
                }
            }
        }
    }

    public EventClass NewEventNum()
    {
        return EventPiece[Random.Range(0, EventPiece.Count)];
    }
}
