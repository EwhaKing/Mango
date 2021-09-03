using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickGamePause()
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        pauseScreen.SetActive(true);
    }

    public void OnClickGameContinue()
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


    public void OnClickGameSetting()
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        settingScreen.SetActive(true);
    }

    public void OnClickSettingExit()
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        settingScreen.SetActive(false);
    }

    public void OnClickGameSave()
    {
        Time.timeScale = 0;
        ButtonSound._buttonInstance.onButtonAudio();
        //pauseScreen.SetActive(false);
    }


}
