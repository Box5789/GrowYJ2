using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBG : MonoBehaviour
{
    float Speed, posX;
    RectTransform rt;

    public void Init(float s)
    {
        Speed = s;

        rt = GetComponent<RectTransform>();
        posX = (Camera.main.orthographicSize * Camera.main.aspect) * 2;

        ChangeBG();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.TimeStop)
        {
            if (rt.position.x < -posX)
            {
                rt.position = new Vector3(posX * 2, rt.position.y, rt.position.z);
            }
            rt.Translate(new Vector3(-Speed * Time.deltaTime, 0, 0));
        }
    }

    public void ChangeBG()
    {
        GetComponent<Image>().sprite = GameManager.Instance.Resource.BG[GameManager.Instance.Data.roadData[GameManager.Instance.gameData.road_id].bg];
    }
}
