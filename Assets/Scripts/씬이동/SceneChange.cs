using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void onLoadStartScene()
    {
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

        SceneManager.LoadScene("StartScene");
    }

    public void onLoadMainScene()
    {
        SceneManager.LoadScene("maingameScene");
    }

    public void onLoadMinigameScene()
    {
        SceneManager.LoadScene("minigameSceneFinish");
    }

    public void onLoadShopScene()
    {
        SceneManager.LoadScene("shopScene");
    }

    public void onLoadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
