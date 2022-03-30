using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public GameObject pauseScreen; //일시정지창
    public GameObject settingScreen; //설정창

    //public GameObject ConButton;

    public GameObject numberScreen;
    public GameObject number3;
    public GameObject number2;
    public GameObject number1;
    public GameObject number0;

    public void OnClickGamePause() //일시정지
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(true);

        //StartCoroutine(BlinkAnimation());
    }

    /*IEnumerator BlinkAnimation()
    {
        float lastTime = Time.realtimeSinceStartup;
        float processTime = 0;

        processTime = Time.realtimeSinceStartup - lastTime;

        while (true)
        {
            if (processTime < 0.5f)
            {
                ConButton.GetComponent<Image>().color = new Color(1, 1, 1, 1 - processTime);
            }

            else
            {
                ConButton.GetComponent<Image>().color = new Color(1, 1, 1, processTime);

                if (processTime > 1f)
                {
                    processTime = 0;
                }
            }

            processTime += lastTime;

            Debug.Log(processTime);

            yield return null;
        }

    }*/

    public void OnClickGameContinue() //이어하기
    {
        ButtonSound._buttonInstance.onButtonAudio();

        numberScreen.SetActive(true); //뒤에 버튼 선택 안되게 하는 배경

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
                number3.GetComponent<Transform>().localScale = Vector3.one * (countDown - 3);
            }

            else if (countDown > 2)
            {
                number3.SetActive(false);
                number2.SetActive(true);

                number2.GetComponent<Transform>().localScale = Vector3.one * (countDown - 2);
            }

            else if (countDown > 1)
            {
                number2.SetActive(false);
                number1.SetActive(true);

                number1.GetComponent<Transform>().localScale = Vector3.one * (countDown - 1);
            }

            else
            {
                number1.SetActive(false);
                number0.SetActive(true);

                //number0.GetComponent<Transform>().localScale = Vector3.one * countDown;
            }

            yield return null;
        }

        for (int i = 0; i < KeepGoing.Length; i++)
        {
            KeepGoing[i].GetComponent<Image>().enabled = true;
        }

        number0.SetActive(false);
        pauseScreen.SetActive(false);

        numberScreen.SetActive(false); //배경 없애기

        Time.timeScale = 1;

    }

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

        //StartCoroutine(SettingExitTimer(1));
    }

    IEnumerator SettingExitTimer(float countTime)
    {
        float lastTime = Time.realtimeSinceStartup;
        float processTime = 0;
        float countDown = 0;

        while (processTime < countTime)
        {
            processTime = Time.realtimeSinceStartup - lastTime;
            countDown = countTime - processTime;
            Debug.Log(countDown);

            if (countDown > 0.5)
            {
                settingScreen.GetComponent<Transform>().localScale = Vector3.one * countDown;
            }

            yield return null;
        }

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

}
