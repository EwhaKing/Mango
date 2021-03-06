using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ClockMove : MonoBehaviour
{
    public GameObject clock_hand;
    public GameObject pop_up;
    public float time = 0;
    bool check = true;
    float limit_time = 30; //1당 1초
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
        //GameObject.Find("GameData").GetComponent<GameStaticData>().mainscene_time = time;
        
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        if (check && time < limit_time) //우선 10초, 원래 3분
        {
            time += Time.deltaTime;
            clock_hand.transform.localEulerAngles = new Vector3(clock_hand.transform.localEulerAngles.x,
                                                            clock_hand.transform.localEulerAngles.y,
                                                            - (360f / limit_time) * time);
            //GameObject.Find("GameData").GetComponent<GameStaticData>().clock_hand_rot = clock_hand.transform.localEulerAngles.z;

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

        //처음 종료 && 건네는 애니메이션 다 끝남 && 건네기가 남아있지 않음
        else if (check && CustomerTalkButton.main_over_check && !CustomerManager.check) //타임오버
        {
            //게임 머니에 한 번만 더하기 위한 체크변수
            check = false; 

            //초기화
            GameObject.Find("GameData").GetComponent<GameStaticData>().mainscene_time = 0;
            GameObject.Find("GameData").GetComponent<GameStaticData>().clock_hand_rot = 0;
            time = 0;

            //메인게임 중지 , 손님 새로 오는 거 중지
            GameObject.Find("GameManager").GetComponent<CustomerManager>().enabled = false;

            //오늘 번 돈 정산 팝업창 
            pop_up.SetActive(true);
            ButtonSound._buttonInstance.onMoneyAudio(); //돈 효과음 재생
            int todayDiff = GameStaticData.data.difficulty; //오늘 난이도 저장
            GameStaticData.data.data_money += TotalMoney.totalMoney;
            GameStaticData.data.data_money_total += TotalMoney.totalMoney;
            GameStaticData.data.date++;
            GameStaticData.data.difficulty = nextDiff(); //다음날 난이도
            File.WriteAllText(Application.persistentDataPath + "/GameData.json", JsonUtility.ToJson(GameStaticData.data));
            Debug.Log("다음 난이도: " + GameStaticData.data.difficulty);
            pop_up.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = diffMent(todayDiff, GameStaticData.data.difficulty);
        }
    }

    int nextDiff()
    {
        int difficulty = GameStaticData.data.difficulty;
        if (TotalMoney.totalMoney < 1000) difficulty = findDiff(0);
        else if (TotalMoney.totalMoney >= 2000) difficulty = findDiff(2);
        else difficulty = findDiff(1);

        return difficulty;
    }

    int findDiff(int diff)
    {
        int[] level = { 0, 0, 0, 0, 0 };
        int num = Random.Range(1, 100 + 1);
        int currLv = GameStaticData.data.difficulty;
        Debug.Log("랜덤값: " + num);

        if(diff == 0)
        {
            if (currLv == 1) level[0] = 100;
            else
            {
                int percent = 100 / (currLv - 1);
                for (int i = 0; i < currLv - 1; i++)
                {
                    level[i] = percent;
                }
            }
        }
        else if(diff == 1)
        {
            int percent;
            if (currLv == 1)
            {
                level[0] = level[1] = 50;
            }
            else if (currLv == 5)
            {
                level[3] = level[4] = 50;
            }
            else
            {
                percent = 33;
                for (int i = currLv - 2; i <= currLv; i++)
                {
                    level[i] = percent;
                }
            }
        }
        else
        {
            if (currLv == 5) level[4] = 100;
            else
            {
                int percent = 100 / (4 - (currLv - 1));
                for (int i = 4; i > currLv - 1; i--)
                {
                    level[i] = percent;
                }
            }  
        }

        if (num <= level[0]) return 1;
        else if (num <= level[0] + level[1]) return 2;
        else if (num <= level[0] + level[1] + level[2]) return 3;
        else if (num <= level[0] + level[1] + level[2] + level[3]) return 4;
        else return 5;

    }

    string diffMent(int prev_diff, int next_diff)
    {
        string ans = "오늘 수익이 들어왔네요!";

        if (prev_diff < next_diff)
        {
            ans += "<br>다음 날은 장사가 힘들 수도 있겠어요...";
        }
        else if(prev_diff > next_diff)
        {
            ans += "<br>다음 날은 왠지 일이 잘 풀릴 것 같아요!";
        }

        return ans;
    }
}
