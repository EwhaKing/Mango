using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomerManager : MonoBehaviour
{
    public static int customer = 0; //누적 손님수
    public static bool check = false; //미니게임 씬 진행하고 왔는지 체크하는 변수
    public int current_customer = 0; //현재 손님 인덱스
    public GameObject[] customer_obj = new GameObject[3]; //손님 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        if(check)
        {
            check = false;
            //해당 손님 인덱스 말풍선 건네기로 바뀌게
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickCustomerTalk()
    {
        check = true;
        customer++;
        current_customer = this.gameObject.transform.GetSiblingIndex();
        SceneManager.LoadScene("minigameSceneFinish");

    }
}
