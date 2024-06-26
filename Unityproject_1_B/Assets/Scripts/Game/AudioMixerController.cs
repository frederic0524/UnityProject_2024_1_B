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

    //스라이더 MinValue 0.001 사운드 불륨은 Log10 단위로 되어있기 때문에

    private void Awake()
    {
        //마스터 슬라이더의 값이 변경될때 리스너를 통해서 함수에 값을전달한다
        musicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        //bgm 슬라이더의 값이 변경될때 리스너를 통해서 함수에 값을전달한다
        musicBGMSlider.onValueChanged.AddListener(SetBGMVolume);
        //SFX 슬라이더의 값이 변경될때 리스너를 통해서 함수에 값을전달한다
        musicSFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    public void SetMasterVolume(float volume)                       //마스터 볼륨 슬라이더가 Mixer에 반영되게
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);   //불륨은 Log10단위에 X 20을 해준다
    }
    public void SetBGMVolume(float volume)                         //BGM 볼륨 슬라이더가 Mixer에 반영되게
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);      
    }
    public void SetSFXVolume(float volume)                        //SFX 볼륨 슬라이더가 Mixer에 반영되게
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

}
