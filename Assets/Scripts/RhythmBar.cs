using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RhythmBar : MonoBehaviour
{
    float MaxPos = 180f;
    float MinPos = -395f;

    public float MaxSuccess;
    public float MinSuccess;
    float speed = 1000f;
    float currPos;

    //public float success = 0;
    //private int successCnt = 0;

    private int cnt = 0;

    //화면 우측 바 윗부분 방울 이미지
    public Sprite Basic; //기본 방울
    public Sprite Success; //성공 방울
    public Sprite Fail; //실패 방울

    //오브젝트 리스트 dropDrop 선언
    public GameObject[] dropDrop;
    public GameObject successArea;

    //성공 횟수로 차 가격 설정하기
    public static int success_count;

    //사운드
    AudioSource audioSource;
    public AudioClip PowerSuccess;
    public AudioClip PowerFail;

    // Start is called before the first frame update
    void Start()
    {
        currPos = this.transform.localPosition.y;
        successArea = GameObject.FindGameObjectWithTag("SuccessArea");

        //dropDrop 리스트에 DropDrop 태그 걸은 오브젝트들(3개-방울) 추가
        //dropDrop = GameObject.FindGameObjectsWithTag("DropDrop");

        //게임 시작할 때 성공횟수 0으로 초기화
        success_count = 0;

        this.audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        currPos = speed * Time.deltaTime;
        this.transform.Translate(new Vector3(0, currPos, 0));

        //리듬바 움직이는 부분
        if (this.transform.localPosition.y >= MaxPos)
        {
            speed *= -1;
            transform.localPosition = new Vector3(695, MaxPos);
        } 
        else if(this.transform.localPosition.y <= MinPos)
        {
            speed *= -1;
            transform.localPosition = new Vector3(695, MinPos);
        }

    }

    //특정 영역 클릭시 리듬바에 대한 수행
    public void OnClickBaby_RhythmBar()
    {
        if (cnt == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                dropDrop[i].GetComponent<Image>().sprite = Basic;
            }
            cnt = 0;
        }

        float now = this.transform.localPosition.y;
        Debug.Log("max: " + MaxSuccess);
        if (now >= MinSuccess && now <= MaxSuccess)
        {
            Debug.Log("success pos: " + now);
            GameObject.Find("TimeSlider").GetComponent<SliderTimer>().success = 0.10f;

            //성공범위에 포함되면 과일색 방울로 이미지 바꿈
            dropDrop[cnt].GetComponent<Image>().sprite = Success;

            //PowerSuccess.Play();
            audioSource.clip = PowerSuccess;
            audioSource.Play();

            //성공하면 횟수 추가
            success_count++;
        }
        else
        {
            Debug.Log("fail pos: " + now);
            GameObject.Find("TimeSlider").GetComponent<SliderTimer>().success = -0.067f;

            //실패범위에 포함되면 실패한 방울로 이미지 바꿈
            dropDrop[cnt].GetComponent<Image>().sprite = Fail;

            //PowerFail.Play();
            audioSource.clip = PowerFail;
            audioSource.Play();

            Handheld.Vibrate(); // 실패시 진동
        }

        cnt++;
    }
}
