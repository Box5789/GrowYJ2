using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEventData
{
    public string ID;
    public string Name;

    public string[] NextRoad;

    private string knowledge;
    private string strength;
    private string mental;
    private string charm;

    public int point;

    public string road_image;
    public string back_image;

    public RoadEventData(string[] info)
    {
        ID = info[0];
        Name = info[1];
        NextRoad = info[2].Split(',');

        if (!info[3].Equals("")) knowledge = info[3];
        if (!info[4].Equals("")) strength = info[4];
        if (!info[5].Equals("")) mental = info[5];
        if (!info[6].Equals("")) charm = info[6];

        if (!info[7].Equals("")) point = int.Parse(info[7]);

        //�켱 ����
        //road_image = info[8];
        //back_image = info[9];
    }

    //Check Road Condition
    public bool CheckRoad(GameData game)
    {
        bool[] check = new bool[4] { false, false, false, false };
        int[] stat = new int[4] { game.knowledge, game.strength, game.mental, game.charm };
        string[] criterion = new string[4] { knowledge, strength, mental, charm };

        for(int i=0; i < stat.Length; i++)
        {
            if (criterion[i] == null)
                check[i] = true;
            else if (criterion[i].IndexOf("<") == 0)
                if(stat[i] <= int.Parse(criterion[i].Split("<")[0]))
                    check[i] = true;
            else if(criterion[i].IndexOf("<") == criterion[i].Length - 1)
                if (stat[i] <= int.Parse(criterion[i].Split("<")[0]))
                    check[i] = true;
            else
                if(int.Parse(criterion[i].Split("<")[0]) <= stat[i] && stat[i] <= int.Parse(criterion[i].Split("<")[1]))
                    check[i] = true;
        }

        for (int i = 0; i < 4; i++)
            if (!check[i])
                return false;

        return true;
    }
}
