using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventObject : MonoBehaviour
{
    public bool active;
    float speed;
    EventClass Data;
    bool click = true, contact = true;
    Vector3 start;

    public void SetEventClass(float x, float speed, EventClass d)
    {
        gameObject.SetActive(true);
        active = true;
        this.speed = speed;
        Data = d;
        start = new Vector3(x, transform.position.y, transform.position.z);

        transform.position = start;

        if (d != null)
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
        else
        {
            click = true;
            contact = true;
        }
    }
    public void Reuse()
    {
        gameObject.SetActive(true);
        active = true;
        transform.position = start;
    }

    public void Over()
    {
        active = false;
        gameObject.SetActive(false);
        transform.position = start;
    }
    private void Update()
    {
        if (active)
        {
            if (!GameController.Instance.TimeStop && Data != null)
            {
                transform.Translate(new Vector3(speed * Time.fixedDeltaTime * -1, 0, 0));
            }
        }
    }


    private void OnMouseUp()
    {
        if (click)
        {
            EventOn();

            //EventObjectPool.setEventPool(this);//Pool로 이동
            Over();
        }

        if (!click)//회피 : Pool로 이동
        {
            //EventObjectPool.setEventPool(this);
            Over();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (contact)
        {
            EventOn();

            active = false;
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
