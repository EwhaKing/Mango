using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class GameData
{
    public int data_money;
    public int data_cloth;
    public int date;

}

public class GameStaticData : MonoBehaviour
{
    //데이터 베이스 저장 x 변수들
    public float clock_hand_rot = 0;
    public float mainscene_time = 0;
    public int click_count = 0;
    public bool is_click = false;

    public static GameData data;

    private void Awake()
    {
        PlayerPrefs.SetInt("Story_Start", PlayerPrefs.GetInt("Story_Start", 0));
        DontDestroyOnLoad(GameObject.Find("GameData"));

        if (PlayerPrefs.GetInt("Story_Start") == 0)
        {
            GameStaticData.data.data_cloth = 0;
            GameStaticData.data.data_money = 0;
            GameStaticData.data.date = 1;
            SaveGameData();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            LoadGameData();
        }
        catch (NullReferenceException ie)
        {
            GameStaticData.data.data_cloth = 0;
            GameStaticData.data.data_money = 0;
            GameStaticData.data.date = 1;
            SaveGameData();
            LoadGameData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Debug.Log("불러오기 성공");

        string str = File.ReadAllText(Application.dataPath + "/GameData.json");
        data = JsonUtility.FromJson<GameData>(str);

        Debug.Log("불러오기 성공");
        Debug.Log("불러온 돈: " + data.data_money + "   불러온 옷: " + data.data_cloth + "  불러온 날짜: " + data.date);
    }


    // 게임 저장하기
    public void SaveGameData()
    {
        File.WriteAllText(Application.dataPath + "/GameData.json", JsonUtility.ToJson(data));

        string str = File.ReadAllText(Application.dataPath + "/GameData.json");
        data = JsonUtility.FromJson<GameData>(str);

        // 올바르게 저장됐는지 확인 (자유롭게 변형)
        Debug.Log("저장완료");
        Debug.Log("저장한 돈: " + data.data_money + "   저장한 옷: " + data.data_cloth + "  저장한 날짜: " + data.date);
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

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
