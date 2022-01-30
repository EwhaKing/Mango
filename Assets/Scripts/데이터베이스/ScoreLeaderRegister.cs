using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoreLeaderRegister : MonoBehaviour
{
    string url_leader_register = "https://mango-love.herokuapp.com/api/leaders/score/registration";


    public string nickname;
    public class Leader
    {
        public string username;
        public int score;

    }

    public void postScore()
    {
        StartCoroutine(PostLeader());
    }


    IEnumerator PostLeader()
    {
        UnityWebRequest request = new UnityWebRequest();
        nickname = GameStaticData.data.name;
        Leader leader;

        leader = new Leader
        {
            username = nickname,
            score = TotalMoney.totalMoney
        };

        string json = JsonUtility.ToJson(leader);
        request = UnityWebRequest.Post(url_leader_register, json);
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

    void Start()
    {
        if(TotalMoney.totalMoney > 0) postScore();
    }
}