using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int knowledge 
    {
        get
        {
            return knowledge;
        }
        set 
        {
            knowledge = value;
        } 
    }
    public int strength
    {
        get
        {
            return strength;
        }
        set
        {
            strength = value;
        }
    }
    public int mental
    {
        get
        {
            return mental;
        }
        set
        {
            mental = value;
        }
    }
    public int charm
    {
        get
        {
            return charm;
        }
        set
        {
            charm = value;
        }
    }

    void init()
    {
        knowledge = 50;
        strength = 50;
        mental = 50;
        charm = 50;
    }
}
