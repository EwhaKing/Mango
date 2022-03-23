using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour
{
    public GameObject bgmSetting, soundSetting, vibrationSetting; //세팅 버튼 이미지 오브젝트
    public static float bgmSliderValue = 1, soundSliderValue = 1;
    public static bool isVibration = true; //바이브레이션 on/off 정보
    public TextMeshProUGUI babyname;
     
    [SerializeField] Slider bgmSlider, soundSlider; //소리 조절 슬라이더

    // Start is called before the first frame update
    void Start()
    {
        changeBgmButtonImage();
        babyname.text = GameStaticData.data.name;
    }

    public void OnClickBackButton()
    {
        if (MusicManager.bgmMuted == false)
        {
            MusicManager.bgmMuted = true;
            MusicManager.backmusic.Pause();
        }
        else
        {
            MusicManager.bgmMuted = false;
            MusicManager.backmusic.Play();
        }
        changeBgmButtonImage();
    }

    public void OnClickSoundButton()
    {
        if(GamePause.soundOnOff == 1)
        {
            GamePause.soundOnOff = 0;
        }
        else
        {
            GamePause.soundOnOff = 1;
        }
        changeBgmButtonImage();
    }

    public void OnClickVibrationButton()
    {
        if (isVibration)
        {
            isVibration = false;
        }
        else
        {
            isVibration = true;
        }
        changeBgmButtonImage();
    }

    public void changeBgmButtonImage()
    {
        //배경음 상태
        if (MusicManager.bgmMuted == false) //온상태
        {
            bgmSetting.transform.GetChild(1).gameObject.SetActive(true); //온 이미지
            bgmSetting.transform.GetChild(2).gameObject.SetActive(false); //오프 이미지
        }
        else
        {
            bgmSetting.transform.GetChild(1).gameObject.SetActive(false);
            bgmSetting.transform.GetChild(2).gameObject.SetActive(true);
        }
        
        //효과음 상태
        if (GamePause.soundOnOff == 0) //오프상태
        {
            soundSetting.transform.GetChild(1).gameObject.SetActive(false);
            soundSetting.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            soundSetting.transform.GetChild(1).gameObject.SetActive(true);
            soundSetting.transform.GetChild(2).gameObject.SetActive(false);
        }

        //진동 상태
        if (!isVibration) //오프상태
        {
            vibrationSetting.transform.GetChild(1).gameObject.SetActive(false);
            vibrationSetting.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            vibrationSetting.transform.GetChild(1).gameObject.SetActive(true);
            vibrationSetting.transform.GetChild(2).gameObject.SetActive(false);
        }

        //슬라이더 버튼
        bgmSlider.value = bgmSliderValue;
        soundSlider.value = soundSliderValue;
    }

    public void changeBackVolume()
    {
        MusicManager.backmusic.volume = bgmSlider.value;
        bgmSliderValue = bgmSlider.value;
    }

    public void changeSoundVolume()
    {
        ButtonSound._buttonInstance.audioSource.volume = soundSlider.value;
        soundSliderValue = soundSlider.value;
    }

}
