using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SettingManager : MonoBehaviour
{
    public GameObject bgmSetting, soundSetting, vibrationSetting; //세팅 버튼 이미지 오브젝트
    public TextMeshProUGUI babyname;
     
    [SerializeField] Slider bgmSlider, soundSlider; //소리 조절 슬라이더

    // Start is called before the first frame update
    void Start()
    {
        changeBgmButtonImage();
        babyname.text = GameStaticData.data.name;
    }

    //백그라운드 뮤직(bgm) 온오프
    public void OnClickBackButton()
    {
        if (!GameStaticData.data.bgm_muted) //브금 켜져있다면
        {
            GameStaticData.data.bgm_muted = true;
            MusicManager.backmusic.Pause();
        }
        else
        {
            GameStaticData.data.bgm_muted = false;
            MusicManager.backmusic.Play();
        }
        changeBgmButtonImage();
        SaveGameData(); //데이터 저장
    }

    public void OnClickSoundButton()
    {
        if(!GameStaticData.data.sound_muted) //효과음 켜져있다면
        {
            GameStaticData.data.sound_muted = true;
        }
        else
        {
            GameStaticData.data.sound_muted = false;
        }
        changeBgmButtonImage();
        SaveGameData();
    }

    public void OnClickVibrationButton()
    {
        if (!GameStaticData.data.vibration_muted) //진동 켜져있다면
        {
            GameStaticData.data.vibration_muted = true;
        }
        else
        {
            GameStaticData.data.vibration_muted = false;
        }
        changeBgmButtonImage();
        SaveGameData();
    }

    public void changeBgmButtonImage()
    {
        //배경음 상태
        if (!GameStaticData.data.bgm_muted) //온상태
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
        if (!GameStaticData.data.sound_muted) //온상태
        {
            soundSetting.transform.GetChild(1).gameObject.SetActive(true);
            soundSetting.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            soundSetting.transform.GetChild(1).gameObject.SetActive(false);
            soundSetting.transform.GetChild(2).gameObject.SetActive(true);
        }

        //진동 상태
        if (!GameStaticData.data.vibration_muted) //온상태
        {
            vibrationSetting.transform.GetChild(1).gameObject.SetActive(true);
            vibrationSetting.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            vibrationSetting.transform.GetChild(1).gameObject.SetActive(false);
            vibrationSetting.transform.GetChild(2).gameObject.SetActive(true);
        }

        //슬라이더 버튼
        bgmSlider.value = GameStaticData.data.bgm_slider_value;
        soundSlider.value = GameStaticData.data.sound_slider_value;
    }

    public void changeBackVolume()
    {
        MusicManager.backmusic.volume = bgmSlider.value;
        GameStaticData.data.bgm_slider_value = bgmSlider.value;
        SaveGameData();
    }

    public void changeSoundVolume()
    {
        ButtonSound._buttonInstance.audioSource.volume = soundSlider.value;
        GameStaticData.data.sound_slider_value = soundSlider.value;
        SaveGameData();
    }

    //데이터 부르기
    public void LoadGameData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/GameData.json");
        GameStaticData.data = JsonUtility.FromJson<GameData>(str);

        Debug.Log(Application.persistentDataPath);
    }

    // 게임 저장하기
    public void SaveGameData()
    {
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));

        //Invoke("LoadGameData", 0.5f);
    }

}
