using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameModel
{
    private GameData _gameData;
    public GameData gameData 
    { 
        get { return _gameData; }
    }

    private List<EventClass> EventPiece = new List<EventClass>();
    private int EventWeight = 0;


    public GameModel()
    {
        _gameData = new GameData();
        _gameData.init();

        _gameData.road_ID = DebugTest.Instance.GetRoadData();
        DebugTest.Instance.PrintGameStat(gameData);
    }

    public void InitDataSet()
    {
        //저장된 게임데이터 읽어오기 -> 뷰 이벤트 실행
    }

    public void ChangeStat(int[] stat_data)
    {

        for(int i=0; i < GameManager.Instance.StatCount; i++)
        {
            gameData.stat[i] += stat_data[i];
        }

        DebugTest.Instance.PrintGameStat(gameData);

        GameController.Instance.EventOff(gameData);
    }

    public void ChangeRoad(string id) 
    { 
        gameData.road_ID = id;
    }

    public void EventSet()
    {
        EventPiece.Clear();
        EventWeight = 0;

        for (int i = 0; i < GameManager.EventData.Count; i++)
        {
            if (GameManager.EventData[i].roadID[0].Equals(""))
            {
                EventPiece.Add(GameManager.EventData[i]);
                EventWeight += GameManager.EventData[i].weight;
            }
            else
            {
                for (int j = 0; j < GameManager.EventData[i].roadID.Length; j++)
                {
                    if (GameManager.EventData[i].roadID[j].Equals(gameData.road_ID))
                    {
                        EventPiece.Add(GameManager.EventData[i]);
                        EventWeight += GameManager.EventData[i].weight;
                        break;
                    }
                }
            }
        }

        DebugTest.Instance.SetEventPiece(EventPiece);
    }

    public EventClass NewEventNum()
    {
        int rand = Random.Range(0, EventWeight);

        Debug.Log(rand);

        for(int i=0, result = 0; i < EventPiece.Count; i++)
        {
            result += EventPiece[i].weight;
            if (result >= rand)
                return EventPiece[i];
        }

        return EventPiece[Random.Range(0, EventPiece.Count) - 1];
    }


}
