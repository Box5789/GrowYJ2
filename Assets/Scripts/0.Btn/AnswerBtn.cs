using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerBtn : MonoBehaviour
{
     int type;

    TMP_Text text;
    [SerializeField] GameObject Panel;
    EventClass data;

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void DataSet(int t, string key)
    {
        type = t;
        data = GameManager.Instance.Data.objectData[key];

        if (type == 1)
        {
            transform.GetChild(0).GetComponent<TMP_Text>().text = data.answer1;
        }
        else
        {
            transform.GetChild(0).GetComponent<TMP_Text>().text = data.answer2;
        }
    }

    public void OnClick()
    {
        Panel.SetActive(false);

        if (type == 1)
        {
            GameController.Instance.InteractionViewResult(data.result1);
        }
        else
        {
            GameController.Instance.InteractionViewResult(data.result2);
        }

        GameController.Instance.Objects[data.EventID].GetComponent<EventObject>().Over();
    }
}
