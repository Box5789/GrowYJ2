using System.Collections.Generic;

public class InteractionEventData
{
    List<string> roads = new List<string>();
    string type;
    string image;
    string question;
    string answer1;
    string answer2;
    int[] result1 = new int[4];
    int[] result2 = new int[4];

    public InteractionEventData(string[] data)
    {

    }
}
