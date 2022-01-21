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

    // Start is called before the first frame update
    void Awake()
    {
        _buttonInstance = this;
    }

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>(); //GameManager 오브젝트

        /*if (!PlayerPrefs.HasKey("soundMuted"))
        {
            PlayerPrefs.SetInt("soundMuted", 0);
            soundLoad();
        }

        else
        {
            soundLoad();
        }*/
    }

    /*private void soundLoad()
    {
        //bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        soundMuted = PlayerPrefs.GetInt("soundMuted") == 1;
    }

    private void soundSave()
    {
        //PlayerPrefs.SetFloat("bgmVolume", bgmSlider.value);
        PlayerPrefs.SetInt("soundMuted", soundMuted ? 1 : 0);
    }*/

    public void onButtonAudio()
    {
        audioSource.clip = buttonBGM;
        if(GamePause.soundOnOff == 1)
        {
            audioSource.Play();
        }
    }

    public void onBabyAudio()
    {
        audioSource.clip = babylaughBGM;
        //audioSource.Play();
        if (GamePause.soundOnOff == 1)
        {
            audioSource.Play();
        }
    }

    public void onPopUpAudio()
    {
        audioSource.clip = popupBGM;
        //audioSource.Play();
        if (GamePause.soundOnOff == 1)
        {
            audioSource.Play();
        }
    }

    public void onMoneyAudio()
    {
        audioSource.clip = moneyBGM;
        //audioSource.Play();
        if (GamePause.soundOnOff == 1)
        {
            audioSource.Play();
        }
    }
}
