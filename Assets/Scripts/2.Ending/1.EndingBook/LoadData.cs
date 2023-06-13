using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadData
{
    private string SavePath = Application.persistentDataPath;
    private string SavePositionFileName = "EndingNodePosition.dat";
    BinaryFormatter formatter = new BinaryFormatter();

    public Dictionary<string, EndingPositionData> ReadEndingUIData()
    {
        try
        {
            using (FileStream stream = new FileStream(SavePath + SavePositionFileName, FileMode.Open, FileAccess.Read))
            {
                return (Dictionary<string, EndingPositionData>)formatter.Deserialize(stream);
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
