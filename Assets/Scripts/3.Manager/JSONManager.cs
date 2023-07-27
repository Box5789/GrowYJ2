using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JSONManager
{

    public void SaveGameData(GameData gameData)
    {
        string jsonText;
        string path = Application.persistentDataPath + "/Data/GameData.json";
//#if UNITY_ANDROID
//    savePath = Application.persistentDataPath + "/GameData.json";
//#endif
        jsonText = JsonUtility.ToJson(gameData, true);
        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }

    public GameData LoadGameData()
    {
        GameData gameData;

        string path = Application.persistentDataPath + "/Data/GameData.json";
//#if UNITY_ANDROID
//    savePath = Application.persistentDataPath + "/GameData.json";
//#endif
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(Application.dataPath);
        }

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);
            gameData = JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            gameData = new GameData();
        }

        return gameData;
    }
}
