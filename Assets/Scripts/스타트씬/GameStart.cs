using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameStart : MonoBehaviour
{
    public int totaluser = 0; //나중에 데이터베이스에서 가져올 총 유저 수
    string url = "https://mango-love.herokuapp.com/api/leaders/score";
    string leaderBoard;
    Data leader;

    // Start is called before the first frame update
    void Awake()
    {
        get();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void get()
    {
        StartCoroutine(GetLeader(leaderBoard));
    }

    IEnumerator GetLeader(string leaderBoard)
    {
        UnityWebRequest request = new UnityWebRequest();
        request = UnityWebRequest.Get(url);
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
    }

    public void OnClickGameRealStart()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        Time.timeScale = 1;
        SceneManager.LoadScene("maingameScene");

        //Debug.Log("버튼 작동 잘 되니?");
    }
}
