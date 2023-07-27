using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    public Dictionary<string, EventClass> objectData { get; set; }
    public Dictionary<string, RoadEventData> roadData { get; set; }


    public void Init()
    {
        ObjectSet();
        RoadSet();
    }

    void ObjectSet()
    {
        objectData = new Dictionary<string, EventClass>();
        EventClass temp;

        string event_filepath = "/Resources/Data/Event.csv";

        StreamReader sr = new StreamReader(Application.dataPath + event_filepath);

        bool first = true;

        while (true)
        {
            string data_String = sr.ReadLine();

            if (data_String == null)
            {
                break;
            }
            else if (first)
            {
                first = false;
            }
            else
            {
                temp = new EventClass(data_String.Split('/'));
                objectData.Add(temp.EventID, temp);
            }
        }
    }

    void RoadSet()
    {
        roadData = new Dictionary<string, RoadEventData>();

        string road_filepath = "/Resources/Data/Road.csv";

        StreamReader sr = new StreamReader(Application.dataPath + road_filepath);

        bool first = true;

        while (true)
        {
            string data_String = sr.ReadLine();

            if (data_String == null)
            {
                break;
            }
            else if (first)
            {
                first = false;
            }
            else
            {
                var data_values = data_String.Split('/');

                roadData.Add(data_values[0], new RoadEventData(data_values));
            }
        }
    }
}
