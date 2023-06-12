using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectPool : MonoBehaviour
{
    public static EventObjectPool Instance;

    private GameObject EventPrefab;

    private Queue<EventObject> EventPool = new Queue<EventObject>();

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        EventPrefab = Resources.Load<GameObject>("Prefabs/Event");
    }

    private EventObject NewEventObject()
    {
        var newEvent = Instantiate(EventPrefab).GetComponent<EventObject>();
        newEvent.gameObject.SetActive(false);
        newEvent.gameObject.transform.SetParent(transform);
        return newEvent;
    }

    public static EventObject getEventObject(EventClass data, float speed)
    {
        EventObject Object;

        if (Instance.EventPool.Count > 0)
        {
            Object = Instance.EventPool.Dequeue();
            Object.transform.SetParent(null);
            Object.gameObject.SetActive(true);
        }
        else
        {
            Object = Instance.NewEventObject();
            Object.transform.SetParent(null);
            Object.gameObject.SetActive(true);
        }

        Object.SetEventClass(data, speed);
        return Object;
    }

    public static void setEventPool(EventObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        obj.SetEventClass(null, 0);
        Instance.EventPool.Enqueue(obj);
    } 
}