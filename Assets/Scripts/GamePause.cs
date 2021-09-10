using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD

public class GamePause : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject settingScreen;
    /*public GameObject numberScreen;
    public GameObject number3;
    public GameObject number2;
    public GameObject number1;*/

    GameObject BackgroundMusic;
    AudioSource backmusic;
=======
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public GameObject pauseScreen; //일시정지창
    public GameObject settingScreen; //설정창
    
    public GameObject numberScreen;

    public GameObject num1;
    public GameObject num2;
    public GameObject num3;

    public Image number1;
    public Image number2;
    public Image number3;

    //GameObject BackgroundMusic;
    //AudioSource backmusic;
>>>>>>> main

    //bool isPause;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        
=======

>>>>>>> main
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        
    }

    public void OnClickGamePause()
=======

    }

    public void OnClickGamePause() //일시정지
>>>>>>> main
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(true);
    }

<<<<<<< HEAD
    public void OnClickGameContinue()
=======
    public void OnClickGameContinue() //이어하기
>>>>>>> main
    {
        Time.timeScale = 1;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(false);
        //numberScreen.SetActive(true); //반투명 이미지만
        //StartCoroutine(CountDown3());
    }

<<<<<<< HEAD
    /*IEnumerator CountDown3() // 알파값 0 -> 1
    {
        number3.SetActive(true);
=======
    IEnumerator CountDown3() // 알파값 0 -> 1
    {
        num3.SetActive(true);
        number3.color = new Color(number3.color.r, number3.color.g, number3.color.b, 0);
        
        while (number3.color.a > 0.0f)
        {
            number3.color = new Color(number3.color.r, number3.color.g, number3.color.b, number3.color.a + (Time.deltaTime / 3.0f));
            yield return null;
            
        }

>>>>>>> main
        yield return new WaitForSeconds(1f);
        StartCoroutine(CountDown2());
    }

    IEnumerator CountDown2() // 알파값 0 -> 1
    {
<<<<<<< HEAD
        number3.SetActive(false);
        number2.SetActive(true);
=======
        num2.SetActive(true);
        number2.color = new Color(number2.color.r, number2.color.g, number2.color.b, 0);

        while (number2.color.a > 0.0f)
        {
            number2.color = new Color(number2.color.r, number2.color.g, number2.color.b, number2.color.a + (Time.deltaTime / 3.0f));
            yield return null;

        }

>>>>>>> main
        yield return new WaitForSeconds(1f);
        StartCoroutine(CountDown1());
    }

    IEnumerator CountDown1() // 알파값 0 -> 1
    {
<<<<<<< HEAD
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


    public void OnClickGameSetting()
    {
        Time.timeScale = 0;
=======
        num1.SetActive(true);
        number1.color = new Color(number1.color.r, number1.color.g, number1.color.b, 0);

        while (number1.color.a > 0.0f)
        {
            number1.color = new Color(number1.color.r, number1.color.g, number1.color.b, number1.color.a + (Time.deltaTime / 3.0f));
            yield return null;

        }

        numberScreen.SetActive(false);

        //yield return new WaitForSeconds(0f);
        //StartCoroutine(CountDown0());
    }



    public void OnClickGameSetting() //설정
    {
        //Time.timeScale = 0;
>>>>>>> main
        ButtonSound._buttonInstance.onButtonAudio();
        settingScreen.SetActive(true);
    }

<<<<<<< HEAD
    public void OnClickSettingExit()
    {
        Time.timeScale = 0;
=======
    public void OnClickSettingExit() //설정창 나가기
    {
        //Time.timeScale = 0;
>>>>>>> main
        ButtonSound._buttonInstance.onButtonAudio();
        settingScreen.SetActive(false);
    }

<<<<<<< HEAD
    public void OnClickGameSave()
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        //pauseScreen.SetActive(false);
=======
    public void OnClickGameSave() //저장하고 종료하기
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(false);
        Application.Quit();
>>>>>>> main
    }


}
