using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            Debug.Log("게임 첫번째 실행 x");
            next_scene = "StartScene";
        }
        StartCoroutine(loadGameData());
        yield return null;
    }

    IEnumerator loadGameData()
    {
        Debug.LogWarning("게임데이터");
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
        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.33f, 1f);
        StartCoroutine(loadLeaderData());
        yield return null;
    }

    IEnumerator loadLeaderData()
    {
        Debug.LogWarning("리더데이터");
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

        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.66f, 1f);
        StartCoroutine(loadStartScene());
        yield return null;
    }

    IEnumerator loadStartScene()
    {
        Debug.LogWarning("스타트씬");
        yield return null; 
        AsyncOperation op = SceneManager.LoadSceneAsync(next_scene); 
        op.allowSceneActivation = false; 
        float timer = 1f; 
        while (!op.isDone) { 
            yield return null; 
            if (op.progress < 0.9f) 
            {
                loadingBar.value += Time.deltaTime * 0.3f * timer;
                if (loadingBar.value >= op.progress) 
                { 
                    timer = 0f; 
                }
                else
                {
                    timer = 1f;
                }

            } 
            else 
            {
                loadingBar.value += Time.deltaTime * 0.3f * timer;
                if (loadingBar.value == 1.0f) 
                { 
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

}
