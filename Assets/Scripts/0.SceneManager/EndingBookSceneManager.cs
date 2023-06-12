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
        // TODO : Read�� Dictionary ���� �����ϰ� �ҷ�����
        
        string saveFilePath = Application.persistentDataPath + "EndingNodePosition.json";
        Dictionary<string, Vector3> UIPosition;

        if (!File.Exists(saveFilePath))
        {
            Debug.Log("No such 'EndingNodePositionData' exists");
        }
        else
        {
            UIPosition = JsonUtility.FromJson<Dictionary<string, Vector3>>(File.ReadAllText(saveFilePath));

            // TODO : ���� ��ư ������ ���� & View
            foreach(KeyValuePair<string, Vector3> data in UIPosition)
            {

            }
        }
    }


}
