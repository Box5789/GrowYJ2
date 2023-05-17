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
    // ↓ Test : 테스트 확인 용
    GameObject Parent_Obj = null;
    List<GameObject> Childrens_Obj = new List<GameObject>();


    public void SetID(string id) { ID = id; }
    public void SetName(string name) {  Name = name; }
    public void SetPosition(Vector3 position) {  Position = position; }
    public void SetParent(GameObject parent) 
    { 
        Parent_Obj = parent; 
        Parent = Parent_Obj.GetComponent<TestEndingNode>().data; 
    }
    public void AddChild(GameObject child) 
    { 
        Childrens_Obj.Add(child);
        Childrens.Add(child.GetComponent<TestEndingNode>().data);
    }
    public void DeleteChild(GameObject child) 
    { 
        Childrens_Obj.Remove(child);
        Childrens.Remove(child.GetComponent<TestEndingNode>().data);
    }

    public string GetID() { return ID; }
    public string GetName() { return Name; }
    public Vector3 GetPosition() { return Position; }
    public TestEndingData GetParent() { return Parent; }
    public GameObject GetParentGameObject() { return Parent_Obj; }
    public List<TestEndingData> GetChildrens() { return Childrens; }
    public List<GameObject> GetChildrensObject() { return Childrens_Obj; }


    public void SetData(string id, string name, Vector3 position, TestEndingData parent)
    {
        ID = id;
        Name = name;
        Position = position;
        Parent = parent;
    }
}