using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public string road_ID;

    private int _knowledge;
    public int knowledge
    {
        get
        {
            return _knowledge;
        }
        set 
        {
            _knowledge = value;
        } 
    }
    private int _strength;
    public int strength
    {
        get
        {
            return _strength;
        }
        set
        {
            _strength = value;
        }
    }
    private int _mental;
    public int mental
    {
        get
        {
            return _mental;
        }
        set
        {
            _mental = value;
        }
    }
    private int _charm;
    public int charm
    {
        get
        {
            return _charm;
        }
        set
        {
            _charm = value;
        }
    }

    public void init()
    {
        road_ID = "n1";
        _knowledge = 50;
        _strength = 50;
        _mental = 50;
        _charm = 50;
    }
}
