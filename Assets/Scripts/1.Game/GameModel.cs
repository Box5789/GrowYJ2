using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    private List<EventClass> EventPiece = new List<EventClass>();
    private int EventWeight = 0;

    public void ChangeStat(int[] stat_data)
    {

        for(int i=0; i < (int)Name.Stat.Count; i++)
        {
            GameManager.Instance.gameData.stat[i] += stat_data[i];
        }

        DebugTest.Instance.PrintGameStat(GameManager.Instance.gameData);

        GameController.Instance.EventOff(GameManager.Instance.gameData);
    }

    public void ChangeRoad(string id) 
    {
        GameManager.Instance.gameData.road_id = id;
    }

    public void EventSet()
    {
        EventPiece.Clear();
        EventWeight = 0;

        foreach (KeyValuePair<string, EventClass> obj in GameManager.Instance.Data.objectData)
        {
            if (obj.Value.roadID.Equals(GameManager.Instance.gameData.road_id))
            {
                EventPiece.Add(obj.Value);
                EventWeight += obj.Value.weight;
            }
        }
    }

    public string NewEventNum()
    {
        int rand = Random.Range(0, EventWeight);

        for(int i=0, result = 0; i < EventPiece.Count; i++)
        {
            result += EventPiece[i].weight;
            if (result >= rand)
                return EventPiece[i].EventID;
        }

        return EventPiece[Random.Range(0, EventPiece.Count) - 1].EventID;
    }
}
