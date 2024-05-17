using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CircleObject;               //과일 프리맵 오브젝트
    public Transform GenTransform;                //과일이 생성될 위치 오브젝트
    public float TimeCheck;                       //시간을 체크하기 위한 (float)값
    public bool isGen;                            //생성 완료 체크 (bool) 값

    // Start is called before the first frame update
    void Start()
    {
        GenObject();                              //게임이 시작되었을때 함수를 호출해서 초기화 시킨다
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGen)   // if(inGen == false)
        {
            TimeCheck -= Time.deltaTime;                        //매 프레임마다 프레임 시간을 빼준다
            if(TimeCheck <= 0)                                  //해당 초 시간이 지났을 경우 (1초 -> 0초가 되었을 경우)
            {
                int RandomNumber = Random.Range(0, 3);
                GameObject Temp = Instantiate(CircleObject[RandomNumber]);        //과일프리팹 오브젝트를 생
                Temp.transform.position = GenTransform.position;
                isGen = true;
            }
        }
    }
    public void GenObject()
    {
        isGen = false;          //초기화 : isGen을 false (생성 되지 않았다)
        TimeCheck = 1.0f;       //1초후 과일 프리팹을 생성 시키기 위한 초기화
    }

    public void MergeObject(int index, Vector3 position)  //Merge함수는 과일번호(int) 과 생성 위치값(Vector3)을 전달 받는다.
    {
        GameObject Temp = Instantiate(CircleObject[index]);    //index그대로 쓴다. (0 부터 배열이 시작되지만 index 값이 1 더 있어서)
        Temp.transform.position = position;
        Temp.GetComponent<CircleObject>().Used();
    }
}
