using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomerTalkButton : MonoBehaviour
{
    public static bool main_over_check = true; //끝낼 때 손님한테 차 건네고 있는지 판단

    public GameObject tea_img;
    public GameObject plus_img; //손님 사라지고 추가금(이미지+텍스트)
    public GameObject music_money;
    public Text tip_money_text; //부자손님 팁 텍스트

    //int speed = 1; //추가금 이동 속도

    AudioSource audioSource, audioSource_customer;
    public AudioClip CoinUp;
    public AudioClip Button_audio; //버튼음
    public AudioClip[] customer_happy; //손님 웃음소리

    void Start()
    {
        audioSource_customer = GameObject.Find("customer").GetComponent<AudioSource>();
    }

    public void OnClickCustomerTalk()
    {
        if (!CustomerManager.check) //느낌표 클릭
        {
            ButtonSound._buttonInstance.onButtonAudio(); //버튼 효과음 재생
            //CustomerManager.customer++; 
            CustomerManager.check = true; //미니게임 넘어갔음을 체크 (다시 돌아왔을 때 메인씬에 건네기+차 뜨게하기 위한 확인변수
            CustomerManager.current_customer = this.gameObject.transform.GetSiblingIndex(); //현재 손님 몇번째인지 저장
            //Debug.Log(CustomerManager.current_customer);

            //현재 메인겜 시간 저장
            GameObject.Find("GameData").GetComponent<GameStaticData>().mainscene_time = GameObject.Find("clock_hand").GetComponent<ClockMove>().time;
            //현재 메인갬 시계바늘각도 저장
            GameObject.Find("GameData").GetComponent<GameStaticData>().clock_hand_rot = GameObject.Find("clock_hand").GetComponent<ClockMove>().clock_hand.transform.localEulerAngles.z;
            //미니게임 부르기
            SceneManager.LoadScene("minigameSceneFinish");
        }
        else if(CustomerManager.current_customer == this.gameObject.transform.GetSiblingIndex()) //건네기 클릭
        {
            main_over_check = false;

            ButtonSound._buttonInstance.onButtonAudio(); //버튼 효과음 재생
            CustomerManager.check = false;
            //차 드래그 애니메이션
            tea_img.GetComponent<ClickMove2>().enabled = true;
            //다른 말풍선 클릭 안되게
            for (int i = 0; i < 3; i++)
            {
                GameObject.Find("GameManager").GetComponent<CustomerManager>().customer_obj[i].GetComponent<Button>().enabled = false;
            }

            if(CustomerManager.tip_money[CustomerManager.current_customer] != 0)
            {
                tip_money_text.text = "+ 보너스 " + CustomerManager.tip_money[CustomerManager.current_customer].ToString();
                tip_money_text.gameObject.SetActive(true);
                TotalMoney.totalMoney += CustomerManager.tip_money[CustomerManager.current_customer];
                CustomerManager.tip_money[CustomerManager.current_customer] = 0;
            }

            audioSource_customer.clip = customer_happy[CustomerManager.customer_img_idx[CustomerManager.current_customer]];
            //audioSource_customer.Play(); //손님 웃음소리 재생

            if (GamePause.soundOnOff == 1)
            {
                audioSource_customer.volume = SettingManager.soundSliderValue;
                audioSource_customer.Play();
            }

            //손님+말풍선 삭제, 차 드래그 되는 시간 기다리기
            Invoke("deleteObject", 2.5f);
        }

    }

    public void deleteObject()
    {
        //손님+말풍선 삭제
        tea_img.SetActive(false);
        gameObject.SetActive(false);

        //부자손님 돈 + 미니게임에서 번 돈 올라가게
        GameObject.Find("Text_money").GetComponent<Text>().text = (string.Format("{0:#,###}", TotalMoney.totalMoney)).ToString();

        Invoke("plusDisplayMoney", 0.1f);
    }

    public void plusDisplayMoney()
    {
        plus_img.SetActive(true); //추가금 관련 게임 오브젝트 표시하고
        audioSource = this.music_money.GetComponent<AudioSource>();

        audioSource.clip = CoinUp;
        //audioSource.Play();

        if (GamePause.soundOnOff == 1)
        {
            audioSource.volume = SettingManager.soundSliderValue;
            audioSource.Play();
        }



        //float yMove = speed * Time.deltaTime; //속도 설정
        //this.transform.Translate(new Vector3(0, yMove, 0)); //customer 오브젝트는 움직이면 안됩니당

        Invoke("Bye", 0.3f);
    }

    void Bye()
    {
        plus_img.SetActive(false);

        //다른 말풍선 클릭 가능하게
        for (int i = 0; i < 3; i++)
        {
            GameObject.Find("GameManager").GetComponent<CustomerManager>().customer_obj[i].GetComponent<Button>().enabled = true;
        }

        Invoke("mainOver", 0.3f);

    }

    void mainOver()
    {
        main_over_check = true;
    }
}
