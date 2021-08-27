using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockMove : MonoBehaviour
{
    GameObject clock_hand;
    public GameObject pop_up;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        //미니 게임 갖다 온 후 이어지는 시간
        time = GameObject.Find("GameData").GetComponent<GameStaticData>().mainscene_time;

        //미니 게임 갖다 온 후 초기 위치 설정
        clock_hand = this.gameObject;
        float clock_hand_z = GameObject.Find("GameData").GetComponent<GameStaticData>().clock_hand_rot;
        clock_hand.transform.localEulerAngles = new Vector3(clock_hand.transform.localEulerAngles.x,
                                                            clock_hand.transform.localEulerAngles.y,
                                                            clock_hand_z);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        GameObject.Find("GameData").GetComponent<GameStaticData>().mainscene_time = time;

        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        if (time < 180)
        {
            clock_hand.transform.localEulerAngles = new Vector3(clock_hand.transform.localEulerAngles.x,
                                                            clock_hand.transform.localEulerAngles.y,
                                                            clock_hand.transform.localEulerAngles.z - 0.0055f);
            GameObject.Find("GameData").GetComponent<GameStaticData>().clock_hand_rot = clock_hand.transform.localEulerAngles.z;
        }

        else
        {
            //초기화
            GameObject.Find("GameData").GetComponent<GameStaticData>().mainscene_time = 0;
            GameObject.Find("GameData").GetComponent<GameStaticData>().clock_hand_rot = 0; 

            pop_up.SetActive(true);
            GameObject.Find("GameData").GetComponent<GameStaticData>().game_money += TotalMoney.totalMoney;
            Debug.Log("게임 머니: " + GameObject.Find("GameData").GetComponent<GameStaticData>().game_money);
        }
    }
}
