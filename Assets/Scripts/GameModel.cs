using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    GameData ���ӵ�����;

    public GameModel()
    {
        //���� �̺�Ʈ�� �Լ� �ޱ�
        GameController.�̺�Ʈ�߻� += �̺�Ʈ�߻�;
    }

    public void �ʱⵥ���ͼ���()
    {
        //����� ���ӵ����� �о���� -> �� �̺�Ʈ ����
    }

    void �̺�Ʈ�߻�()
    {
        Debug.Log("�̺�Ʈ �߻�");
        Debug.Log("�̺�Ʈ �� �߻�");
        Debug.Log("����� ���� ���� ����");

        //delegate event�� ó���Ϸ��ϱ� �������ܼ� �ϴ� �׳�...
        Debug.Log("�̺�Ʈ ������");
    }
}
