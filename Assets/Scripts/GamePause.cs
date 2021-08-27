using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject settingScreen;

    GameObject BackgroundMusic;
    AudioSource backmusic;

    //bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        //isPause = false;
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
    }

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
