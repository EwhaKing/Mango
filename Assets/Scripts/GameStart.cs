﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickGameRealStart()
    {
        ButtonSound._buttonInstance.onButtonAudio();

        SceneManager.LoadScene("maingameScene");

        //Debug.Log("버튼 작동 잘 되니?");
    }
}
