using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class EventClass
{
    public string EventID;
    public string[] roadID;

    public int weight;

    public string select_type;
    public bool conversation_type;

    public string image;

    public string question;

    public string answer1;
    public string answer2;

    public int[] result1 = new int[GameManager.Instance.StatCount];
    public int[] result2 = new int[GameManager.Instance.StatCount];

    public EventClass(string[] info)
    {
        int i = 0;

        EventID = info[i++];
        roadID = info[i++].Replace(" ","").Split(",");

        weight = int.Parse(info[i++]);

        select_type = info[i++];
        conversation_type = Convert.ToBoolean(info[i++]);

        image = info[i++];

        question = info[i++];


        if (conversation_type)
        {
            answer1 = info[i++];
            answer2 = info[i++];
        }
        else i += 2;

        for(int j=0; j < result1.Length; j++)
        {
            result1[j] = int.Parse(info[i++]);
        }

        if (conversation_type)
        {
            for(int j=0; j < result2.Length; j++) 
            {
                result2[j] = int.Parse(info[i++]);
            }
        }
    }
}
