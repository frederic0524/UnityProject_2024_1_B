using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;                     //드래그 중인지 판단하는 (bool)
    public bool isUsed;                     //사용 완료 판단하는 (bool)
    Rigidbody2D rigidbody2D;                //2D 강체를 불러온다.

    public int index;                      //과일 번호를 만든다.

    public float EndTime = 0.0f;           //종료 선 시간 체크 변수(float)
    public SpriteRenderer spriteRenderer;  //종료시 스프라이트 색을 변환 시키키 위해 접근 선업

    public GameManager gameManager;

    void Awake()                                         //시작하기 전 소스 단계
    {
        isUsed = false;                                  //사용 완료가 되지 않음(처음 사용)
        rigidbody2D = GetComponent<Rigidbody2D>();       //강체를 가져온다.
        rigidbody2D.simulated = false;                   //생성될때는 시뮬레이팅 되지 않는다.
        spriteRenderer = GetComponent<SpriteRenderer>(); //해당 오브젝트의 스프라이트 랜더러 접근
    }
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();    //게임 메니저를 얻어온다.
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

    public void Used()
    {
        isDrag = false;                 //드래그가 종료
        isUsed = true;                  //사용이 완료
        rigidbody2D.simulated = true;   //물리 현상 시작
    }

    public void OnTriggerStay2D(Collider2D collision)    //Trigger 충돌 중일 때
    {
        if(collision.tag == "EndLine")                  //충돌중인 물체가의 TAG가 EndLine 일 경우
        {
            EndTime += Time.deltaTime;                  //프레임시작만큼 누적 시켜서 초를 만든다.

            if(EndTime > 1)                             //충돌진행이 1초 되었을 경우
            {
                spriteRenderer.color = new Color(0.9f, 0.2f, 0.2f);      //빨강색 처리
            }
            if(EndTime > 3)
            {
                gameManager.EndGame();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "EndLine")                   //충돌 물체가 빠져 나갔을때
        {
            EndTime = 0.0f;
            spriteRenderer.color = Color.white;          //기존 색상으로 변경
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)            //2D 충돌이 일어날 겨우
    {
        if (index >= 7)                      //준비된 과일이 최대 7개
            return;

        if (collision.gameObject.tag == "Fruit")           //충돌 물체의 TAG가 Fruit일 경우
        {
            CircleObject temp = collision.gameObject.GetComponent<CircleObject>();        //임시로 Class temp를 선언하고 충돌체의 Class(CircleObject)를 받아온다.

            if(temp.index == index)   //과일 번호가 같은 경우
            {
                if(gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())    //유니티에서 지원하는 고유의 ID를 받아와서 ID가 큰쪽에서 다음 과일 생성
                {
                    //GameManager 에서 생성함수 호출
                    GameObject Temp = GameObject.FindWithTag("GameManager");
                    if (Temp != null)
                    {
                        Temp.gameObject.GetComponent<GameManager>().MergeObject(index, gameObject. transform.position);
                    }
                    Destroy(temp.gameObject);             //충돌 물체 파괴
                    Destroy(gameObject);                  //자기 자신 파괴
                }
            }
        }
    }
}
