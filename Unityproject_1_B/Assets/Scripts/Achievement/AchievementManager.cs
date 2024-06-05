using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;   //�̱��� ȭ
    public List<Achievement> achievements;       //Achievement

    public Text[] AchievementTexts = new Text[4];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void UpdateAchievementUI()
    {
        AchievementTexts[0].text = achievements[0].name;
        AchievementTexts[1].text = achievements[0].description;
        AchievementTexts[2].text = $"{achievements[0].currentProgress}/{achievements[0].goal }";
        AchievementTexts[3].text = achievements[0].isUnlocked ? "�޼�" : "�̴޼�";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AddProgress("����", 1);
        }
    }
    public void AddProgress(string achievementName, int amount)  //���� ���� ��Ȳ ���� �Լ�
    {
        Achievement achievement = achievements.Find(a => a.name == achievementName);  //�μ����� �޾ƿ� �̸����� ���� ����Ʈ���� ã�Ƽ� ��ȯ
        if(achievement != null)                                                       //��ȯ�� ������ ���� ���
        {
            achievement.Addprogress(amount);                                          //���α׷����� ���� ��Ų��.
        }
    }

    //���ο� ���� �߰�
    public void AddAchievement(Achievement achievement)
    {
        //Achievement tmep = new Achievement("�̸�", "����", 5);
        achievements.Add(achievement);                          //List�� ���� �߰�
    }
    void Start()
    {
        
    }


}
