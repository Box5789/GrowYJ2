using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

public class GameController : MonoBehaviour
{
    GameModel model;
    GameView view;

    private void Awake()
    {
        model = new GameModel();
        view = gameObject.GetComponent<GameView>();

        //������ �о���� : ����
    }


    //�̺�Ʈ
    public delegate void �̺�Ʈ();
    public static event �̺�Ʈ �̺�Ʈ�߻�;
    public static event �̺�Ʈ �б����߻�;

    float �̺�Ʈ�߻��ð�, tmp_time;//�̺�Ʈ�߻��ð� : �̺�Ʈ �߻� ���� �ð�(5~8��), tmp_time : �ð� �� ����
    bool �̺�Ʈ�߻����� = false;


    //�б���
    float �б����ð�;//�б����ð� : �б��� ���� �ð�(1��)
    

    // Start is called before the first frame update
    void Start()
    {
        model.�ʱⵥ���ͼ���();

    }

    // Update is called once per frame
    void Update()
    {
        if (!�̺�Ʈ�߻�����)
        {
            if (�б����ð� >= 60.0f)//�б��� üũ
            {
                �б����߻�();
                �̺�Ʈ�߻����� = true;
            }
            else if (�̺�Ʈ�߻��ð� <= 0.0f)
            {
                �̺�Ʈ�����ʱ�ȭ();
            }
            else if (tmp_time >= �̺�Ʈ�߻��ð�)
            {
                �̺�Ʈ�߻�();
                �̺�Ʈ�߻����� = true;

                //���߿� �� �����
                �̺�Ʈ�����ʱ�ȭ();
            }
            else
            {
                //�ð����� = ������ �ɾ
                tmp_time += Time.deltaTime;
                �б����ð� += Time.deltaTime;
            }
        }
        
    }

    void �̺�Ʈ�����ʱ�ȭ()
    {
        //�̺�Ʈ ���� �ʱ�ȭ
        �̺�Ʈ�߻��ð� = Random.Range(5.0f, 8.0f);
        tmp_time = 0.0f;
        �̺�Ʈ�߻����� = false;

        //Debug.Log("�̺�Ʈ���� �ʱ�ȭ\n���� �̺�Ʈ �߻� ���� : " + �̺�Ʈ�߻��ð�.ToString());
    }
}
