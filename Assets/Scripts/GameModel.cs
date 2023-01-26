using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameModel
{
    GameData gameData;

    public GameModel()
    {

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
    }
}
