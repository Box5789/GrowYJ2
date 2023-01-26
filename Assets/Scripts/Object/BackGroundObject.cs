using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundObject : MonoBehaviour//추후 상속으로 변경
{
    private void FixedUpdate()
    {
        if (!GameController.Instance.TimeStop)
        {
            transform.Translate(new Vector3(GameController.Instance.MoveSpeed * Time.fixedDeltaTime * -1, transform.position.y, transform.position.z));
            if (transform.position.x < -22)
                transform.position = new Vector3(33, 0, 0);
        }
    }
}
