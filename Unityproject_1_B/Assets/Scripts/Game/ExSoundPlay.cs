using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExSoundPlay : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))            //1���� ������
        {
            SoundManger.instance.PlaySound("BackGround"); //BackGround ���
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))            //2���� ������
        {
            SoundManger.instance.PlaySound("Cannon");     //Cannon ���
        }
        
    }
}
