using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RankingManager : MonoBehaviour
{
    public GameObject _user;
    public List<GameObject> _users = new List<GameObject>();
    public int totaluser = 0; //나중에 데이터베이스에서 가져올 총 유저 수
    string url = "https://mango-love.herokuapp.com/api/leaders";
    string leaderBoard;
    Data d;
    // Start is called before the first frame update

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
            d = JsonUtility.FromJson<Data>("{\"item\":" + leaderBoard + "}");
        }
    }

    public void addUser()
    {
        for (int i = 1; i < d.item.Length+1; i++)
        {
            GameObject user = GameObject.Instantiate(_user) as GameObject;
            user.name = "user" + (i + 1).ToString();
            user.transform.SetParent(_user.transform.parent);
            user.transform.localScale = Vector3.one;
            user.transform.localRotation = Quaternion.identity;

            user.transform.GetChild(0).GetComponent<Text>().text = (i).ToString() + "."; //등수
            user.transform.GetChild(1).GetComponent<Text>().text = d.item[i-1].username; //데이터베이스에서 가져올 이름
            user.transform.GetChild(2).GetComponent<Text>().text = d.item[i-1].score; //데이터베이스에서 가져올 돈

            _users.Add(user);
        }
    }
    void Start()
    {
        Vector3 startPos = _user.transform.localPosition;
        _users.Clear();
        _users.Add(_user);

        _user.transform.GetChild(0).GetComponent<Text>().text = "  "; 
        _user.transform.GetChild(1).GetComponent<Text>().text = "이름"; //데이터베이스에서 가져올 이름
        _user.transform.GetChild(2).GetComponent<Text>().text = "돈"; //데이터베이스에서 가져올 돈

        totaluser = 11; //우선 10으로
        Invoke("get", 1f);
        Invoke("addUser", 3f);

    }

    void Update()
    {
        
    }
}
