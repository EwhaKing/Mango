using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    string url = "http://localhost:8080/api/leaders";
    public void post()
    {
        StartCoroutine(PostUser());
    }

    public void get()
    {
        StartCoroutine(GetLeader());
    }
    public class User
    {
        public string username;
        public int score;

    }
    IEnumerator PostUser()
    {
        UnityWebRequest request = new UnityWebRequest();

        User user = new User
        {
            username = "unity",
            score = 2000
        };

        string json = JsonUtility.ToJson(user);
        request = UnityWebRequest.Post(url, json);
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

    IEnumerator GetLeader()
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
            string res = request.downloadHandler.text;
            Debug.Log(res);
        }
    }
}