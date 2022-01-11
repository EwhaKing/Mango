using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public GameObject pauseScreen; //일시정지창
    public GameObject settingScreen; //설정창

    public Image bgmOnImg;
    public Image bgmOffImg;

    public Image soundOnImg;
    public Image soundOffImg;

    //public GameObject backOff;

    //public static int bgmOnOff = 1; // bgm 온오프 변수
    public static int soundOnOff = 1; // 사운드 온오프 변수

    void Start()
    {
        
    }

    /*public GameObject numberScreen;
    public GameObject number3;
    public GameObject number2;
    public GameObject number1;*/

    //GameObject BackgroundMusic;
    //AudioSource backmusic;

    //bool isPause;

    public void OnClickGamePause() //일시정지
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(true);
    }

    public void OnClickGameContinue() //이어하기
    {
        Time.timeScale = 1;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(false);
        //numberScreen.SetActive(true); //반투명 이미지만
        //StartCoroutine(CountDown3());
    }

    /*IEnumerator CountDown3() // 알파값 0 -> 1
    {
        number3.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(CountDown2());
    }

    IEnumerator CountDown2() // 알파값 0 -> 1
    {
        number3.SetActive(false);
        number2.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(CountDown1());
    }

    IEnumerator CountDown1() // 알파값 0 -> 1
    {
        number2.SetActive(false);
        number1.SetActive(true);

        yield return new WaitForSeconds(1f);
        StartCoroutine(CountDown0());
    }

    IEnumerator CountDown0() // 알파값 0 -> 1
    {
        number1.SetActive(false);
        numberScreen.SetActive(false);
        yield return null;
    }*/


    public void OnClickGameSetting() //설정
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        settingScreen.SetActive(true);
    }

    public void OnClickSettingExit() //설정창 나가기
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        settingScreen.SetActive(false);
    }

    public void OnClickGameSave() //저장하고 종료하기 - 인데 종료만 구현
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(false);
        Application.Quit();
    }


    public void OnClickBGMOn()
    {
        if (MusicManager.backmusic.isPlaying)
        {
            MusicManager.backmusic.Pause();
            bgmOnImg.enabled = false;
            bgmOffImg.enabled = true;
        }

        else
        {
            MusicManager.backmusic.Play();
            bgmOnImg.enabled = true;
            bgmOffImg.enabled = false;
        }

        //MusicManager.backmusic.Play();
    }

    public void OnClickBGMOff()
    {
        MusicManager.backmusic.Pause();
    }

    public void OnClickButtonSoundOn()
    {
        if (soundOnOff == 1)
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
        }
        //soundOnOff = 1;
    }

    public void OnClickButtonSoundOff()
    {
        soundOnOff = 0;
    }


}
