using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void onLoadStartScene()
    {
        Time.timeScale = 1;

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
        if(GameObject.Find("LifeManager_mini"))
            GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt = 0;

        //시간 초기화
        GameObject.Find("GameData").GetComponent<GameStaticData>().clock_hand_rot = 0;
        GameObject.Find("GameData").GetComponent<GameStaticData>().mainscene_time = 0;
        GameObject.Find("GameData").GetComponent<GameStaticData>().click_count = 0;
        GameObject.Find("GameData").GetComponent<GameStaticData>().is_click = false;

        //스타트씬 로드
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
