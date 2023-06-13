using UnityEngine;
using System;

[Serializable]
public class EndingPositionData
{
    string ID;
    string Name;
    float start_x, start_y, start_z, end_x, end_y, end_z;//start : this, end : parent


    public void SetID(string id) { ID = id; }
    public void SetName(string name) { Name = name; }
    public void SetStartPosition(Vector3 position)
    {
        start_x = position.x;
        start_y = position.y;
        start_z = position.z;
    }
    public void SetEndPosition(Vector3 position)
    {
        end_x = position.x;
        end_y = position.y;
        end_z = position.z;
    }


    public string GetID() { return ID; }
    public string GetName() { return Name; }
    public Vector3 GetStartPosition() { return new Vector3(start_x, start_y, start_z); }
    public Vector3 GetEndPosition() { return new Vector3(end_x, end_y, end_z); }


    public EndingPositionData(string id, string name, Vector3 position0, Vector3 position1)
    {
        ID = id;
        Name = name;
        start_x = position0.x;
        start_y = position0.y;
        start_z = position0.z;
        end_x = position1.x;
        end_y = position1.y;
        end_z = position1.z;
    }
}
