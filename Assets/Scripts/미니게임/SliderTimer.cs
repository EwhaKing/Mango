using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderTimer : MonoBehaviour
{
    private Slider slTimer;
    private GameObject sliderTimer;
    public float success = 0;
    public float speed = 0.05f;

    private int currcus;
    private GameObject successArea;

    public static bool time_over = false; //타임오버됐는지 확인하는 변수, 메인씬에서 customerManager에서 쓰려고 만들었습니다!
    AudioSource timemusic;

    // Start is called before the first frame update
    void Start()
    {
        sliderTimer = GameObject.Find("TimeSlider");
        timemusic = sliderTimer.GetComponent<AudioSource>();

        slTimer = GetComponent<Slider>();
        successArea = GameObject.FindGameObjectWithTag("SuccessArea");

        currcus = GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt;
        //부자 손님이 없을 경우만 손님수 카운트
        if (CustomerManager.tip_money[0] == 0 && CustomerManager.tip_money[1] == 0 && CustomerManager.tip_money[2] == 0) 
        {
            GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt = ++currcus;
        }

        int difficulty = GameStaticData.data.difficulty;
        Debug.LogWarning("난이도: " + difficulty);

        if (difficulty == 1) //1단계
        {
            speed = 0.05f;
        }
        else if (difficulty == 2) //2단계
        {
            speed = 0.05f;
            successArea.transform.localScale = new Vector3(1, 0.66f, 1); //작아짐
        }
        else if (difficulty == 3) //3단계
        {
            speed = 0.05f;
            successArea.transform.localScale = new Vector3(1, 1, 1); //넓고 움직임
            SuccessArea.MaxPos = 208f; SuccessArea.MinPos = -233f;
            GameObject.Find("Image_success").GetComponent<SuccessArea>().successSpeed = 100f;
        }
        else if (difficulty == 4) //4단계
        {
            speed = 0.05f;
            successArea.transform.localScale = new Vector3(1, 0.66f, 1); //좁고 움직임
            GameObject.Find("Image_success").GetComponent<SuccessArea>().successSpeed = 100f;
        }
        else //5단계
        {
            speed = 0.067f;
            successArea.transform.localScale = new Vector3(1, 0.66f, 1); //좁고 움직이는데 시간도 빨리 줄어듦
            GameObject.Find("Image_success").GetComponent<SuccessArea>().successSpeed = 100f;
        }
    }

    void Update()
    {

        if (slTimer.value>0.0f)
        {
            slTimer.value -= speed * Time.deltaTime;
            slTimer.value += success;
            success = 0;
            if(slTimer.value <= 0.25f)
            {
                if(!timemusic.isPlaying)
                {
                    //timemusic.Play();
                    if (GamePause.soundOnOff == 1)
                    {
                        timemusic.Play();
                    }
                }
            }
        }else
        {
            Debug.Log("Timeup!");
            //int life = GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().lifecnt;
            //if(life!=0)
            //{
                time_over = true;
                //GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().lifecnt -= 1;
            //}
            SceneManager.LoadScene("maingameScene");
        }
        
    }
}
