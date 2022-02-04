using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingButton : MonoBehaviour
{
    public void onRestartButton()
    {
        ButtonSound._buttonInstance.onButtonAudio();

        //static 변수 초기화
        CustomerManager.check = false;
        CustomerManager.current_customer = -1;
        CustomerManager.customer_img_idx = new int[3];
        CustomerManager.tip_money = new int[3];
        CustomerManager.rich_check = 0;

        TotalMoney.plusMoney = 0;
        TotalMoney.totalMoney = 0;

        RhythmBar.success_count = 0;
        SliderTimer.time_over = false;

        //손님 수 초기화
        GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt = 0;

        //메인게임 씬 로드
        SceneManager.LoadScene("maingameScene");
    }

    public void onQuitButton()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        Application.Quit();
    }

}
