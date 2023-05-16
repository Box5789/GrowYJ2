using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveNode
{
    private string SavePath = Application.persistentDataPath;
    private string SaveFileName = "EndingNodePosition.json";

    public void SaveData(TestEndingData data)
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string saveFilePath = SavePath + SaveFileName;
        File.WriteAllText(saveFilePath, JsonUtility.ToJson(data));
    }

    public TestEndingData LoadData()
    {
        string saveFilePath = SavePath + SaveFileName;

        if (!File.Exists(saveFilePath))
        {
            Debug.Log("No such saveFile exists");
            return null;
        }

        string saveFile = File.ReadAllText(saveFilePath);
        return JsonUtility.FromJson<TestEndingData > (saveFile);
    }
}
