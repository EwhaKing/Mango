using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //스타트씬
    public void onLoadNameScene()
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

        //GameData 초기화

        GameStaticData.data.data_money_total = 0;
        GameStaticData.data.data_money = 0;
        GameStaticData.data.data_cloth = 0;
        GameStaticData.data.date = 0;
        GameStaticData.data.name = "";

        File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));

        //ShopData 초기화

        for (int i=0; i<11; i++)
        {
            ShopDataScript.sd.item[i].own = false;
            ShopDataScript.sd.item[i].is_read = false;
        }

        File.WriteAllText(Application.persistentDataPath + "/ShopData.json", JsonUtility.ToJson(ShopDataScript.sd));

        //TeaDex 초기화

        for (int i=0; i<18; i++)
        {
            TeaDataScript.teaDex.item[i].own = false;
            TeaDataScript.teaDex.item[i].is_read = false;
        }
        
        File.WriteAllText(Application.persistentDataPath + "/TeaDex.json", JsonUtility.ToJson(TeaDataScript.teaDex));

        GameObject.Destroy(GameObject.Find("SoundManager"));

        //유저네임씬 로드
        SceneManager.LoadScene("usernameScene");
    }

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
        if (GameObject.Find("LifeManager_mini"))
            GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt = 0;

        //시간 초기화
        GameObject.Find("GameData").GetComponent<GameStaticData>().clock_hand_rot = 0;
        GameObject.Find("GameData").GetComponent<GameStaticData>().mainscene_time = 0;
        GameObject.Find("GameData").GetComponent<GameStaticData>().click_count = 0;
        GameObject.Find("GameData").GetComponent<GameStaticData>().is_click = false;

        //유저네임씬 로드
        SceneManager.LoadScene("StartScene");
    }

    //메인게임씬
    public void onLoadMainScene()
    {
        SceneManager.LoadScene("maingameScene");
    }

    //미니게임씬
    public void onLoadMinigameScene()
    {
        SceneManager.LoadScene("minigameSceneFinish");
    }

    //꾸미기씬
    public void onLoadShopScene()
    {
        SceneManager.LoadScene("shopScene");
    }

    //스토리씬
    public void onLoadStoryScene()
    {
        SceneManager.LoadScene("storyScene");
    }

    //로딩씬
    public void onLoadLoadingScene()
    {
        SceneManager.LoadScene("loadingScene");
    }

    //같은씬
    public void onLoadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
