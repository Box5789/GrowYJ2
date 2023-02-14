using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public string road_ID;

    public int[] stat = new int[GameManager.Instance.StatCount];

    public void init()
    {
        road_ID = "n1";
        
        for(int i=0; i < stat.Length; i++)
        {
            stat[i] = 50;
        }
    }
}
