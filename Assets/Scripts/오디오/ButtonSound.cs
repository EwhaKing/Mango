using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    AudioSource audioSource;
    public static ButtonSound _buttonInstance;

    public AudioClip buttonBGM;
    public AudioClip babylaughBGM;
    public AudioClip popupBGM;
    public AudioClip moneyBGM;

    public Image soundOnImg;
    public Image soundOffImg;

    private bool soundMuted = false;
    public static int soundOnOff = 1;

    // Start is called before the first frame update
    void Awake()
    {
        _buttonInstance = this;
    }

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>(); //GameManager 오브젝트

        if (!PlayerPrefs.HasKey("soundMuted"))
        {
            PlayerPrefs.SetInt("soundMuted", 0);
            soundLoad();
        }

        else
        {
            soundLoad();
        }

        UpdateSoundImg();
        AudioListener.pause = soundMuted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickButtonSoundOn()
    {
        if (soundMuted == false)
        {
            soundMuted = true;
            soundOnOff = 0;
        }

        else
        {
            soundMuted = false;
            soundOnOff = 1;
        }

        soundSave();
        UpdateSoundImg();

        /*if (soundOnOff == 1)
        {
            soundOnOff = 0;
            soundOnImg.enabled = false;
            soundOffImg.enabled = true;
        }

        else
        {
            soundOnOff = 1;
            soundOnImg.enabled = true;
            soundOffImg.enabled = false;
        }*/
        //soundOnOff = 1;
    }

    private void UpdateSoundImg()
    {
        if (soundMuted == false)
        {
            soundOnImg.enabled = true;
            soundOffImg.enabled = false;
        }

        else
        {
            soundOnImg.enabled = false;
            soundOffImg.enabled = true;
        }
    }

    private void soundLoad()
    {
        //bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        soundMuted = PlayerPrefs.GetInt("soundMuted") == 1;
    }

    private void soundSave()
    {
        //PlayerPrefs.SetFloat("bgmVolume", bgmSlider.value);
        PlayerPrefs.SetInt("soundMuted", soundMuted ? 1 : 0);
    }


    public void onButtonAudio()
    {
        audioSource.clip = buttonBGM;
        audioSource.volume = 0.7f;
        if(soundOnOff == 1)
        {
            audioSource.Play();
        }
    }

    public void onBabyAudio()
    {
        audioSource.clip = babylaughBGM;
        //audioSource.Play();
        if (soundOnOff == 1)
        {
            audioSource.Play();
        }
    }

    public void onPopUpAudio()
    {
        audioSource.clip = popupBGM;
        audioSource.volume = 0.5f;
        //audioSource.Play();
        if (soundOnOff == 1)
        {
            audioSource.Play();
        }
    }

    public void onMoneyAudio()
    {
        audioSource.clip = moneyBGM;
        //audioSource.Play();
        if (soundOnOff == 1)
        {
            audioSource.Play();
        }
    }
}
