using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    int 지식;
    int 체력;
    int 정신력;
    int 인기도;

    float 시간;

    void init()
    {
        지식 = 50;
        체력 = 50;
        정신력 = 50;
        인기도 = 50;

        시간 = 0;
    }
}
