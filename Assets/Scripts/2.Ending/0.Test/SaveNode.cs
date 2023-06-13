using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveNode
{
    private string SavePath = Application.persistentDataPath;
    private string SaveTestFileName = "EndingTestNodePosition.dat";
    private string SavePositionFileName = "EndingNodePosition.dat";

    BinaryFormatter formatter = new BinaryFormatter();

    public void SaveData(TestEndingData testData)
    {
        using (FileStream stream = new FileStream(SavePath + SaveTestFileName, FileMode.Create, FileAccess.Write))
        {
            formatter.Serialize(stream, testData);
        }

        using (FileStream stream = new FileStream(SavePath + SavePositionFileName, FileMode.Create, FileAccess.Write))
        {
            formatter.Serialize(stream, SetPositionData(new Dictionary<string, EndingPositionData>(), testData));
        }
    }

    //CODE REVIEW : modification required ( return data, parameter )
    Dictionary<string, EndingPositionData> SetPositionData(Dictionary<string, EndingPositionData> dic, TestEndingData data)
    {
        if (data.GetID().Equals("n1"))
        {
            dic.Add(data.GetID(), new EndingPositionData(data.GetID(), data.GetName(), data.GetPosition(), data.GetPosition()));
        }
        else
            dic.Add(data.GetID(), new EndingPositionData(data.GetID(), data.GetName(), data.GetPosition(), data.GetParentData().GetPosition()));

        if(data.GetChildrensData() != null && data.GetChildrensData().Count != 0)
            foreach (TestEndingData child in data.GetChildrensData())
            {
                dic = SetPositionData(dic, child);
            }

        return dic;
    }

    public TestEndingData LoadData()
    {
        try
        {
            using (FileStream stream = new FileStream(SavePath + SaveTestFileName, FileMode.Open, FileAccess.Read))
            {
                return (TestEndingData)formatter.Deserialize(stream);
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
