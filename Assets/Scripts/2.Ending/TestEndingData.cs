using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class TestEndingData
{
    string ID;
    string Name;
    [SerializeField]
    float x, y, z;

    TestEndingData ParentData = null;
    List<TestEndingData> ChildrensData = new List<TestEndingData>();

    public void SetID(string id) { ID = id; }
    public void SetName(string name) {  Name = name; }
    public void SetPosition(Vector3 position) 
    {
        x = position.x;
        y = position.y;
        z = position.z;
    }
    public void SetParent(TestEndingData data) 
    { 
        ParentData = data;
    }
    public void AddChild(TestEndingData data) 
    { 
        ChildrensData.Add(data);
    }
    public void RemoveChild(TestEndingData data) 
    { 
        ChildrensData.Remove(data);
    }


    public string GetID() { return ID; }
    public string GetName() { return Name; }
    public Vector3 GetPosition() { return new Vector3(x, y, z); }
    public TestEndingData GetParentData() { return ParentData; }
    public List<TestEndingData> GetChildrensData() { return ChildrensData; }


    public void SetData(string id, string name, Vector3 position)
    {
        ID = id;
        Name = name;
        x = position.x;
        y = position.y;
        z = position.z;
    }

}