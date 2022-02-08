using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public int data_money;
    public int data_cloth;
    public int date;
    public string name;

}

public class GameStaticData : MonoBehaviour
{
    //데이터 베이스 저장 x 변수들
    public float clock_hand_rot = 0;
    public float mainscene_time = 0;
    public int click_count = 0;
    public bool is_click = false;

    public static GameData data;
    //public Text dateStart;
    //public List<Sprite> startButton;
    //public GameObject start_button;

    //스타트 버튼 이어하기랑 게임시작 중 선택하는 코드 부분 StartButtonText.cs에 옮겼습니다! (로딩씬에서 gamestaticdata 시작하기위해)

    void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("GameData"));

        if (File.Exists(Application.persistentDataPath + "/GameData.json"))
        {
            Debug.LogWarning("게임파일있음");
            Debug.Log("파일 주소: " + Application.persistentDataPath + "/GameData.json");
            LoadGameData();
            //DateCheck(); 
        }
        else
        {
            Debug.LogWarning("게임파일없음");
            Invoke("CreateFile", 0.1f);
            Invoke("LoadGameData", 0.2f);
            Invoke("DateCheck", 0.3f);
        }
    }

    void CreateFile()
    {
        File.Create(Application.persistentDataPath + "/GameData.json").Close();
        data = JsonUtility.FromJson<GameData>("{\"data_money\": 0,\"data_cloth\": 0,\"date\" :  0,\"name\" : \"\"}");
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(data));
    }

    // Start is called before the first frame update
    /*void DateCheck()
    {
        if (data.date > 0)
        {
            start_button.GetComponent<Image>().sprite = startButton[1];
            dateStart.text = data.date.ToString();
            dateStart.transform.gameObject.SetActive(true);
        }
        else
        {
            dateStart.transform.gameObject.SetActive(false);
            start_button.GetComponent<Image>().sprite = startButton[0];
        }
    }*/

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
