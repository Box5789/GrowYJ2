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

            EventObjectPool.setEventPool(this);//Pool로 이동
        }

        if (!click)//회피 : Pool로 이동
        {
            EventObjectPool.setEventPool(this);
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
