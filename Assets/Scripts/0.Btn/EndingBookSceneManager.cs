using System.Collections.Generic;
using UnityEngine;

public class EndingBookSceneManager : MonoBehaviour
{
    private void SettingNode(Dictionary<string, EndingPositionData> PositionData)
    {
        /*
        GameObject temp, nodegroup, prefab;
        nodegroup = GameObject.Find("EndingNodeGroup").gameObject;
        prefab = Resources.Load<GameObject>("Prefabs/EndingNode");

        // TODO : 엔딩 버튼 프리팹 복사 & View
        foreach (KeyValuePair<string, EndingPositionData> data in PositionData)
        {
            temp = Instantiate(prefab);
            temp.name = data.Value.GetID() + ", " + data.Value.GetName();
            temp.transform.position = data.Value.GetStartPosition();
            temp.transform.parent = nodegroup.transform;
            //temp.AddComponent<TestEndingNode>().SetData(data);
        }


        Debug.Log("Error : Reading Ending Position Data (or Null. Check the File.)");
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadData load = new LoadData();
        SettingNode(load.ReadEndingUIData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
}
