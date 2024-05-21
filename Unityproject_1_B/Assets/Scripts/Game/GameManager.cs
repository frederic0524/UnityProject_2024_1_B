using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CircleObject;               //과일 프리맵 오브젝트
    public Transform GenTransform;                //과일이 생성될 위치 오브젝트
    public float TimeCheck;                       //시간을 체크하기 위한 (float)값
    public bool isGen;                            //생성 완료 체크 (bool) 값

    public int Point;                                  //점수 값 선언 (int)
    public int BestScore;
    public static event Action<int> OnPointChanged;    //event Action 선언 (Point 값이 변경될 경우 호출)
    public static event Action<int> OnBestScoreChanged;
    // Start is called before the first frame update
    void Start()
    {
        BestScore = PlayerPrefs.GetInt("BestScore");
        GenObject();                              //게임이 시작되었을때 함수를 호출해서 초기화 시킨다
        OnPointChanged?.Invoke(Point);
        OnBestScoreChanged?.Invoke(BestScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGen)   // if(inGen == false)
        {
            TimeCheck -= Time.deltaTime;                        //매 프레임마다 프레임 시간을 빼준다
            if(TimeCheck <= 0)                                  //해당 초 시간이 지났을 경우 (1초 -> 0초가 되었을 경우)
            {
                int RandomNumber = UnityEngine.Random.Range(0, 3);
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

        Point += (int)Math.Pow(index, 2) * 10;           //index의 2승으로 점수 포인트
        OnPointChanged?.Invoke(Point);                   //포인트가 변경되었을때 이벤트에 변경 되었다고 알림
    }

    public void EndGame()
    {
        if(Point > BestScore)                                  //포인트와 비교한다.
        {
            BestScore = Point;
            PlayerPrefs.SetInt("BestScore", BestScore);           //포인트가 더 클경우 저장한다.
            OnBestScoreChanged?.Invoke(BestScore);
        }
    }
}
