using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CircleObject;               //���� ������ ������Ʈ
    public Transform GenTransform;                //������ ������ ��ġ ������Ʈ
    public float TimeCheck;                       //�ð��� üũ�ϱ� ���� (float)��
    public bool isGen;                            //���� �Ϸ� üũ (bool) ��

    public int Point;                                  //���� �� ���� (int)
    public int BestScore;
    public static event Action<int> OnPointChanged;    //event Action ���� (Point ���� ����� ��� ȣ��)
    public static event Action<int> OnBestScoreChanged;
    // Start is called before the first frame update
    void Start()
    {
        BestScore = PlayerPrefs.GetInt("BestScore");
        GenObject();                              //������ ���۵Ǿ����� �Լ��� ȣ���ؼ� �ʱ�ȭ ��Ų��
        OnPointChanged?.Invoke(Point);
        OnBestScoreChanged?.Invoke(BestScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGen)   // if(inGen == false)
        {
            TimeCheck -= Time.deltaTime;                        //�� �����Ӹ��� ������ �ð��� ���ش�
            if(TimeCheck <= 0)                                  //�ش� �� �ð��� ������ ��� (1�� -> 0�ʰ� �Ǿ��� ���)
            {
                int RandomNumber = UnityEngine.Random.Range(0, 3);
                GameObject Temp = Instantiate(CircleObject[RandomNumber]);        //���������� ������Ʈ�� ��
                Temp.transform.position = GenTransform.position;
                isGen = true;
            }
        }
    }
    public void GenObject()
    {
        isGen = false;          //�ʱ�ȭ : isGen�� false (���� ���� �ʾҴ�)
        TimeCheck = 1.0f;       //1���� ���� �������� ���� ��Ű�� ���� �ʱ�ȭ
    }

    public void MergeObject(int index, Vector3 position)  //Merge�Լ��� ���Ϲ�ȣ(int) �� ���� ��ġ��(Vector3)�� ���� �޴´�.
    {
        GameObject Temp = Instantiate(CircleObject[index]);    //index�״�� ����. (0 ���� �迭�� ���۵����� index ���� 1 �� �־)
        Temp.transform.position = position;
        Temp.GetComponent<CircleObject>().Used();

        Point += (int)Math.Pow(index, 2) * 10;           //index�� 2������ ���� ����Ʈ
        OnPointChanged?.Invoke(Point);                   //����Ʈ�� ����Ǿ����� �̺�Ʈ�� ���� �Ǿ��ٰ� �˸�
    }

    public void EndGame()
    {
        if(Point > BestScore)                                  //����Ʈ�� ���Ѵ�.
        {
            BestScore = Point;
            PlayerPrefs.SetInt("BestScore", BestScore);           //����Ʈ�� �� Ŭ��� �����Ѵ�.
            OnBestScoreChanged?.Invoke(BestScore);
        }
    }
}
