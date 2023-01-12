using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    GameData 게임데이터;

    public GameModel()
    {
        //각종 이벤트에 함수 달기
        GameController.이벤트발생 += 이벤트발생;
    }

    public void 초기데이터셋팅()
    {
        //저장된 게임데이터 읽어오기 -> 뷰 이벤트 실행
    }

    void 이벤트발생()
    {
        Debug.Log("이벤트 발생");
        Debug.Log("이벤트 뷰 발생");
        Debug.Log("결과에 따라 스탯 조정");

        //delegate event로 처리하려니까 오류생겨서 일단 그냥...
        Debug.Log("이벤트 마무리");
    }
}
