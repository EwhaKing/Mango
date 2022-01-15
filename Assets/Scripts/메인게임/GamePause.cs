using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public GameObject pauseScreen; //일시정지창
    public GameObject settingScreen; //설정창

    //public SpriteRenderer pauseRenderer;
    //public SpriteRenderer settingRenderer;

    //public SpriteRenderer pauseRenderer = GetComponent<SpriteRenderer>();
    //settingRenderer = GetComponent<SpriteRenderer>();


    
    //public GameObject backOff;

    //public static int bgmOnOff = 1; // bgm 온오프 변수
    public static int soundOnOff = 1; // 사운드 온오프 변수

    //private bool bgmMuted = false;


    //public GameObject numberScreen;
    public GameObject number3;
    public GameObject number2;
    public GameObject number1;
    public GameObject number0;

    

    //GameObject BackgroundMusic;
    //AudioSource backmusic;

    //bool isPause;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void OnClickGamePause() //일시정지
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(true);
    }

    public void OnClickGameContinue() //이어하기
    {
        //Time.timeScale = 1;
        ButtonSound._buttonInstance.onButtonAudio();
        
        //numberScreen.SetActive(true); //반투명 이미지만
        //StartCoroutine(CountDown3());

        StartCoroutine(Timer(4));

        

    }

    IEnumerator Timer(float countTime)
    {
        float lastTime = Time.realtimeSinceStartup;
        float processTime = 0;
        float countDown = 0;

        GameObject[] KeepGoing = GameObject.FindGameObjectsWithTag("KeepGoing");

        while (processTime < countTime)
        {
            processTime = Time.realtimeSinceStartup - lastTime;
            countDown = countTime - processTime;
            Debug.Log(countDown);

            if (countDown > 3)
            {

                for (int i = 0; i < KeepGoing.Length; i++)
                {
                    KeepGoing[i].GetComponent<Image>().enabled = false;
                }

                number3.SetActive(true);
            }

            else if (countDown > 2)
            {
                number3.SetActive(false);
                number2.SetActive(true);
            }

            else if (countDown > 1)
            {
                number2.SetActive(false);
                number1.SetActive(true);
            }

            else
            {
                number1.SetActive(false);
                number0.SetActive(true);
            }

            yield return null;
        }

        for (int i = 0; i < KeepGoing.Length; i++)
        {
            KeepGoing[i].GetComponent<Image>().enabled = true;
        }

        number0.SetActive(false);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        
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

    public void OnClickSettingExit2() //설정창 나가기 - 스타트화면용
    {
        Time.timeScale = 1;
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


    


    public void OnClickBGMOff()
    {
        MusicManager.backmusic.Pause();
    }

    

    public void OnClickButtonSoundOff()
    {
        //soundOnOff = 0;
    }


}
