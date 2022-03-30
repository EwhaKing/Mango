using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public static ButtonSound _buttonInstance;

    public AudioClip buttonBGM;
    public AudioClip babylaughBGM;
    public AudioClip popupBGM;
    public AudioClip moneyBGM;
    public AudioClip timeBGM; 
    public AudioClip powerSuccessBGM;
    public AudioClip powerFailBGM;

    // Start is called before the first frame update
    void Awake()
    {
        _buttonInstance = this;
    }

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>(); //GameManager 오브젝트
    }


    public void onButtonAudio()
    {
        audioSource.clip = buttonBGM;
        if(!GameStaticData.data.sound_muted) //브금 켜져있다면
        {
            audioSource.volume = GameStaticData.data.sound_slider_value;
            audioSource.Play();
        }
    }

    public void onBabyAudio()
    {
        audioSource.clip = babylaughBGM;
        if (!GameStaticData.data.sound_muted)
        {
            audioSource.volume = GameStaticData.data.sound_slider_value;
            audioSource.Play();
        }
    }

    public void onPopUpAudio()
    {
        audioSource.clip = popupBGM;
        if (!GameStaticData.data.sound_muted)
        {
            audioSource.volume = GameStaticData.data.sound_slider_value;
            audioSource.Play();
        }
    }

    public void onMoneyAudio()
    {
        audioSource.clip = moneyBGM;
        if (!GameStaticData.data.sound_muted)
        {
            audioSource.volume = GameStaticData.data.sound_slider_value;
            audioSource.Play();
        }
    }

    public void onTimeAudio()
    {
        audioSource.clip = timeBGM;
        if (!GameStaticData.data.sound_muted)
        {
            audioSource.volume = GameStaticData.data.sound_slider_value;
            audioSource.Play();
        }
    }


    public void onPowerSuccessAudio()
    {
        audioSource.clip = powerSuccessBGM;
        if (!GameStaticData.data.sound_muted)
        {
            audioSource.volume = GameStaticData.data.sound_slider_value;
            audioSource.Play();
        }
    }


    public void onPowerFailAudio()
    {
        audioSource.clip = powerFailBGM;
        if (!GameStaticData.data.sound_muted)
        {
            audioSource.volume = GameStaticData.data.sound_slider_value;
            audioSource.Play();
        }
    }
}
