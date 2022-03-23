using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameStart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClickGameRealStart()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        Time.timeScale = 1;
        SceneManager.LoadScene("maingameScene");

        //Debug.Log("버튼 작동 잘 되니?");
    }
}
