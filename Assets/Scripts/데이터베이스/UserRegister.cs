using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserRegister : MonoBehaviour
{
    string url_user = "https://mango-love.herokuapp.com/api/registration";
    public Text textUsername;


    public string username;
    public void post()
    {
        StartCoroutine(PostUser());
    }

    public class User
    {
        public string username;

    }

    IEnumerator PostUser()
    {
        UnityWebRequest request = new UnityWebRequest();
        username = textUsername.text;

        User user = new User
        {
            username = username
        };

        string json = JsonUtility.ToJson(user);
        request = UnityWebRequest.Post(url_user, json);
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
            string result = request.downloadHandler.text;
            if(result == "exist")
            {
                Debug.Log("이미 있는 이름입니다. 다시 입력하세요");
            }
            else
            {
                Debug.Log("이름 등록 완료");
                PlayerPrefs.SetString("PlayerName", username);
            }
        }
    }
}
