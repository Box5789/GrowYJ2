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

        //우선 공백
        //road_image = info[8];
        //back_image = info[9];
    }

    //조건 확인 함수
    public bool CheckRoad()
    {
        bool result = false;

        //조건 확인

        return result;
    }
}
