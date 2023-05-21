using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    Vector2 click, drag, move;
    Vector3 startposition;
    GameObject NodeGroup;

    private void Start()
    {
        NodeGroup = GameObject.Find("EndingNodeGroup").gameObject;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        click = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);

        startposition = NodeGroup.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        drag = Camera.main.ScreenToWorldPoint(eventData.position);
        move = drag - click;

        NodeGroup.transform.position = startposition + new Vector3(move.x, move.y, 0);
    }
}
