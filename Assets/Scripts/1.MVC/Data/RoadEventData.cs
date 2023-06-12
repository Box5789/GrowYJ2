using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEventData
{
    public string ID;
    public string Name;

    public string[] NextRoad;

    private string[] stat_conditions = new string[GameManager.Instance.StatCount];

    public int point;

    public string road_image;
    public string back_image;

    public RoadEventData(string[] info)
    {
        int i = 0; 

        ID = info[i++];
        Name = info[i++];
        NextRoad = info[i++].Replace(" ","").Split(',');

        for(int j=0; j < stat_conditions.Length; j++)
        {
            if (!info[i].Replace(" ", "").Equals(""))
                stat_conditions[j] = info[i++];
            else
                i++;
        }

        if (!info[i].Equals("")) point = int.Parse(info[i++]);

        //road_image = info[8];
        //back_image = info[9];
    }

    //Check Road Condition
    public bool CheckRoad(GameData game)
    {
        bool[] check = new bool[4] { false, false, false, false };

        for (int i=0; i < stat_conditions.Length; i++)
        {
            if (stat_conditions[i] == null)//No Condition
                check[i] = true;
            else if (stat_conditions[i].IndexOf("<") == 0)//More than
            {
                if (int.Parse(stat_conditions[i].Substring(1, stat_conditions[i].Length - 1)) <= game.stat[i])
                    check[i] = true;
            }
            else if (stat_conditions[i].IndexOf("<") == stat_conditions[i].Length - 1)//Less than
            {
                if (game.stat[i] <= int.Parse(stat_conditions[i].Split("<")[0]))
                    check[i] = true;
            }
            else //More & Less
            {
                if (int.Parse(stat_conditions[i].Split("<")[0]) <= game.stat[i] && game.stat[i] <= int.Parse(stat_conditions[i].Split("<")[1]))
                    check[i] = true;
            }
        }

        for (int i = 0; i < 4; i++)
            if (!check[i])
                return false;

        return true;
    }
}
