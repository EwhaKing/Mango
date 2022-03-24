﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class GetLeaderData : MonoBehaviour
{
    public TextMeshProUGUI text_rank;
    public GameObject boardScreen;
    public GameObject boardBackground;
    public GameObject buttonDay, buttonTotal;
    public GameObject _user;
    public List<GameObject> _users = new List<GameObject>();
    public int totaluser = 0; //나중에 데이터베이스에서 가져올 총 유저 수
    
    string url_leader_score = "https://mango-love.herokuapp.com/api/leaders/score";
    string url_leader_total = "https://mango-love.herokuapp.com/api/leaders/total";
    string url_leader_score_me = "https://mango-love.herokuapp.com/api/leaders/score/me";
    string url_leader_total_me = "https://mango-love.herokuapp.com/api/leaders/total/me";

    string leaderBoard;
    string leaderMe;
    bool isTotalScore;
    Data leader;
    me leader_me;

    [System.Serializable]
    class Data
    {
        public item[] item;
    }

    [System.Serializable]
    class item
    {
        public string leaderNum;
        public string user;
        public string score;
    }

    [System.Serializable]
    class me
    {
        public string leaderNum;
        public string rank;
        public string user;
        public string score;
    }

    public class User
    {
        public string username;

    }

    public void getScore()
    {
        isTotalScore = false;
        StartCoroutine(GetLeader(leaderBoard, isTotalScore));

        //버튼 색 조정
        buttonDay.GetComponent<Image>().color = new Color(1f, 217f / 255f, 102f / 255f, 1f);
        buttonTotal.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f); //하얀색
    }

    public void getTotal()
    {
        isTotalScore = true;
        StartCoroutine(GetLeader(leaderBoard, isTotalScore));

        //버튼 색 조정
        buttonTotal.GetComponent<Image>().color = new Color(1f, 217f / 255f, 102f / 255f, 1f);
        buttonDay.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f); //하얀색
    }

    IEnumerator GetLeader(string leaderBoard, bool isTotal)
    {
        UnityWebRequest request = new UnityWebRequest();
        UnityWebRequest request_me = new UnityWebRequest();
        string username = GameStaticData.data.name;
        User user = new User
        {
            username = username
        };
        string json = JsonUtility.ToJson(user);

        if (isTotal == false)
        {
            request = UnityWebRequest.Get(url_leader_score);
            request_me = UnityWebRequest.Post(url_leader_score_me, json);
        }
        else
        {
            request = UnityWebRequest.Get(url_leader_total);
            request_me = UnityWebRequest.Post(url_leader_total_me, json);
        }
        yield return request.SendWebRequest();

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request_me.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request_me.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request_me.SetRequestHeader("Content-Type", "application/json");
        yield return request_me.SendWebRequest();


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

        if (request_me.isNetworkError || request_me.isHttpError)
        {
            Debug.Log(request_me.error);
        }
        else
        {
            leaderMe = request_me.downloadHandler.text;
            Debug.Log(leaderMe);
            leader_me = JsonUtility.FromJson<me>(leaderMe);
            Debug.Log(leader_me.user);
        }
        StartCoroutine(addUser());
        yield return null;
    }

    IEnumerator addUser()
    {
        if (isTotalScore == false) text_rank.text = "하루 랭킹";
        else text_rank.text = "전체 랭킹";

        if ( _users.Count > 1 && leader.item.Length < _users.Count)
        {
            int rank = 1;
            if (leader_me.rank == "0")
            {
                leader_me.rank = "-";
                leader_me.score = "-";
            }
            _users[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = leader_me.rank;
            _users[1].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader_me.user;
            _users[1].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = leader_me.score;
            Color color1 = _users[1].transform.GetChild(0).GetComponent<Image>().color; color1.a = 1.0f;
            _users[1].transform.GetChild(0).GetComponent<Image>().color = color1;

            int i;
            for (i = 2; i <= leader.item.Length + 1; i++)
            {
                if(i > 2)
                {
                    if (leader.item[i - 3].score != leader.item[i - 2].score) rank = i-1;
                }
                _users[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (rank).ToString();
                _users[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader.item[i - 2].user;
                _users[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = leader.item[i - 2].score;
                Color color = _users[i].transform.GetChild(0).GetComponent<Image>().color; color.a = 0.0f;
                _users[i].transform.GetChild(0).GetComponent<Image>().color = color;
            }
            for (int j = _users.Count-1; j >= i; j--) {
                Destroy(_users[j]);
                _users.Remove(_users[j]);
            }
            Debug.Log("컴포넌트수: " + _users.Count + "    리스트수: " + leader.item.Length);
        }
        else if (_users.Count > 1 && leader.item.Length >= _users.Count)
        {
            int rank = 1;
            if (leader_me.rank == "0")
            {
                leader_me.rank = "-";
                leader_me.score = "-";
            }
            _users[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = leader_me.rank;
            _users[1].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader_me.user;
            _users[1].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = leader_me.score;
            Color color1 = _users[1].transform.GetChild(0).GetComponent<Image>().color; color1.a = 1.0f;
            _users[1].transform.GetChild(0).GetComponent<Image>().color = color1;

            int i;
            for (i = 2; i <= _users.Count; i++)
            {
                if(i > 2)
                {
                    if (leader.item[i - 3].score != leader.item[i - 2].score) rank = i-1;
                }
                _users[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (rank).ToString();
                _users[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader.item[i - 2].user;
                _users[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = leader.item[i - 2].score;
                Color color = _users[i].transform.GetChild(0).GetComponent<Image>().color; color.a = 0.0f;
                _users[i].transform.GetChild(0).GetComponent<Image>().color = color;
            }
            for (int j = i; j <= leader.item.Length + 1; j++)
            {
                GameObject user = GameObject.Instantiate(_user) as GameObject;
                user.name = "user" + (j + 1).ToString();
                user.transform.SetParent(_user.transform.parent);
                user.transform.localScale = Vector3.one;
                user.transform.localRotation = Quaternion.identity;

                if (leader.item[j - 3].score != leader.item[j - 2].score) rank = j-1;
                user.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (rank).ToString(); //등수
                user.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader.item[j - 2].user; //데이터베이스에서 가져올 이름
                user.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = leader.item[j - 2].score; //데이터베이스에서 가져올 돈
                Color color = _users[j].transform.GetChild(0).GetComponent<Image>().color; color.a = 0.0f;
                _users[j].transform.GetChild(0).GetComponent<Image>().color = color;

                _users.Add(user);
            }
            Debug.Log("컴포넌트수: " + _users.Count + "    리스트수: " + leader.item.Length);
        }
        else if (_users.Count <= 1)
        {
            int rank = 1;
            GameObject userme = GameObject.Instantiate(_user) as GameObject;
            userme.name = "user" + (2).ToString();
            userme.transform.SetParent(_user.transform.parent);
            userme.transform.localScale = Vector3.one;
            userme.transform.localRotation = Quaternion.identity;
            
            if (leader_me.rank == "0")
            {
                leader_me.rank = "-";
                leader_me.score = "-";
            }
            userme.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = leader_me.rank;
            userme.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader_me.user;
            userme.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = leader_me.score;
            Color color1 = userme.transform.GetChild(0).GetComponent<Image>().color; color1.a = 1.0f;
            userme.transform.GetChild(0).GetComponent<Image>().color = color1;
            _users.Add(userme);


            for (int i = 2; i<= leader.item.Length + 1; i++)
            {
                GameObject user = GameObject.Instantiate(_user) as GameObject;
                user.name = "user" + (i + 1).ToString();
                user.transform.SetParent(_user.transform.parent);
                user.transform.localScale = Vector3.one;
                user.transform.localRotation = Quaternion.identity;

                if( i > 2)
                {
                    if (leader.item[i - 3].score != leader.item[i - 2].score) rank = i-1;
                }
                user.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (rank).ToString(); //등수
                user.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader.item[i - 2].user; //데이터베이스에서 가져올 이름
                user.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = leader.item[i - 2].score; //데이터베이스에서 가져올 돈

                _users.Add(user);
            }
        }
        yield return null;
    }

    IEnumerator clearUser(bool isTotal)
    {
        if(_users.Count > 1)
        {
            for (int i = 1; i < _users.Count; i++)
            {
                Destroy(_users[i]);
            }
        }
        StartCoroutine(GetLeader(leaderBoard, isTotal));
        yield return null;
    }

    void Start()
    {
        boardBackground.SetActive(false);
        boardScreen.SetActive(false);
        Vector3 startPos = _user.transform.localPosition;
        Debug.Log("clearStart");
        _users.Clear();
        _users.Add(_user);

        _user.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "순위";
        _user.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "이름"; //데이터베이스에서 가져올 이름
        _user.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "돈"; //데이터베이스에서 가져올 돈
        Color color = _user.transform.GetChild(0).GetComponent<Image>().color; color.a = 0.0f;
        _user.transform.GetChild(0).GetComponent<Image>().color = color;

        totaluser = 32;
        getScore();

    }

    public void onClickScoreBoard()
    {
        boardBackground.SetActive(true);
        boardScreen.SetActive(true);

        //하루 버튼 노란색으로 시작
        buttonDay.GetComponent<Image>().color = new Color(1f, 217f / 255f, 102f / 255f, 1f);
    }

    public void onClickExit()
    {
        boardBackground.SetActive(false);
        boardScreen.SetActive(false);
    }
}
