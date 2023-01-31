using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventObject : MonoBehaviour
{
    float speed;
    EventClass Data;
    bool click = true, contact = true;

    public void SetEventClass(EventClass d, float speed)
    {
        this.speed = speed;
        Data = d;

        if (d != null)
        {
            Debug.Log(Data.EventID + " / " + Data.select_type + " / " + Data.conversation_type);

            //���� - Ŭ��&&���� : �⺻

            if (Data.select_type.Equals("����"))//���� - Only Ŭ��
            {
                contact = false;
            }
            else if (Data.select_type.Equals("ȸ��"))//ȸ�� - Only ����
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!GameController.Instance.TimeStop && Data != null)
        {
            transform.Translate(new Vector2(speed * Time.fixedDeltaTime * -1, transform.position.y));
        }
        
        if(transform.position.x < Camera.main.orthographicSize * Camera.main.aspect * -1)
        {
            EventObjectPool.setEventPool(this);
        }
    }


    private void OnMouseUp()
    {
        if (click)
        {
            EventOn();

            EventObjectPool.setEventPool(this);//Pool�� �̵�
        }

        if (!click)//ȸ�� : Pool�� �̵�
        {
            EventObjectPool.setEventPool(this);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (contact)
        {
            EventOn();

            Destroy(gameObject);//Pool�� �̵�(����)
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
