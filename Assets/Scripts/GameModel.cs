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
        //����� ���ӵ����� �о���� -> �� �̺�Ʈ ����
    }

    public void ChangeStat(int k, int s, int m, int c)
    {
        gameData.knowledge += k;
        gameData.strength += s;
        gameData.mental += m;
        gameData.charm += c;
    }
}
