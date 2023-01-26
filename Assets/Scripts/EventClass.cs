using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class EventClass
{
    public string EventID;
    public string roadID;

    public string select_type;
    public bool conversation_type;

    public string image;

    public string question;

    public string answer1;
    public string answer2;

    public int result1_knowledge;
    public int result1_strength;
    public int result1_mental;
    public int result1_charm;

    public int result2_knowledge;
    public int result2_strength;
    public int result2_mental;
    public int result2_charm;

    public EventClass(string[] info)
    {
        EventID = info[0];
        roadID = info[1];

        select_type = info[2];
        conversation_type = Convert.ToBoolean(info[3]);

        image = info[4];

        question = info[5];

        result1_knowledge = int.Parse(info[8]);
        result1_strength = int.Parse(info[9]);
        result1_mental= int.Parse(info[10]);
        result1_charm = int.Parse(info[11]);

        if (conversation_type)
        {
            answer1 = info[6];
            answer2 = info[7];

            result2_knowledge = int.Parse(info[12]);
            result2_strength = int.Parse(info[13]);
            result2_mental = int.Parse(info[14]);
            result2_charm = int.Parse(info[15]);
        }
    }
}
