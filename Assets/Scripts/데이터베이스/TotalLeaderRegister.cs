using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TotalLeaderRegister : MonoBehaviour
{
    string url_leader_register = "https://mango-love.herokuapp.com/api/leaders/total/registration";


    public string nickname;
    public class Leader
    {
        public string username;
        public int score;

    }

    void Start()
    {
        if (GameStaticData.data.data_money_total > 0) postTotal();
    }

    public void postTotal()
    {
        StartCoroutine(PostLeader());
    }

    IEnumerator PostLeader()
    {
        UnityWebRequest request = new UnityWebRequest();
        Leader leader;

        leader = new Leader
        {
            username = GameStaticData.data.name,
            score = GameStaticData.data.data_money_total
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
}
