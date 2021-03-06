using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public int data_money_total;
    public int data_money;
    public int data_cloth;
    public int date;
    public int difficulty;
    public string name;
    public string score_viewed_time;
    public string total_viewed_time;
    public bool bgm_muted;
    public bool sound_muted;
    public bool vibration_muted;
    public float bgm_slider_value;
    public float sound_slider_value;
}

public class GameStaticData : MonoBehaviour
{
    //데이터 베이스 저장 x 변수들
    public float clock_hand_rot = 0;
    public float mainscene_time = 0;
    public int click_count = 0;
    public bool is_click = false;

    public static GameData data;
    // Update is called once per frame
    void Update()
    {
        /*
         * 뒤로가기 한 번 눌러서 종료 팝업창 뜨는 것으로 바꿨으므로 주석 처리
        if (Input.GetKey(KeyCode.Escape) && !this.is_click)
        {
            this.StartCoroutine(this.CrQuitTimer());
            this.is_click = true;

            this.click_count++;
            Debug.Log(this.click_count);

            if(this.click_count >= 2)
            {
                this.click_count = 0;
                this.is_click = false;
                Application.Quit();
            }
        }
        */

    }

    IEnumerator CrQuitTimer()
    {
        yield return new WaitForSeconds(0.1f);
        this.is_click = false;

        yield return new WaitForSeconds(0.3f);
        this.is_click = false;
        this.click_count = 0;

    }

    public void LoadGameData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/GameData.json");
        data = JsonUtility.FromJson<GameData>(str);

        Debug.Log("불러온 데이터" + str);
    }


    // 게임 저장하기
    public void SaveGameData()
    {
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(data));

        Invoke("LoadGameData", 0.5f);
    }

    public void DateUp()
    {
        data.date++;
        SaveGameData();

    }

    public void DataClear()
    {
        data.date = 0;
        SaveGameData();
    }

}
