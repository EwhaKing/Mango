using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static string next_scene;
    public int totaluser = 0;
    string url_login = "https://mango-love.herokuapp.com/api/server/connection"; //서버와의 접속 상태 체크 api
    string url_version_num = "https://mango-love.herokuapp.com/api/version";
    string leaderBoard;
    Data leader;

    [SerializeField] public Image progressBar;
    public Slider loadingBar;
    public TextMeshProUGUI progress_text;

    [System.Serializable]
    class Data
    {
        public Item[] item;
    }

    [System.Serializable]
    class Item
    {
        public string leaderNum;
        public string username;
        public string score;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("update_popup_big").transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(GetVersion());
    }

    IEnumerator GetVersion()
    {
        UnityWebRequest versionnum = UnityWebRequest.Get(url_version_num);
        yield return versionnum.SendWebRequest();
        progress_text.text = "아기가 새로운 메뉴가 있는지 찾아보는 중이에요...";

        if (versionnum.isNetworkError || versionnum.isHttpError)
        {
            Debug.Log(versionnum.error); //오류 시 pass
            progress_text.text = "네트워크 오류. 나중에 다시 시도해주세요";
        }
        else
        {
            string version_num = versionnum.downloadHandler.text;

            if (version_num != Application.version) //서버의 version_num과 유저의 version code 다르면
            {
                Debug.Log("업데이트 버전 숫자? " + version_num);
                GameObject.Find("update_popup_big").transform.GetChild(0).gameObject.SetActive(true); //강제 업데이트 팝업 띄우면서
                GameObject.Find("update_popup").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = version_num; //업데이트 버전 숫자 띄움
            }
            else
            {
                StartCoroutine(defineNextScene());
            }
        }

    }

    IEnumerator defineNextScene()
    {
        if (!File.Exists(Application.persistentDataPath + "/GameData.json"))
        {
            next_scene = "storyScene";
        }
        else
        {
            LoadGameData();
            if(GameStaticData.data.name == "")
            {
                next_scene = "storyScene";
            }
            else
            {
                Debug.Log("게임 첫번째 실행 x");
                next_scene = "StartScene";
            }
        }
        StartCoroutine(loadGameData());
        yield return null;
    }

    IEnumerator loadGameData()
    {
        progress_text.text = "아기 정보를 불러오고 있습니다...";
        DontDestroyOnLoad(GameObject.Find("GameData"));

        if (File.Exists(Application.persistentDataPath + "/GameData.json"))
        {
            Debug.LogWarning("게임파일있음");
            LoadGameData();

            DateTime newDate = new DateTime(0001, 01, 01, 00, 00, 00);
            string newDateString = newDate.ToString("yyyy-MM-dd HH:mm:ss");

            if (GameStaticData.data.score_viewed_time == null)
            {
                Debug.LogWarning("업데이트 한 사람이다");
                GameStaticData.data.score_viewed_time = newDateString;
                SaveGameData();
            }

            if (GameStaticData.data.total_viewed_time == null)
            {
                Debug.LogWarning("업데이트 한 사람이다");
                GameStaticData.data.total_viewed_time = newDateString;
                GameStaticData.data.bgm_slider_value = 1.0f;    //슬라이더 값..처음에 json데이터에 0으로 초기화돼있을텐데, 사용자가 0으로 바꾼 경우도 있을테니까 업데이트 한지 판단을 어케할지 모르겠어서 여기에 넣었어욥,,,
                GameStaticData.data.sound_slider_value = 1.0f;
                SaveGameData();
            }
        }
        else
        {
            Debug.LogWarning("게임파일없음");
            CreateFile();
        }

        if(File.Exists(Application.persistentDataPath + "/ScoreData.json"))
        {
            Debug.LogWarning("하루랭킹 파일있음");
        }
        else
        {
            Debug.LogWarning("하루랭킹 파일없음");
            CreateScoreFile();
        }

        if (File.Exists(Application.persistentDataPath + "/TotalData.json"))
        {
            Debug.LogWarning("전체랭킹 파일있음");
        }
        else
        {
            Debug.LogWarning("전체랭킹 파일없음");
            CreateTotalFile();
        }

        if (File.Exists(Application.persistentDataPath + "/ScoreMeData.json"))
        {
            Debug.LogWarning("내랭킹 파일있음");
        }
        else
        {
            Debug.LogWarning("내랭킹 파일없음");
            CreateScoreMeFile();
        }

        if (File.Exists(Application.persistentDataPath + "/TotalMeData.json"))
        {
            Debug.LogWarning("내랭킹 파일있음");
        }
        else
        {
            Debug.LogWarning("내랭킹 파일없음");
            CreateTotalMeFile();
        }

        StartCoroutine(moveProgress(0.2f, "shop"));
        yield return null;
    }

    IEnumerator loadShopData()
    {
        progress_text.text = "아기가 옷을 고르고 있어요...";
        DontDestroyOnLoad(GameObject.Find("ShopData"));

        if (File.Exists(Application.persistentDataPath + "/ShopData.json"))
        {
            Debug.LogWarning("옷파일 있음");
            Debug.Log("파일 주소: " + Application.persistentDataPath + "/ShopData.json");
            LoadShopData();

            ShopDataScript.sd.item[0].own = true; //1.0.0 ~ 1.6.0 버전에서 '다시 시작'한 유저 : '기본 옷' 초기화 덮어쓰기
            File.WriteAllText(Application.persistentDataPath + "/ShopData.json", JsonUtility.ToJson(ShopDataScript.sd));
        }
        else
        {
            Debug.LogWarning("옷파일 없음");
            CreateShopFile();
        }
        StartCoroutine(moveProgress(0.4f, "tea"));
        yield return null;
    }

    IEnumerator loadTeaData()
    {
        progress_text.text = "아기가 레시피를 보고 있어요...";
        DontDestroyOnLoad(GameObject.Find("TeaDex"));
        if (File.Exists(Application.persistentDataPath + "/TeaDex.json"))
        {
            Debug.LogWarning("차파일 있음");
            Debug.Log("파일 주소: " + Application.persistentDataPath + "/TeaDex.json");
            LoadTeaDex();
        }
        else
        {
            Debug.LogWarning("차파일 없음");
            File.Create(Application.persistentDataPath + "/TeaDex.json").Close();
            createTeaData();
        }
        StartCoroutine(moveProgress(0.6f, "leader"));
        yield return null;
    }

    IEnumerator loadLeaderData()
    {
        progress_text.text = "서버에 접속하고 있습니다(최대 30초 소요 가능)...";
        UnityWebRequest request = new UnityWebRequest();

        //해당 api는 서버와의 접속 상태만을 확인하는 api로, 실제 로그인 기능처럼 사용자의 정보를 요구하는 기능이 아님
        //해당 api는 사용자의 개인정보를 일체 요구하지 않음
        request = UnityWebRequest.Get(url_login);  

        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }

        StartCoroutine(moveProgress(0.8f, "scene"));
        yield return null;
    }

    IEnumerator loadStartScene()
    {
        progress_text.text = "아기가 가게를 열고 있어요...";
        yield return null; 
        AsyncOperation op = SceneManager.LoadSceneAsync(next_scene); 
        op.allowSceneActivation = false; 
        float timer = 0.0f; 
        while (!op.isDone) { 
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f) 
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, 0.8f + 0.2f*op.progress, timer);
                if (loadingBar.value >= op.progress) 
                { 
                    timer = 0f; 
                }

            } 
            else 
            {
                loadingBar.value += timer;
                if (loadingBar.value == 1.0f) 
                {
                    yield return new WaitForSeconds(1f);
                    op.allowSceneActivation = true;
                    yield break;
                }
            } 
        }

    }

    void CreateFile()
    {
        File.Create(Application.persistentDataPath + "/GameData.json").Close();
        GameStaticData.data = JsonUtility.FromJson<GameData>("{\"data_money_total\": 0,\"data_money\": 0,\"data_cloth\": 0,\"date\" :  0,\"difficulty\" :  1,\"name\" : \"\",\"score_viewed_time\" : \"0001-01-01 00:00:00\",\"total_viewed_time\" : \"0001-01-01 00:00:00\",\"bgm_muted\": false,\"sound_muted\": false,\"vibration_muted\": false,\"bgm_slider_value\": 1.0,\"sound_slider_value\": 1.0}");
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));
    }

    public void LoadGameData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/GameData.json");
        GameStaticData.data = JsonUtility.FromJson<GameData>(str);

        Debug.Log(Application.persistentDataPath);
    }


    // 게임 저장하기
    public void SaveGameData()
    {
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));

        Invoke("LoadGameData", 0.5f);
    }

    public void DateUp()
    {
        GameStaticData.data.date++;
        SaveGameData();

    }

    public void DataClear()
    {
        GameStaticData.data.date = 0;
        SaveGameData();
    }

    public void LoadShopData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/ShopData.json");
        ShopDataScript.sd = JsonUtility.FromJson<ShopData>(str);
        Debug.Log("불러온 옷: " + str);

    }

    public void CreateShopFile()
    {
        File.Create(Application.persistentDataPath + "/ShopData.json").Close();
        ShopDataScript.sd = JsonUtility.FromJson<ShopData>("{\"item\":[{\"item_num\":0,\"item_name\":\"기본옷\", \"item_cost\":0,\"own\":true,\"is_read\":false},{\"item_num\":1,\"item_name\":\"꽃무늬옷\", \"item_cost\":10000,\"own\": false,\"is_read\":false},{\"item_num\":2,\"item_name\":\"트레이닝복\", \"item_cost\":12500,\"own\":false,\"is_read\":false},{\"item_num\":3,\"item_name\":\"유치원복\", \"item_cost\":15000,\"own\":false,\"is_read\":false},{\"item_num\":4,\"item_name\":\"멜빵세트\", \"item_cost\":15000,\"own\":false,\"is_read\":false},{\"item_num\":5,\"item_name\":\"피크닉세트\", \"item_cost\":17500,\"own\":false,\"is_read\":false},{\"item_num\":6,\"item_name\":\"탐정세트\", \"item_cost\":17500,\"own\":false,\"is_read\":false},{\"item_num\":7,\"item_name\":\"꿀벌잠옷\", \"item_cost\":20000,\"own\":false,\"is_read\":false},{\"item_num\":8,\"item_name\":\"곰돌이잠옷\", \"item_cost\":20000,\"own\":false,\"is_read\":false},{\"item_num\":9,\"item_name\":\"한복 두루마기\", \"item_cost\":25000,\"own\":false,\"is_read\":false},{\"item_num\":10,\"item_name\":\"한복 치마\", \"item_cost\":25000,\"own\":false,\"is_read\":false}]}");
        File.WriteAllText(Application.persistentDataPath + "/ShopData.json", JsonUtility.ToJson(ShopDataScript.sd));
    }

    public void LoadTeaDex()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/TeaDex.json");
        TeaDataScript.teaDex = JsonUtility.FromJson<TeaDex>(str);
        Debug.Log("불러온 차도감: " + str);

    }

    public void createTeaData()
    {
        TeaDataScript.teaDex.item[0] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 0, \"tea_type\": 0, \"tea_name\": \"형아를 위한 차\", \"tea_description\" : \"먹자마자 나았다는 전설의 마법차.<br>어마어마하다는 소문이~\", \"tea_recipe\": [{\"ingredient_num\": 0,\"ingredient_amout\": 12}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[1] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 1, \"tea_type\": 2,\"tea_name\": \"눈에서 꿀이 뚝뚝\", \"tea_description\" : \"눈에서 꿀이 뚝뚝<br>내 눈엔 눈물이 뚝뚝...?ㅎ\", \"tea_recipe\": [{\"ingredient_num\": 13,\"ingredient_amout\": 12}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[2] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 2, \"tea_type\": 1,\"tea_name\": \"코코파인 플리쉬\",\"tea_description\" : \"무더운 한여름에 생각나는 달콤짜릿한 그 맛!<br>코코넨네한 아기도 못 참는다구~\",\"tea_recipe\": [{\"ingredient_num\": 5,\"ingredient_amout\": 6}, {\"ingredient_num\": 8,\"ingredient_amout\": 6}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[3] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 3, \"tea_type\": 1,\"tea_name\": \"딸바보 주스\",\"tea_description\" : \"당신 혹시 이 주스를 매일 사가지 않습니까..?<br>당신은 이미 딸바보입니다..\",\"tea_recipe\": [{\"ingredient_num\": 2,\"ingredient_amout\": 3}, {\"ingredient_num\": 3,\"ingredient_amout\": 3}, {\"ingredient_num\": 15,\"ingredient_amout\": 6}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[4] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 4, \"tea_type\": 1,\"tea_name\": \"허니만을 위한 밀크티♥\",\"tea_description\" : \"ONLY ♥ YOU\",\"tea_recipe\": [{\"ingredient_num\": 10,\"ingredient_amout\": 4}, {\"ingredient_num\": 13,\"ingredient_amout\": 3}, {\"ingredient_num\": 15,\"ingredient_amout\": 5}],\"own\": false, \"is_read\": false}");

        TeaDataScript.teaDex.item[5] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 5, \"tea_type\": 0,\"tea_name\": \"레몬 디베트\",\"tea_description\" : \"신맛 쓴맛 단맛 세가지 맛을<br>동시에 즐길 수 있는 삼미자차\",\"tea_recipe\": [{\"ingredient_num\": 0,\"ingredient_amout\": 5}, {\"ingredient_num\": 12,\"ingredient_amout\": 4}, {\"ingredient_num\": 13,\"ingredient_amout\": 3}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[6] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 6, \"tea_type\": 1,\"tea_name\": \"레몬아... 나 자몽다...\",\"tea_description\" : \"바나나의 부드러운 맛에 감겨<br>레몬과 자몽이 스르륵 잠에 녹아들..Zzz\",\"tea_recipe\": [{\"ingredient_num\": 0,\"ingredient_amout\": 3}, {\"ingredient_num\": 1,\"ingredient_amout\": 3}, {\"ingredient_num\": 3,\"ingredient_amout\": 6}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[7] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 7, \"tea_type\": 1,\"tea_name\": \"올 때 메론라떼\",\"tea_description\" : \"형아! 올 때 메론라떼~~~\",\"tea_recipe\": [{\"ingredient_num\": 9,\"ingredient_amout\": 6}, {\"ingredient_num\": 15,\"ingredient_amout\": 8}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[8] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 8, \"tea_type\": 0,\"tea_name\": \"궁금해 허니~\",\"tea_description\" : \"자니? 자는구나... 뭐 해?\",\"tea_recipe\": [{\"ingredient_num\": 10,\"ingredient_amout\": 7}, {\"ingredient_num\": 13,\"ingredient_amout\": 7}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[9] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 9, \"tea_type\": 1,\"tea_name\": \"민트 폴라디츠\",\"tea_description\" : \"근본라떼입니다. 반박 안 받음.\",\"tea_recipe\": [{\"ingredient_num\": 11,\"ingredient_amout\": 3}, {\"ingredient_num\": 14,\"ingredient_amout\": 4}, {\"ingredient_num\": 15,\"ingredient_amout\": 7}],\"own\": false, \"is_read\": false}");

        TeaDataScript.teaDex.item[10] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 10, \"tea_type\": 1,\"tea_name\": \"언덕 위의 트로피컬\",\"tea_description\" : \"언덕 위에서 트로피컬 샤워\",\"tea_recipe\": [{\"ingredient_num\": 4,\"ingredient_amout\": 4}, {\"ingredient_num\": 5,\"ingredient_amout\": 4}, {\"ingredient_num\": 6,\"ingredient_amout\": 6}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[11] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 11, \"tea_type\": 0,\"tea_name\": \"디벳 민트티\",\"tea_description\" : \"음료들의 학교에 녹차와 민트의 등장이라...\",\"tea_recipe\": [{\"ingredient_num\": 11,\"ingredient_amout\": 7}, {\"ingredient_num\": 12,\"ingredient_amout\": 7}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[12] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 12, \"tea_type\": 1,\"tea_name\": \"신호등 주스\",\"tea_description\" : \"삐빅- 딸기불입니다. 드시고 가세요.\",\"tea_recipe\": [{\"ingredient_num\": 2,\"ingredient_amout\": 4}, {\"ingredient_num\": 3,\"ingredient_amout\": 4}, {\"ingredient_num\": 7,\"ingredient_amout\": 6}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[13] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 13, \"tea_type\": 2,\"tea_name\": \"봄 사랑 벚꽃차\",\"tea_description\" : \"봄보다 멀고 벚꽃보다 가까운 사랑\",\"tea_recipe\": [{\"ingredient_num\": 19,\"ingredient_amout\": 16}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[14] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 14, \"tea_type\": 2,\"tea_name\": \"쑥쑥라떼\",\"tea_description\" : \"우유를 먹어야 쑥쑥 자라지~<br>거기에 쑥을 더한\",\"tea_recipe\": [{\"ingredient_num\": 15,\"ingredient_amout\": 10}, {\"ingredient_num\": 16,\"ingredient_amout\": 6}],\"own\": false, \"is_read\": false}");

        TeaDataScript.teaDex.item[15] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 15, \"tea_type\": 2,\"tea_name\": \"감기 뚝 마늘차\",\"tea_description\" : \"뚝! 이거 먹으면 감기 뚝!<br>감기 안 나으면 계속 먹어야 할걸? ^^\",\"tea_recipe\": [{\"ingredient_num\": 13,\"ingredient_amout\": 10}, {\"ingredient_num\": 17,\"ingredient_amout\": 6}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[16] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 16, \"tea_type\": 2,\"tea_name\": \"PROTEIN\",\"tea_description\" : \"울끈불끈\",\"tea_recipe\": [{\"ingredient_num\": 3,\"ingredient_amout\": 6}, {\"ingredient_num\": 18,\"ingredient_amout\": 10}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[17] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 17, \"tea_type\": 1,\"tea_name\": \"아프디망고!\",\"tea_description\" : \"세상 모든 사람들 이제 더 이상 <b>아프디망고!</b>\",\"tea_recipe\": [{\"ingredient_num\": 20,\"ingredient_amout\": 18}],\"own\": false, \"is_read\": false}");

        File.WriteAllText(Application.persistentDataPath + "/TeaDex.json", JsonUtility.ToJson(TeaDataScript.teaDex));
    }

    void CreateScoreFile()
    {
        File.Create(Application.persistentDataPath + "/ScoreData.json").Close();
    }

    void CreateTotalFile()
    {
        File.Create(Application.persistentDataPath + "/TotalData.json").Close();
    }

    void CreateScoreMeFile()
    {
        File.Create(Application.persistentDataPath + "/ScoreMeData.json").Close();
    }

    void CreateTotalMeFile()
    {
        File.Create(Application.persistentDataPath + "/TotalMeData.json").Close();
    }

    IEnumerator moveProgress(float destination, string next)
    {
        while (loadingBar.value < destination)
        {
            loadingBar.value = Mathf.Lerp(loadingBar.value, destination + 0.5f, 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log(loadingBar.value + "    " + destination);

        if (next == "shop")
        {
            StartCoroutine(loadShopData());
        }
        else if (next == "tea")
        {
            StartCoroutine(loadTeaData());
        }
        else if (next == "leader")
        {
            StartCoroutine(loadLeaderData());
        }
        else
        {
            StartCoroutine(loadStartScene());
        }
        yield return null;
    }

}
