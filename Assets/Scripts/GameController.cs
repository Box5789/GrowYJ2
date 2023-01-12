using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

public class GameController : MonoBehaviour
{
    GameModel model;

    private void Awake()
    {
        //이벤트에 함수 달기
        model = new GameModel();
        gameObject.GetComponent<GameView>().이벤트셋팅();
    }


    //이벤트
    public delegate void 이벤트();
    public static event 이벤트 이벤트발생;

    float 이벤트발생시간, tmp_time;//이벤트발생시간 : 이벤트 발생 간격 시간(5~8초), tmp_time : 시간 비교 변수
    bool 이벤트발생여부 = false;


    //분기점
    float 분기점시간;//분기점시간 : 분기점 측정 시간(1분)
    

    // Start is called before the first frame update
    void Start()
    {
        model.초기데이터셋팅();

    }

    // Update is called once per frame
    void Update()
    {
        if (!이벤트발생여부)
        {
            if (분기점시간 >= 60.0f)
            {
                //분기점 발생
                이벤트발생여부 = true;
            }
            else if (이벤트발생시간 <= 0.0f)
            {
                이벤트변수초기화();
            }
            else if (tmp_time >= 이벤트발생시간)
            {
                이벤트발생();
                이벤트발생여부 = true;

                //나중에 꼭 지우기
                이벤트변수초기화();
            }
            else
            {
                //시간지남 = 영준이 걸어감
                tmp_time += Time.deltaTime;
                분기점시간 += Time.deltaTime;
            }
        }
        
    }

    void 이벤트변수초기화()
    {
        //이벤트 변수 초기화
        이벤트발생시간 = Random.Range(5.0f, 8.0f);
        tmp_time = 0.0f;
        이벤트발생여부 = false;

        Debug.Log("이벤트변수 초기화\n다음 이벤트 발생 간격 : " + 이벤트발생시간.ToString());
    }
}
