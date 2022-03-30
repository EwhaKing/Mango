using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
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
    public bool isScoreGetted;
    public bool isTotalGetted;

    DateTime curr_time;
    DateTime prev_renewal_time;

    string url_leader_score = "https://mango-love.herokuapp.com/api/leaders/score";
    string url_leader_total = "https://mango-love.herokuapp.com/api/leaders/total";
    string url_leader_score_me = "https://mango-love.herokuapp.com/api/leaders/score/me";
    string url_leader_total_me = "https://mango-love.herokuapp.com/api/leaders/total/me";
    string url_time = "https://mango-love.herokuapp.com/api/time";

    string leaderBoard;
    string leaderMe;
    bool isTotalScore;

    Data leader_score;
    Data leader_total;
    me leader_me_score;
    me leader_me_total;

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
        Debug.Log("하루랭킹 구하기 시작");
        isTotalScore = false;
        if (isScoreGetted == false) StartCoroutine(GetLeader(leaderBoard, isTotalScore));
        else
        {
            LoadScoreData();
            LoadScoreMeData();
            StartCoroutine(addUser());
        }

        //버튼 색 조정
        buttonDay.GetComponent<Image>().color = new Color(1f, 217f / 255f, 102f / 255f, 1f);
        buttonTotal.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f); //하얀색
    }

    public void getTotal()
    {
        isTotalScore = true;
        if (isTotalGetted == false) StartCoroutine(GetLeader(leaderBoard, isTotalScore));
        else
        {
            LoadTotalData();
            LoadTotalMeData();
            StartCoroutine(addUser());
        }

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

        //전체
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            leaderBoard = request.downloadHandler.text;
            if(isTotal == false)
            {
                leader_score = JsonUtility.FromJson<Data>("{\"item\":" + leaderBoard + "}");
                SaveScoreData();
                getJustTime();
                GameStaticData.data.score_viewed_time = curr_time.ToString("yyyy-MM-dd HH:mm:ss");
                File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));
                isScoreGetted = true;
            }
            else
            {
                leader_total = JsonUtility.FromJson<Data>("{\"item\":" + leaderBoard + "}");
                SaveTotalData();
                getJustTime();
                GameStaticData.data.total_viewed_time = curr_time.ToString("yyyy-MM-dd HH:mm:ss");
                File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));
                isTotalGetted = true;
            }
        }

        //나
        if (request_me.isNetworkError || request_me.isHttpError)
        {
            Debug.Log(request_me.error);
        }
        else
        {
            leaderMe = request_me.downloadHandler.text;
            Debug.Log(leaderMe);
            if(isTotal == false)
            {
                leader_me_score = JsonUtility.FromJson<me>(leaderMe);
                SaveScoreMeData();
            }
            else
            {
                leader_me_total = JsonUtility.FromJson<me>(leaderMe);
                SaveTotalMeData();
            }
        }
        StartCoroutine(addUser());
        yield return null;
    }

    IEnumerator addUser()
    {
        Data leader;
        me leader_me;
        if (isTotalScore == false)
        {
            text_rank.text = "하루 랭킹";
            leader = leader_score;
            leader_me = leader_me_score;
        }
        else
        {
            text_rank.text = "전체 랭킹";
            leader = leader_total;
            leader_me = leader_me_total;
        }

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
    }

    public void onClickScoreBoard()
    {
        boardBackground.SetActive(true);
        boardScreen.SetActive(true);
        DateTime score_viewed_time = DateTime.ParseExact(GameStaticData.data.score_viewed_time, "yyyy-MM-dd HH:mm:ss", null);
        DateTime total_viewed_time = DateTime.ParseExact(GameStaticData.data.total_viewed_time, "yyyy-MM-dd HH:mm:ss", null);
        Debug.Log("하루본시간" + score_viewed_time + "    전체본시간" + total_viewed_time);

        StartCoroutine(getTime(score_viewed_time, total_viewed_time, true));

        //하루 버튼 노란색으로 시작
        buttonDay.GetComponent<Image>().color = new Color(1f, 217f / 255f, 102f / 255f, 1f);
    }

    public void onClickExit()
    {
        boardBackground.SetActive(false);
        boardScreen.SetActive(false);
    }

    IEnumerator getTime(DateTime score_viewed_time, DateTime total_viewed_time, bool compare)
    {
        UnityWebRequest request = new UnityWebRequest();
        string currTime = "";

        request = UnityWebRequest.Get(url_time);
        yield return request.SendWebRequest();

        //전체
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            currTime = request.downloadHandler.text;
            Debug.Log("스프링시간" + currTime);
            curr_time = DateTime.ParseExact(currTime, "yyyy-MM-dd HH:mm:ss", null);
            if (curr_time.Minute >= 0 && curr_time.Minute < 30) prev_renewal_time = new DateTime(curr_time.Year, curr_time.Month, curr_time.Day, curr_time.Hour, 00, 00);
            else prev_renewal_time = new DateTime(curr_time.Year, curr_time.Month, curr_time.Day, curr_time.Hour, 30, 00);
        }
        if(compare) StartCoroutine(compareTime(score_viewed_time, total_viewed_time));

    }

    IEnumerator compareTime(DateTime score_viewed_time, DateTime total_viewed_time)
    {
        int result1 = DateTime.Compare(score_viewed_time, prev_renewal_time);
        int result2 = DateTime.Compare(total_viewed_time, prev_renewal_time);
        Debug.Log(result1 + "    " + result2);
        if (result1 >= 0) isScoreGetted = true;
        else
        {
            isScoreGetted = false;
            Debug.Log("하루갱신시작");
        }

        if (result2 >= 0) isTotalGetted = true;
        else
        {
            isTotalGetted = false;
            Debug.Log("전체갱신시작");
        }
        getScore();
        yield return null;
    }

    public void LoadScoreData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/ScoreData.json");
        leader_score = JsonUtility.FromJson<Data>(str);
    }

    public void SaveScoreData()
    {
        File.WriteAllText(Application.persistentDataPath + "/ScoreData.json", JsonUtility.ToJson(leader_score));
        Invoke("LoadScoreData", 0.5f);
    }

    public void LoadTotalData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/TotalData.json");
        leader_total = JsonUtility.FromJson<Data>(str);
    }

    public void SaveTotalData()
    {
        File.WriteAllText(Application.persistentDataPath + "/TotalData.json", JsonUtility.ToJson(leader_total));
        Invoke("LoadTotalData", 0.5f);
    }

    public void LoadScoreMeData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/ScoreMeData.json");
        leader_me_score = JsonUtility.FromJson<me>(str);
        Debug.Log(str);
    }

    public void SaveScoreMeData()
    {
        File.WriteAllText(Application.persistentDataPath + "/ScoreMeData.json", JsonUtility.ToJson(leader_me_score));
        Invoke("LoadScoreMeData", 0.5f);
    }

    public void LoadTotalMeData()
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/TotalMeData.json");
        leader_me_total = JsonUtility.FromJson<me>(str);
        Debug.Log(str);
    }

    public void SaveTotalMeData()
    {
        File.WriteAllText(Application.persistentDataPath + "/TotalMeData.json", JsonUtility.ToJson(leader_me_total));
        Invoke("LoadTotalMeData", 0.5f);
    }

    IEnumerator getJustTime()
    {
        UnityWebRequest request = new UnityWebRequest();
        string currTime = "";

        request = UnityWebRequest.Get(url_time);
        yield return request.SendWebRequest();

        //전체
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            currTime = request.downloadHandler.text;
            Debug.Log("스프링시간" + currTime);
            curr_time = DateTime.ParseExact(currTime, "yyyy-MM-dd HH:mm:ss", null);
        }

    }


}
