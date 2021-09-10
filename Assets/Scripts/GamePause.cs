using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //bool isPause;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        Time.timeScale = 1;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(false);
        //numberScreen.SetActive(true); //반투명 이미지만
        //StartCoroutine(CountDown3());
    }

    IEnumerator CountDown3() // 알파값 0 -> 1
    {
        num3.SetActive(true);
        number3.color = new Color(number3.color.r, number3.color.g, number3.color.b, 0);
        
        while (number3.color.a > 0.0f)
        {
            number3.color = new Color(number3.color.r, number3.color.g, number3.color.b, number3.color.a + (Time.deltaTime / 3.0f));
            yield return null;
            
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(CountDown2());
    }

    IEnumerator CountDown2() // 알파값 0 -> 1
    {
        num2.SetActive(true);
        number2.color = new Color(number2.color.r, number2.color.g, number2.color.b, 0);

        while (number2.color.a > 0.0f)
        {
            number2.color = new Color(number2.color.r, number2.color.g, number2.color.b, number2.color.a + (Time.deltaTime / 3.0f));
            yield return null;

        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(CountDown1());
    }

    IEnumerator CountDown1() // 알파값 0 -> 1
    {
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
        ButtonSound._buttonInstance.onButtonAudio();
        settingScreen.SetActive(true);
    }

    public void OnClickSettingExit() //설정창 나가기
    {
        //Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        settingScreen.SetActive(false);
    }

    public void OnClickGameSave() //저장하고 종료하기
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(false);
        Application.Quit();
    }


}
