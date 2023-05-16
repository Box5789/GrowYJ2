using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling.Memory.Experimental;

public class TestEndingNode : MonoBehaviour
{
    public TestEndingData data;
    [SerializeField] private string ID;
    [SerializeField] private string Name;
    [SerializeField] private TestEndingData Parent;
    [SerializeField] private List<TestEndingData> Childs;

    void SetData()
    {
        ID = data.GetID();
        Name = data.GetName();
        Parent = data.GetParent();
        Childs = data.GetChildrens();
    }

    public void Remove(TestEndingData remove)
    {
        //TODO : 테스트 필요
        //Q:TestEndingData안에서 Destroy 시 게임오브젝트도 삭제 되나? : 될듯?
        foreach (TestEndingData child in data.GetChildrens())
        {
            Remove(child);
        }
        Destroy(this.gameObject);
    }

    //Var
    Vector3 mouse, clickposition;
    LineRenderer line;
    bool Move, Drag, Line, Click = false;

    GameObject Round;

    private void Start()
    {
        line = GetComponent<LineRenderer>();

        line.SetWidth(0.2f, 0.2f);
        line.SetColors(Color.white, Color.white);
        line.enabled = false;

        Round = transform.GetChild(0).gameObject;
        Round.SetActive(false);

        SetData();
    }

    // Update is called once per frame
    void Update()
    {
        if (data.GetParent() != null && !Drag)
        {
            line.SetPosition(1, data.GetParent().GetPosition());
        }
        if (Input.GetMouseButton(0))
        {
            line.SetPosition(0, transform.position);
        }
        if (Click)
        {
            if(Input.GetKeyUp(KeyCode.Delete))
            {
                //노드삭제 - 미완
                Remove(data);
            }
        }
        
    }

    private void OnMouseDown()
    {
        clickposition = Input.mousePosition;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            EEM.Instance.StartDrag = this;
            Drag = true;

            line.enabled = true;
            line.SetPosition(0, transform.position);
        }
        else
        {
            Move = true;
        }

        EEM.Instance.DragNode = this;
        this.enabled = true;
    }

    private void OnMouseDrag()
    {
        if(!Vector2.Equals(clickposition, Input.mousePosition))
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;

            if (Move)
            {
                transform.position = mouse;

                if (Line) line.SetPosition(0, transform.position);
            }
            else if (Drag)
            {
                line.SetPosition(1, mouse);
            }
        }
    }


    private void OnMouseOver()
    {
        if(!(Drag) && !(Move))
        {
            if(EEM.Instance.StartDrag != this)
            {
                EEM.Instance.EndDrag = this;
                Round.SetActive(true);
            }
        }

    }

    private void OnMouseExit()
    {
        if (!(Drag) && !(Move))
        {
            if (EEM.Instance.StartDrag != this)
            {
                EEM.Instance.EndDrag = null;
                Round.SetActive(false);
            }
        }
    }

    private void OnMouseUp()
    {
        if(Vector2.Equals(clickposition, Input.mousePosition))
        {
            
            if (Round.active)
            {
                Round.SetActive(false);
            }
            else
            {
                Round.SetActive(true);
            }
        }
        else if (Move)
        {
            Move = false;
        }
        else if(Drag)
        {
            Drag = false;

            if(EEM.Instance.EndDrag != null)
            {
                if (data.GetParent() != null)
                {
                    data.GetParent().DeleteChild(data);
                }

                data.SetParent(EEM.Instance.EndDrag.data);
                EEM.Instance.EndDrag.data.AddChild(data);

                Line = true;
            }
            else if(data.GetParent() == null)
            {
                line.enabled = false;
            }
            else
            {
                line.SetPosition(1,data.GetParent().GetPosition());
            }

            EEM.Instance.StartDrag = null;
            EEM.Instance.EndDrag = null;
        }

        SetData();
    }
}
