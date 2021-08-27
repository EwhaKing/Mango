using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSetting : MonoBehaviour
{
    public GameObject settingScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
