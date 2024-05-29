using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicMasterSlider;
    [SerializeField] private Slider musicBGMSlider;
    [SerializeField] private Slider musicSFXSlider;

    //�����̴� MinValue 0.001 ���� �ҷ��� Log10 ������ �Ǿ��ֱ� ������

    private void Awake()
    {
        //������ �����̴��� ���� ����ɶ� �����ʸ� ���ؼ� �Լ��� ���������Ѵ�
        musicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        //bgm �����̴��� ���� ����ɶ� �����ʸ� ���ؼ� �Լ��� ���������Ѵ�
        musicBGMSlider.onValueChanged.AddListener(SetBGMVolume);
        //SFX �����̴��� ���� ����ɶ� �����ʸ� ���ؼ� �Լ��� ���������Ѵ�
        musicSFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    public void SetMasterVolume(float volume)                       //������ ���� �����̴��� Mixer�� �ݿ��ǰ�
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);   //�ҷ��� Log10������ X 20�� ���ش�
    }
    public void SetBGMVolume(float volume)                         //BGM ���� �����̴��� Mixer�� �ݿ��ǰ�
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);      
    }
    public void SetSFXVolume(float volume)                        //SFX ���� �����̴��� Mixer�� �ݿ��ǰ�
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

}
