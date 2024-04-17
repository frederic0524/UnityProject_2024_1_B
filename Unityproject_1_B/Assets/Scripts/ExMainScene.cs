using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                              //UI ����ϱ� �����߰�
using UnityEngine.SceneManagement;                                 //UI ���� �Ŵ����� �ϱ����� �߰�

public class ExMainScene : MonoBehaviour
{
    public Text PointUI;                                           //UI ���� �߰�
    private void Start()
    {
        PointUI.text = PlayerPrefs.GetInt("Point").ToString();     //����� ����Ʈ ������ UI ǥ��
    }

    public void GoTOGame()
    {
        SceneManager.LoadScene("GameScene_Shoot");                 //���� ������ ����.
    }

}