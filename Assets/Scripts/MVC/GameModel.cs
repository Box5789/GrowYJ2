using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameModel
{
    GameData gameData;

    public GameModel()
    {
        gameData = new GameData();
        gameData.init();
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
}
