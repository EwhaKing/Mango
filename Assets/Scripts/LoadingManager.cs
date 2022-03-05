using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static string next_scene;
    public int totaluser = 0;
    string url_leader_score = "https://mango-love.herokuapp.com/api/leaders/score";
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
        StartCoroutine(defineNextScene());
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
                next_scene = "usernameScene";
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
        progress_text.text = "아기정보를 불러오고 있습니다...";
        DontDestroyOnLoad(GameObject.Find("GameData"));

        if (File.Exists(Application.persistentDataPath + "/GameData.json"))
        {
            Debug.LogWarning("게임파일있음");
            LoadGameData();
        }
        else
        {
            Debug.LogWarning("게임파일없음");
            CreateFile();
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
        progress_text.text = "서버에 접속하고 있습니다(최대 30초 소요가능)...";
        UnityWebRequest request = new UnityWebRequest();
        request = UnityWebRequest.Get(url_leader_score);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            leaderBoard = request.downloadHandler.text;
            Debug.Log(leaderBoard);
            leader = JsonUtility.FromJson<Data>("{\"item\":" + leaderBoard + "}");
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
        GameStaticData.data = JsonUtility.FromJson<GameData>("{\"data_money\": 0,\"data_cloth\": 0,\"date\" :  0,\"name\" : \"\"}");
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));
    }

    public void LoadGameData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/GameData.json");
        GameStaticData.data = JsonUtility.FromJson<GameData>(str);

        Debug.Log("불러온 데이터" + str);
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
        ShopDataScript.sd = JsonUtility.FromJson<ShopData>("{\"item\":[{\"item_num\":0,\"item_name\":\"기본옷\", \"item_cost\":0,\"own\":true,\"is_read\":false},{\"item_num\":1,\"item_name\":\"꽃무늬옷\", \"item_cost\":20000,\"own\": false,\"is_read\":false},{\"item_num\":2,\"item_name\":\"트레이닝복\", \"item_cost\":25000,\"own\":false,\"is_read\":false},{\"item_num\":3,\"item_name\":\"유치원복\", \"item_cost\":30000,\"own\":false,\"is_read\":false},{\"item_num\":4,\"item_name\":\"멜빵세트\", \"item_cost\":30000,\"own\":false,\"is_read\":false},{\"item_num\":5,\"item_name\":\"피크닉세트\", \"item_cost\":35000,\"own\":false,\"is_read\":false},{\"item_num\":6,\"item_name\":\"탐정세트\", \"item_cost\":35000,\"own\":false,\"is_read\":false},{\"item_num\":7,\"item_name\":\"꿀벌잠옷\", \"item_cost\":40000,\"own\":false,\"is_read\":false},{\"item_num\":8,\"item_name\":\"곰돌이잠옷\", \"item_cost\":40000,\"own\":false,\"is_read\":false},{\"item_num\":9,\"item_name\":\"한복 두루마기\", \"item_cost\":50000,\"own\":false,\"is_read\":false},{\"item_num\":10,\"item_name\":\"한복 치마\", \"item_cost\":50000,\"own\":false,\"is_read\":false}]}");
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
        TeaDataScript.teaDex.item[0] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 0, \"tea_type\": 0, \"tea_name\": \"형아를 위한 차\", \"tea_description\" : \"먹자마자 나앗다는 전설의 마법차. 어마어마하다는 소문이~\", \"tea_recipe\": [{\"ingredient_num\": 0,\"ingredient_amout\": 8}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[1] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 1, \"tea_type\": 2,\"tea_name\": \"눈에서 꿀이 뚝뚝\", \"tea_description\" : \"눈에서 꿀이 뚝뚝 내 눈엔 눈물이 뚝뚝...?ㅎ\", \"tea_recipe\": [{\"ingredient_num\": 13,\"ingredient_amout\": 8}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[2] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 2, \"tea_type\": 1,\"tea_name\": \"코코파인 플리쉬\",\"tea_description\" : \"무더운 한여름에 생각나는 달콤짜릿한 그 맛! 코코넨네한 아기도 못참는다구~\",\"tea_recipe\": [{\"ingredient_num\": 8,\"ingredient_amout\": 4}, {\"ingredient_num\": 5,\"ingredient_amout\": 4}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[3] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 3, \"tea_type\": 1,\"tea_name\": \"딸바보 주스\",\"tea_description\" : \"당신 혹시 이 주스를 매일 사가지 않습니까..? 당신은 이미 딸바보입니다..\",\"tea_recipe\": [{\"ingredient_num\": 2,\"ingredient_amout\": 2}, {\"ingredient_num\": 3,\"ingredient_amout\": 2}, {\"ingredient_num\": 15,\"ingredient_amout\": 4}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[4] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 4, \"tea_type\": 1,\"tea_name\": \"허니만을 위한 밀크티❤\",\"tea_description\" : \"ONLY ❤ YOU\",\"tea_recipe\": [{\"ingredient_num\": 13,\"ingredient_amout\": 2}, {\"ingredient_num\": 10,\"ingredient_amout\": 3}, {\"ingredient_num\": 15,\"ingredient_amout\": 3}],\"own\": false, \"is_read\": false}");

        TeaDataScript.teaDex.item[5] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 5, \"tea_type\": 0,\"tea_name\": \"레몬디베트\",\"tea_description\" : \"신맛 쓴맛 단맛 세가지맛을 동시에 즐길 수 있는 삼미자차\",\"tea_recipe\": [{\"ingredient_num\": 11,\"ingredient_amout\": 3}, {\"ingredient_num\": 14,\"ingredient_amout\": 3}, {\"ingredient_num\": 15,\"ingredient_amout\": 2}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[6] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 6, \"tea_type\": 1,\"tea_name\": \"레몬아...나 자몽다...\",\"tea_description\" : \"신맛 쓴맛 단맛 세가지맛을 동시에 즐길 수 있는 삼미자차\",\"tea_recipe\": [{\"ingredient_num\": 3,\"ingredient_amout\": 4}, {\"ingredient_num\": 0,\"ingredient_amout\": 2}, {\"ingredient_num\": 1,\"ingredient_amout\": 2}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[7] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 7, \"tea_type\": 1,\"tea_name\": \"올때 메론라떼\",\"tea_description\" : \"형아! 올때 메론라떼~~~\",\"tea_recipe\": [{\"ingredient_num\": 9,\"ingredient_amout\": 4}, {\"ingredient_num\": 15,\"ingredient_amout\": 6}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[8] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 8, \"tea_type\": 0,\"tea_name\": \"궁금해 허니~\",\"tea_description\" : \"자니? 자는구나... 뭐해?\",\"tea_recipe\": [{\"ingredient_num\": 10,\"ingredient_amout\": 5}, {\"ingredient_num\": 13,\"ingredient_amout\": 5}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[9] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 9, \"tea_type\": 1,\"tea_name\": \"민트 폴라디츠\",\"tea_description\" : \"근본라떼입니다. 반박 안받음.\",\"tea_recipe\": [{\"ingredient_num\": 11,\"ingredient_amout\": 2}, {\"ingredient_num\": 14,\"ingredient_amout\": 3}, {\"ingredient_num\": 15,\"ingredient_amout\": 5}],\"own\": false, \"is_read\": false}");

        TeaDataScript.teaDex.item[10] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 10, \"tea_type\": 1,\"tea_name\": \"언덕위의 트로피컬\",\"tea_description\" : \"언덕 위에서 트로피컬 샤워\",\"tea_recipe\": [{\"ingredient_num\": 4,\"ingredient_amout\": 3}, {\"ingredient_num\": 5,\"ingredient_amout\": 3}, {\"ingredient_num\": 6,\"ingredient_amout\": 4}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[11] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 11, \"tea_type\": 0,\"tea_name\": \"디벳 민트티\",\"tea_description\" : \"근본 + 근본 = 근본\",\"tea_recipe\": [{\"ingredient_num\": 12,\"ingredient_amout\": 4}, {\"ingredient_num\": 11,\"ingredient_amout\": 2}, {\"ingredient_num\": 14,\"ingredient_amout\": 4}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[12] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 12, \"tea_type\": 1,\"tea_name\": \"신호등 주스\",\"tea_description\" : \"삐빅- 딸기불입니다. 드시고가세요.\",\"tea_recipe\": [{\"ingredient_num\": 7,\"ingredient_amout\": 4}, {\"ingredient_num\": 2,\"ingredient_amout\": 3}, {\"ingredient_num\": 3,\"ingredient_amout\": 3}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[13] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 13, \"tea_type\": 2,\"tea_name\": \"봄사랑 벚꽃차\",\"tea_description\" : \"봄보다 멀고 벚꽃보다 가까운 사랑\",\"tea_recipe\": [{\"ingredient_num\": 19,\"ingredient_amout\": 8}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[14] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 14, \"tea_type\": 2,\"tea_name\": \"쑥쑥라떼\",\"tea_description\" : \"우유를 먹어야 쑥쑥 자라지~ 거기에 쑥을 더한\",\"tea_recipe\": [{\"ingredient_num\": 16,\"ingredient_amout\": 4}, {\"ingredient_num\": 15,\"ingredient_amout\": 8}],\"own\": false, \"is_read\": false}");

        TeaDataScript.teaDex.item[15] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 15, \"tea_type\": 2,\"tea_name\": \"감기 뚝 마늘차\",\"tea_description\" : \"뚝! 이거 먹으면 감기 뚝! 감기 안나으면 계속 먹어야할걸? ^^\",\"tea_recipe\": [{\"ingredient_num\": 17,\"ingredient_amout\": 4}, {\"ingredient_num\": 13,\"ingredient_amout\": 8}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[16] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 16, \"tea_type\": 2,\"tea_name\": \"PROTEIN\",\"tea_description\" : \"울끈불끈\",\"tea_recipe\": [{\"ingredient_num\": 18,\"ingredient_amout\": 9}, {\"ingredient_num\": 3,\"ingredient_amout\": 5}],\"own\": false, \"is_read\": false}");
        TeaDataScript.teaDex.item[17] = JsonUtility.FromJson<TeaData>("{\"tea_num\": 17, \"tea_type\": 1,\"tea_name\": \"아프디망고!\",\"tea_description\" : \"세상 모든 사람들 이제 더 이상 아프디망고!\",\"tea_recipe\": [{\"ingredient_num\": 20,\"ingredient_amout\": 18}],\"own\": false, \"is_read\": false}");

        File.WriteAllText(Application.persistentDataPath + "/TeaDex.json", JsonUtility.ToJson(TeaDataScript.teaDex));
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
