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

    public void postTotal()
    {
        StartCoroutine(PostLeader());
    }

    IEnumerator PostLeader()
    {
        UnityWebRequest request = new UnityWebRequest();
        nickname = GameStaticData.data.name;
        Leader leader;

        int clothes_cnt = ShopDataScript.sd.item.Length;
        int total = GameStaticData.data.data_money;

        for (int i = 0; i < clothes_cnt; i++)
        {
            if (ShopDataScript.sd.item[i].own) //해당 옷 세트를 가지고 있다면
            {
                total += ShopDataScript.sd.item[i].item_cost;
            }
        }

        leader = new Leader
        {
            username = nickname,
            score = total
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
        Invoke("postWait", 1f);
    }

    void postWait()
    {
        if (GameStaticData.data.data_money > 0)
        {
            postTotal();
        }
    }
}
