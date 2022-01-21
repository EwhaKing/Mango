using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public GameObject bgmSetting, soundSetting; //세팅 버튼 이미지 오브젝트
    public static float bgmSliderValue = 1, soundSliderValue = 1;
     
    [SerializeField] Slider bgmSlider, soundSlider; //소리 조절 슬라이더

    // Start is called before the first frame update
    void Start()
    {
        changeBgmButtonImage();
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

    public void changeBgmButtonImage()
    {
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
