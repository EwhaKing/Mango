using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    string url_leader = "https://mango-love.herokuapp.com/api/leaders";


    public string nickname;
    public void post()
    {
        StartCoroutine(PostLeader());
    }

    public class Leader
    {
        public string username;
        public int score;

    }

    IEnumerator PostLeader()
    {
        UnityWebRequest request = new UnityWebRequest();
        nickname = PlayerPrefs.GetString("PlayerName");

        Leader leader = new Leader
        {
            username = nickname,
            score = TotalMoney.totalMoney
        };

        string json = JsonUtility.ToJson(leader);
        request = UnityWebRequest.Post(url_leader, json);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("성공");
        }
    }
}