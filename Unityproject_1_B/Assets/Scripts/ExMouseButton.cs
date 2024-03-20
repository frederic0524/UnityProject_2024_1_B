using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;     //ui를 사용하기 위해서 추가

public class ExMouseButton : MonoBehaviour
{
    public int Hp = 100;
    public Text textUI;  //UI 글씨 문자열 추가
    
    // Update is called once per frame
    void Update()            //매 프레임마다 호출 된다.
    {
        //Debug.Log("체력 : " + Hp);

        textUI.text = "체력 : " + Hp.ToString();

        if (Input.GetMouseButton(0))  //마우스 입력이 들어왔을 때
        {
            Hp -= 10;
            Debug.Log("체력 : " + Hp); //체력확인을 위한 Debug.Log 함수
            if(Hp <= 0 )          //Hp가 0이하로 내려가면
            {
                textUI.text = "체력 : " + Hp.ToString();
                Destroy(gameObject);      //이 오브젝트를 파과한다는 함수
            }        
         }

    }
}
