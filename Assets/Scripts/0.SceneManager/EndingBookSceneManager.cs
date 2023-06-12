using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EndingBookSceneManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        ReadEndingUIData(Resources.Load<GameObject>("Prefabs/EndingNode"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadEndingUIData(GameObject prefab)
    {
        // TODO : Read용 Dictionary 파일 저장하고 불러오기
        
        string saveFilePath = Application.persistentDataPath + "EndingNodePosition.json";
        Dictionary<string, Vector3> UIPosition;

        if (!File.Exists(saveFilePath))
        {
            Debug.Log("No such 'EndingNodePositionData' exists");
        }
        else
        {
            UIPosition = JsonUtility.FromJson<Dictionary<string, Vector3>>(File.ReadAllText(saveFilePath));

            // TODO : 엔딩 버튼 프리팹 복사 & View
            foreach(KeyValuePair<string, Vector3> data in UIPosition)
            {

            }
        }
    }


}
