using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestEndingNode : MonoBehaviour
{
    TestEndingData data;

    public GameObject Parent = null;
    public List<GameObject> Childrens = new List<GameObject>();

    private GameObject UIText;
    private RectTransform text_transform;


    public void Remove()
    {
        data.GetParentData().RemoveChild(data);

        if (Childrens != null && Childrens.Count != 0)
        {
            foreach (GameObject child in Childrens)
            {
                child.GetComponent<TestEndingNode>().Remove();
            }
        }

        Destroy(UIText);
        Destroy(gameObject);
    }

    //Var
    Vector3 mouse, clickposition;
    LineRenderer line;
    public bool Move, Drag, Line, Select = false;

    GameObject Round;


    public void SetData(TestEndingData data)
    {
        //data setting
        this.data = data;
        line = GetComponent<LineRenderer>();
        line.SetWidth(0.2f, 0.2f);
        line.SetColors(Color.white, Color.white);
        
        if (this.data.GetParentData() != null)
        {
            //Parent & Children GameObject Setting
            Parent = GameObject.Find(this.data.GetParentData().GetID() + ", " + this.data.GetParentData().GetName()).gameObject;
            Parent.GetComponent<TestEndingNode>().Childrens.Add(gameObject);

            //Line Setting
            Line = true;
            line.enabled = true;
            line.SetPosition(0, this.data.GetPosition());
            line.SetPosition(1, this.data.GetParentData().GetPosition());
        }
        else
        {
            //Line Setting
            line.enabled = false;
        }
    }

    private void Start()
    {
        //Round UI Setting
        Round = transform.GetChild(0).gameObject;
        Round.SetActive(false);

        //Node Name UI Text Setting
        UIText = Instantiate(Resources.Load<GameObject>("Prefabs/NodeName"));
        UIText.transform.parent = GameObject.Find("Canvas/NodeNameGroup").gameObject.transform;
        UIText.name = data.GetID() + "_NameText";
        UIText.GetComponent<TMP_Text>().text = data.GetName();
        text_transform = UIText.GetComponent<RectTransform>();
        text_transform.anchorMin = new Vector2(0.5f, 0.5f);
        text_transform.anchorMax = new Vector2(0.5f, 0.5f);
        text_transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Select)
        {
            if (Round.active == false)
                Round.SetActive(true);

            if (Input.GetKeyUp(KeyCode.Delete))
            {
                Remove();
            }
        }
        else if (Round.active == true)
            Round.SetActive(false);


        if (Input.GetMouseButton(0))
        {
            text_transform.position = Camera.main.WorldToScreenPoint(transform.position);
            if (Line)
            {
                line.SetPosition(0, transform.position);
                if (data.GetParentData() != null && !(Drag)) 
                    line.SetPosition(1, Parent.transform.position);

            }
        }


        if(data.GetPosition() != transform.position)
        {
            data.SetPosition(transform.position);
        }
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
        //enabled = true;
    }

    private void OnMouseDrag()
    {
        if(!Vector2.Equals(clickposition, Input.mousePosition))
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;

            if (Drag)
            {
                line.SetPosition(1, mouse);
            }
            else if (Move)
            {
                transform.position = mouse;
                data.SetPosition(transform.position);

                if (Line) 
                    line.SetPosition(0, transform.position);
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
                if (data.GetParentData() != null)
                {
                    data.GetParentData().RemoveChild(data);
                }

                Parent = EEM.Instance.EndDrag;
                EEM.Instance.EndDrag.GetComponent<TestEndingNode>().Childrens.Add(gameObject);

                data.SetParent(EEM.Instance.EndDrag.GetComponent<TestEndingNode>().data);
                EEM.Instance.EndDrag.GetComponent<TestEndingNode>().data.AddChild(data);

                Line = true;
                line.SetPosition(1, Parent.transform.position);
            }
            else if(Parent == null)
            {
                line.enabled = false;
            }
            else
            {
                line.SetPosition(1,Parent.transform.position);
            }

            EEM.Instance.StartDrag = null;
            EEM.Instance.EndDrag = null;
        }
    }
}
