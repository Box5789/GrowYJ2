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
        Debug.Log(Data.EventID + " / " + Data.select_type + " / " + Data.conversation_type);
    }

    // Start is called before the first frame update
    void Start()
    {
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

    private void FixedUpdate()
    {
        if (!GameController.Instance.TimeStop)
        {
            transform.Translate(new Vector2(GameController.Instance.MoveSpeed * Time.fixedDeltaTime * -1, transform.position.y));
        }
    }

    private void OnMouseDown()
    {
        if (click)
        {
            EventOn();

            Destroy(gameObject);//Pool�� �̵�(����)
        }

        if (!click)//ȸ�� : Ŭ���� Destroy : Pool�� �̵�(����)
        {
            Destroy(gameObject);
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
