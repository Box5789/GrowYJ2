using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestEndingNode : MonoBehaviour
{
    public TestEndingData data;
    // ↓ Test : 테스트 확인 용
    [SerializeField] private string ID;
    [SerializeField] private string Name;
    [SerializeField] private GameObject Parent;
    [SerializeField] private List<GameObject> Childs;

    private GameObject UIGroup;
    private GameObject UIText;

    void SetData()
    {
        ID = data.GetID();
        Name = data.GetName();
        Parent = data.GetParentGameObject();
        Childs = data.GetChildrensObject();
    }

    public void Remove()
    {
        data.GetParent().DeleteChild(gameObject);

        if(data.GetChildrens() == null)
        {
            foreach (GameObject child in data.GetChildrensObject())
            {
                data.DeleteChild(child);
                child.GetComponent<TestEndingNode>().Remove();
            }
        }
        Destroy(gameObject);
    }

    //Var
    Vector3 mouse, clickposition;
    LineRenderer line;
    bool Move, Drag, Line, Select = false;

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

        //노드 확인용 UI
        UIText = Instantiate(Resources.Load<GameObject>("Prefabs/NodeName"));
        UIText.transform.parent = GameObject.Find("Canvas/NodeNameGroup").gameObject.transform;
        UIText.name = data.GetID() + "_NameText";
        UIText.GetComponent<TMP_Text>().text = data.GetName();
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
        if (Select)
        {
            if(Input.GetKeyUp(KeyCode.Delete))
            {
                Remove();
            }
        }

        //UI
        UIText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        clickposition = Input.mousePosition;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            EEM.Instance.StartDrag = gameObject;
            Drag = true;

            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position);
        }
        else
        {
            Move = true;
        }

        EEM.Instance.DragNode = gameObject;
        enabled = true;
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
                data.SetPosition(transform.position);

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
        if(!(Drag) && !(Move) && !Select)
        {
            if(EEM.Instance.StartDrag != gameObject)
            {
                EEM.Instance.EndDrag = gameObject;
                Round.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
        if (!(Drag) && !(Move) && !(Select))
        {
            if (EEM.Instance.StartDrag != gameObject)
            {
                EEM.Instance.EndDrag = null;
                Round.SetActive(false);
            }
        }
    }

    private void OnMouseUp()
    {
        if (Move)
        {
            Move = false;
        }

        if(Vector2.Equals(clickposition, Input.mousePosition))
        {
            if (Select)
            {
                Select = false;
                Round.SetActive(false);
            }
            else
            {
                Select = true;
                Round.SetActive(true);
            }
        }
        else if(Drag)
        {
            Drag = false;

            if(EEM.Instance.EndDrag != null)
            {
                if (data.GetParent() != null)
                {
                    data.GetParent().DeleteChild(gameObject);
                }

                data.SetParent(EEM.Instance.EndDrag);
                EEM.Instance.EndDrag.GetComponent<TestEndingNode>().data.AddChild(gameObject);

                Line = true;
                line.SetPosition(1, data.GetParent().GetPosition());
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
