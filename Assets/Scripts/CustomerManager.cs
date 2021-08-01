using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomerManager : MonoBehaviour
{
    public static bool check = false; //미니게임 씬 진행하고 왔는지 체크하는 변수
    public static int current_customer = -1; //현재 손님 인덱스
    public static int[] customer_img_idx = new int[3];
    public static int[] tip_money = new int[3]; //부자 손님 팁
    public static int rich_check = 0; //부자 손님 중복 피하기 위한 변수

    public Sprite deliver; //건네기 이미지
    public Sprite exc_mark; //느낌표 이미지
    public Sprite bad_mark; //말풍선_기분나쁨 이미지
    public GameObject[] customer_obj = new GameObject[3]; //손님 오브젝트, 손님 오브젝트가 말풍선이고, 자식(0)이 손님, 자식(1)은 차 타겟 포인트

    public Sprite[] customer_img; //손님 이미지
    public GameObject tea_img; //건네는 차

    int leng; //손님 이미지 배열 길이
    float time = 0f;

    int customer_num = 0; //현재 누적 손님 

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("LifeManager_mini"))
        {
            customer_num = GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt;
        }

        leng = customer_img.Length - 1; // 길이 마지막 인덱스는 부자손님
        
        if(current_customer == -1)
        {
            /* //손님 이미지 중복 x 버전
            bool img_check = true;    
            for(int i=0;i<3;)
            {
                customer_img_idx[i] = Random.Range(0, leng);
                img_check = true;
                for(int j=i-1;j>=0;j--)
                {
                    if(customer_img_idx[i] == customer_img_idx[j])
                    {
                        img_check = false;
                        break;
                    }
                }
                if (img_check) i++;
            }
            */
            for(int i=0;i<3;i++)
            {
                customer_img_idx[i] = Random.Range(0, leng);
            }
        }

        //손님 띄우기
        customerImage();

        if (check)
        {
            if (!SliderTimer.time_over) //타임 오버가 아니라면
            {
                //해당 손님 인덱스 말풍선 건네기로 바뀌게
                customer_obj[current_customer].GetComponent<Image>().sprite = deliver;
                tea_img.SetActive(true);
                //차 타겟 해당 손님으로 지정해주기
                tea_img.GetComponent<ClickMove2>().targetPosition = customer_obj[current_customer].transform.GetChild(1).gameObject;
                //애니메이션 건네기 눌렀을 때 실행되어야 하므로 일단 컴포넌트 false로 바꿔주기
                tea_img.GetComponent<ClickMove2>().enabled = false;
            }
            else //타임 오버라면, 즉 차 만들기 실패
            {
                SliderTimer.time_over = false;

                for (int i = 0; i < 3; i++) //버튼 클릭 못하게
                {
                    customer_obj[i].GetComponent<Button>().enabled = false;
                }

                customer_obj[current_customer].GetComponent<Image>().sprite = bad_mark; //말풍선_기분나쁨으로 바꾸기

                Debug.Log("손님:" + GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt);

                if (tip_money[0] != 0 || tip_money[1] != 0 || tip_money[2] != 0) //부자 손님있을 때 실패한 경우, 부자손님 있을 땐 손님 카운트가 안됨
                {
                    if(tip_money[current_customer] != 0) tip_money[current_customer] = 0; //그 중 현재 손님이 부자손님인 경우, 팁 삭제
                }
                else
                {
                    GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt--; //손님수 카운트 된거 삭제
                }

                Debug.Log("손님:" + GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt);

                Invoke("failtea_customerBye", 1f); //1초 뒤 손님+말풍선 사라짐
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!check && current_customer != -1 && time != -1) //건네기 눌렀을 때
        {
            if(time == 0)
            {
                //부자손님 이미지 저장
                if (customer_num == 5 && rich_check == 0)
                {
                    rich_check++;
                    customer_img_idx[current_customer] = leng;
                    tip_money[current_customer] = 1000;
                }
                else if (customer_num == 8 && rich_check == 1)
                {
                    rich_check++;
                    customer_img_idx[current_customer] = leng;
                    tip_money[current_customer] = 1500;
                }
                else if (customer_num == 11 && rich_check == 2)
                {
                    rich_check++;
                    customer_img_idx[current_customer] = leng;
                    tip_money[current_customer] = 2000;
                }
                else if (customer_num == 15 && rich_check == 3)
                {
                    rich_check++;
                    customer_img_idx[current_customer] = leng;
                    tip_money[current_customer] = 2500;
                }
                else
                {
                    customer_img_idx[current_customer] = Random.Range(0, leng); //새로운 손님 이미지 저장
                }
            }
            time += Time.deltaTime;
        }

        if(time > 5f)
        {
            time = -1f;
            customer_obj[current_customer].SetActive(true);
            customerImage();
            customer_obj[current_customer].GetComponent<Image>().sprite = exc_mark;
        }
    }

    public void customerImage()
    {
        for (int i = 0; i < 3; i++)
        {
            customer_obj[i].transform.GetChild(0).GetComponent<Image>().sprite = customer_img[customer_img_idx[i]]; //손님 이미지
        }
    }

    public void failtea_customerBye()
    {
        customer_obj[current_customer].SetActive(false);
        for (int i = 0; i < 3; i++) //버튼 클릭 다시 가능하게
        {
            customer_obj[i].GetComponent<Button>().enabled = true;
        }
        check = false;
    }

}
