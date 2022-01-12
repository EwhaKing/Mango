using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ClockMove : MonoBehaviour
{
    GameObject clock_hand;
    public GameObject pop_up;
    float time = 0;
    bool check = true;
    float limit_time = 10;
    float fade_time = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("first");
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
        GameObject.Find("GameData").GetComponent<GameStaticData>().mainscene_time = time;

        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        if (check && time < limit_time) //우선 10초, 원래 3분
        {
            time += Time.deltaTime;
            clock_hand.transform.localEulerAngles = new Vector3(clock_hand.transform.localEulerAngles.x,
                                                            clock_hand.transform.localEulerAngles.y,
                                                            - (360f / limit_time) * time);
            GameObject.Find("GameData").GetComponent<GameStaticData>().clock_hand_rot = clock_hand.transform.localEulerAngles.z;

            //시계 빨간색으로 깜빡임
            if (time > limit_time * 0.9f)
            {
                if(fade_time < 0.5f)
                {
                    gameObject.transform.parent.gameObject.GetComponent<Image>().color = new Color(1, 1 - fade_time, 1 - fade_time, 1);
                }  
                else
                {
                    gameObject.transform.parent.gameObject.GetComponent<Image>().color = new Color(1, fade_time, fade_time, 1);
                    if (fade_time > 1f) fade_time = 0f;
                }

                fade_time += Time.deltaTime;
            }
        }

        else if (check && CustomerTalkButton.main_over_check) //타임오버
        {
            //게임 머니에 한 번만 더하기 위한 체크변수
            check = false; 

            //초기화
            GameObject.Find("GameData").GetComponent<GameStaticData>().mainscene_time = 0;
            GameObject.Find("GameData").GetComponent<GameStaticData>().clock_hand_rot = 0;
            time = 0;

            //메인게임 중지
            GameObject.Find("GameManager").SetActive(false);

            //오늘 번 돈 정산 팝업창 
            pop_up.SetActive(true);
            ButtonSound._buttonInstance.onMoneyAudio(); //돈 효과음 재생
            GameStaticData.data.data_money += TotalMoney.totalMoney;
            GameStaticData.data.date++;
            File.WriteAllText(Application.dataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));
            Debug.Log("게임 머니: " + GameStaticData.data.data_money + "    다음날: " + GameStaticData.data.date);


        }
    }
}
