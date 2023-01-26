using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    EventClass Data;
    bool click = true, contact = true;

    public void SetEventClass(EventClass d)
    {
        Data = d;
    }

    // Start is called before the first frame update
    void Start()
    {
        //강제 - 클릭&&접촉 : 기본

        if (Data.select_type.Equals("선택"))//선택 - Only 클릭
        {
            contact = false;
        }
        else if (Data.select_type.Equals("회피"))//회피 - Only 접촉
        {
            click = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (click)
        {
            EventOn();

            Destroy(gameObject);//Pool로 이동(추후)
        }

        if (!click)//회피 : 클릭시 Destroy : Pool로 이동(추후)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (contact)
        {
            EventOn();

            Destroy(gameObject);//Pool로 이동(추후)
        }
    }

    void EventOn()
    {
        if (Data.conversation_type)
        {
            GameController.Instance.InteractinoEventOn(Data);
        }
        else
        {
            GameController.Instance.OneWayEventOn(Data);
        }
    }
}
