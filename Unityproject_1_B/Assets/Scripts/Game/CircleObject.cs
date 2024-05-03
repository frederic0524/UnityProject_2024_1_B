using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;                     //드래그 중인지 판단하는 (bool)
    public bool isUsed;                     //사용 완료 판단하는 (bool)
    Rigidbody2D rigidbody2D;                //2D 강체를 불러온다.

    void Start()
    {
        isUsed = false;                                  //사용 완료가 되지 않음(처음 사용)
        rigidbody2D = GetComponent<Rigidbody2D>();       //강체를 가져온다
        
    }

    void Update()
    {
        if (isUsed) return;             //사용완료된 물체를 더이상 업데이트 하지 않기 위해서 return으로 돌려 준다.

        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float leftBorder = -4f + transform.localScale.x/ 2f;
            float rightBorder = 4f - transform.localScale.x/ 2f;

            if (mousePos.x < leftBorder) mousePos. x = leftBorder;
            if (mousePos.x > rightBorder) mousePos.x = rightBorder;

            mousePos.y = 8;
            mousePos.z = 0;

            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f); //이 오브젝트의

        }
        

        if (Input.GetMouseButtonDown(0)) Drag();   //마우스 버튼이 눌렸을 때 Drag 함수 호출
        if (Input.GetMouseButtonUp(0)) Drop();     //마우스 버튼이 올라갈 때 Drop 함수 호출
    }
    void Drag()
    {
        isDrag = true;                  //드래그 시작(true)
        rigidbody2D.simulated = false;  //드래그 중에는 물리 현상이 일어난것을 막기 위해서 (false)
    }
    void Drop()
    {
        isDrag = false;                //드래그가 종료
        isUsed = true;                 //사용이 완료
        rigidbody2D.simulated = true;  //물리 현상 시작

        GameObject Temp = GameObject.FindWithTag("GameManager");             //Tag ; GameManager를 Scene 찾아서 오브젝트를 가져온다
        if(Temp != null)                                                     //해당 오브젝트가 존재하면
        {
            Temp.gameObject.GetComponent<GameManager>().GenObject();         //Genobject 함수를 호출 한다 (GetComponent 통해서 클래스에 접근한다,
        }
    }
}
