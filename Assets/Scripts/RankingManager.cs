using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public GameObject _user;
    public List<GameObject> _users = new List<GameObject>();
    public int totaluser = 0; //나중에 데이터베이스에서 가져올 총 유저 수

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = _user.transform.localPosition;
        _users.Clear();
        _users.Add(_user);

        _user.transform.GetChild(0).GetComponent<Text>().text = "1."; 
        _user.transform.GetChild(1).GetComponent<Text>().text = "이름"; //데이터베이스에서 가져올 이름
        _user.transform.GetChild(2).GetComponent<Text>().text = "돈"; //데이터베이스에서 가져올 돈

        totaluser = 20; //우선 20으로

        for (int i = 1; i < totaluser; i++)
        {
            GameObject user = GameObject.Instantiate(_user) as GameObject;
            user.name = "user" + (i + 1).ToString();
            user.transform.SetParent(_user.transform.parent);
            user.transform.localScale = Vector3.one;
            user.transform.localRotation = Quaternion.identity;

            user.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString() + "."; //등수
            user.transform.GetChild(1).GetComponent<Text>().text = "이름"; //데이터베이스에서 가져올 이름
            user.transform.GetChild(2).GetComponent<Text>().text = Random.Range(0, 10000).ToString(); //데이터베이스에서 가져올 돈

            _users.Add(user);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
