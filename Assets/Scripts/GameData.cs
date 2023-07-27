using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public string road_id;
    public int point;

    public int[] stat = new int[(int)Name.Stat.Count];

    public void init()
    {
        road_id = "n1";
        point = 0;

        for (int i=0; i < stat.Length; i++)
        {
            stat[i] = 50;
        }
    }
}
