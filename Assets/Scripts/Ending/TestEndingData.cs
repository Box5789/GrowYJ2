using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEndingData
{
    string ID;
    string Name;
    Vector3 Position;
    TestEndingData Parent = null;
    List<TestEndingData> Childrens = new List<TestEndingData>();


    public void SetID(string id) { ID = id; }
    public void SetName(string name) {  Name = name; }
    public void SetPosition(Vector3 position) {  Position = position; }
    public void SetParent(TestEndingData parent) { Parent = parent; }
    public void AddChild(TestEndingData child) { Childrens.Add(child); }
    public void DeleteChild(TestEndingData child) { Childrens.Remove(child); }


    public string GetID() { return ID; }
    public string GetName() { return Name; }
    public Vector3 GetPosition() { return Position; }
    public TestEndingData GetParent() { return Parent; }
    public List<TestEndingData> GetChildrens() { return Childrens; }


    public void SetData(string id, string name, Vector3 position, TestEndingData parent)
    {
        ID = id;
        Name = name;
        Position = position;
        Parent = parent;
    }
}