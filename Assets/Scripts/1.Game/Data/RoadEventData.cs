
public class RoadEventData
{
    public string ID;
    public string RoadName;

    public string[] NextRoad;

    public string[] stat_conditions = new string[(int)Name.Stat.Count];

    public int point;

    public string bg;

    public RoadEventData(string[] info)
    {
        int i = 0; 

        ID = info[i++];
        RoadName = info[i++];
        NextRoad = info[i++].Replace(" ","").Split(',');

        for(int j=0; j < stat_conditions.Length; j++)
        {
            if (!info[i].Replace(" ", "").Equals(""))
                stat_conditions[j] = info[i++];
            else
                i++;
        }

        if (!info[i].Equals("")) point = int.Parse(info[i++]);

        bg = info[8];
    }

    //Check Road Condition
    public bool CheckRoad(GameData game)
    {
        bool[] check = new bool[4] { false, false, false, false };

        for (int i=0; i < stat_conditions.Length; i++)
        {
            if (stat_conditions[i] == null)//No Condition
                check[i] = true;
            else if (stat_conditions[i].IndexOf("<") == 0)//More than
            {
                if (int.Parse(stat_conditions[i].Substring(1, stat_conditions[i].Length - 1)) <= game.stat[i])
                    check[i] = true;
            }
            else if (stat_conditions[i].IndexOf("<") == stat_conditions[i].Length - 1)//Less than
            {
                if (game.stat[i] <= int.Parse(stat_conditions[i].Split("<")[0]))
                    check[i] = true;
            }
            else //More & Less
            {
                if (int.Parse(stat_conditions[i].Split("<")[0]) <= game.stat[i] && game.stat[i] <= int.Parse(stat_conditions[i].Split("<")[1]))
                    check[i] = true;
            }
        }

        for (int i = 0; i < 4; i++)
            if (!check[i])
                return false;

        return true;
    }
}
