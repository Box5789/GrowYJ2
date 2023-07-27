using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ResourceManager
{
    public Dictionary<string, Sprite> BG { get; set; }
    public Dictionary<string, Sprite> Ending { get; set; }
    public Dictionary<string, GameObject> Objects { get; set; }

    StringBuilder name;

    public void Init()
    {
        name = new StringBuilder();
        BGSetting();
        EndingSetting();
        ObjectSetting();
    }
    void BGSetting()
    {
        name.Clear();
        BG = new Dictionary<string, Sprite>();

        for(int i=0; i < (int)Name.BG.Count; i++)
        {
            name.Append(Enum.GetName(typeof(Name.BG), i));
            BG.Add(name.ToString(), Resources.Load<Sprite>("Sprite/BackGround/" + name.ToString()));
            name.Clear();
        }
    }

    void EndingSetting()
    {
        name.Clear();
        Ending = new Dictionary<string, Sprite>();

        for (int i = 0; i < (int)Name.Ending.Count; i++)
        {
            name.Append(Enum.GetName(typeof(Name.Ending), i));
            Ending.Add(name.ToString(), Resources.Load<Sprite>("Sprite/Ending/" + name.ToString()));
            name.Clear();
        }
    }

    void ObjectSetting()
    {
        name.Clear();
        Objects = new Dictionary<string, GameObject>();

        for (int i = 0; i < (int)Name.Object.Count; i++)
        {
            name.Append(Enum.GetName(typeof(Name.Object), i));
            Objects.Add(name.ToString(), Resources.Load<GameObject>("Prefabs/Object/" + name.ToString()));
            name.Clear();
        }
    }
}