using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmBar : MonoBehaviour
{
    float startPos = -104f;
    float MaxPos = 180f;
    float MinPos = -395f;

    float MaxSuccess;
    float MinSuccess;
    float speed = 15f;
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

    // Start is called before the first frame update
    void Start()
    {
        currPos = this.transform.localPosition.y;
        GameObject successArea = GameObject.FindGameObjectWithTag("SuccessArea");
        float successHeight = successArea.GetComponent<RectTransform>().rect.width;

        MaxSuccess = startPos + (successHeight / 2f);
        MinSuccess = startPos - (successHeight / 2f);

        //dropDrop 리스트에 DropDrop 태그 걸은 오브젝트들(3개-방울) 추가
        dropDrop = GameObject.FindGameObjectsWithTag("DropDrop");

    }

    // Update is called once per frame
    void Update()
    {
        currPos = speed * Time.deltaTime;
        this.transform.Translate(new Vector3(0, currPos, 0));
       
        //GetMouseButtonDown으로 수행하던 부분 아래 OnClickBaby_RhythmBar에 옮겨짐
        /*if(Input.GetMouseButtonDown(0))
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

            if (now >= MinSuccess && now <= MaxSuccess)
            {
                Debug.Log("success pos: " + now);
                success = 0.10f;

                //성공범위에 포함되면 과일색 방울로 이미지 바꿈
                dropDrop[cnt].GetComponent<Image>().sprite = Success;
            }
            else
            {
                Debug.Log("fail pos: " + now);
                success = -0.05f;

                //실패범위에 포함되면 실패한 방울로 이미지 바꿈
                dropDrop[cnt].GetComponent<Image>().sprite = Fail;
            }

            cnt++;
        }*/

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

        if (now >= MinSuccess && now <= MaxSuccess)
        {
            Debug.Log("success pos: " + now);
            GameObject.Find("TimeSlider").GetComponent<SliderTimer>().success = 0.10f;

            //성공범위에 포함되면 과일색 방울로 이미지 바꿈
            dropDrop[cnt].GetComponent<Image>().sprite = Success;
        }
        else
        {
            Debug.Log("fail pos: " + now);
            GameObject.Find("TimeSlider").GetComponent<SliderTimer>().success = -0.05f;

            //실패범위에 포함되면 실패한 방울로 이미지 바꿈
            dropDrop[cnt].GetComponent<Image>().sprite = Fail;
        }

        cnt++;
    }
}
