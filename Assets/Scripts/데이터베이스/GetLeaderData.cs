using System.Collections;
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
    string leaderBoard;
    bool isTotalScore;
    Data leader;

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
        if(isTotal == false)
        {
            request = UnityWebRequest.Get(url_leader_score);
        }
        else
        {
            request = UnityWebRequest.Get(url_leader_total);
        }
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
        StartCoroutine(addUser());
        yield return null;
    }

    IEnumerator addUser()
    {
        if (isTotalScore == false) text_rank.text = "하루 랭킹";
        else text_rank.text = "전체 랭킹";

        if ( _users.Count > 1 && leader.item.Length < _users.Count)
        {
            int i;
            for (i = 1; i < leader.item.Length + 1; i++)
            {
                _users[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i).ToString() + ".";
                _users[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = leader.item[i - 1].user;
                _users[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader.item[i - 1].score;
            }
            for (int j = _users.Count-1; j >= i; j--) {
                Destroy(_users[j]);
                _users.Remove(_users[j]);
            }
            Debug.Log("컴포넌트수: " + _users.Count + "    리스트수: " + leader.item.Length);
        }
        else if (_users.Count > 1 && leader.item.Length >= _users.Count)
        {
            int i;
            for (i = 1; i < _users.Count; i++)
            {
                _users[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i).ToString() + ".";
                _users[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = leader.item[i - 1].user;
                _users[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader.item[i - 1].score;
            }
            for (int j = i; j < leader.item.Length + 1; j++)
            {
                GameObject user = GameObject.Instantiate(_user) as GameObject;
                user.name = "user" + (j + 1).ToString();
                user.transform.SetParent(_user.transform.parent);
                user.transform.localScale = Vector3.one;
                user.transform.localRotation = Quaternion.identity;

                user.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (j).ToString() + "."; //등수
                user.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = leader.item[j - 1].user; //데이터베이스에서 가져올 이름
                user.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader.item[j - 1].score; //데이터베이스에서 가져올 돈

                _users.Add(user);
            }
            Debug.Log("컴포넌트수: " + _users.Count + "    리스트수: " + leader.item.Length);
        }
        else if (_users.Count <= 1)
        {
            for(int i = 1; i< leader.item.Length + 1; i++)
            {
                GameObject user = GameObject.Instantiate(_user) as GameObject;
                user.name = "user" + (i + 1).ToString();
                user.transform.SetParent(_user.transform.parent);
                user.transform.localScale = Vector3.one;
                user.transform.localRotation = Quaternion.identity;

                user.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i).ToString() + "."; //등수
                user.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = leader.item[i - 1].user; //데이터베이스에서 가져올 이름
                user.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = leader.item[i - 1].score; //데이터베이스에서 가져올 돈

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

        _user.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "  ";
        _user.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "이름"; //데이터베이스에서 가져올 이름
        _user.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "돈"; //데이터베이스에서 가져올 돈

        totaluser = 31; //우선 10으로
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
