using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BabyChange : MonoBehaviour
{

    // 거대한 아기 이미지
    public Image wry_baby;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        /*
        // 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            Orig.sprite = New1; // 찡그린 거대 아기로 이미지 변경
            Handheld.Vibrate(); // 진동
            Invoke("Change", 0.5f);
        }*/

    }

    void Change()
    {
        wry_baby.gameObject.SetActive(false); // 원래 거대 아기로 이미지 변경
    }

    //특정 영역 클릭시 아기 표정 바꾸기
    public void OnClickBaby_BabyChange()
    {
        wry_baby.gameObject.SetActive(true); // 찡그린 거대 아기로 이미지 변경
        //Handheld.Vibrate(); // 진동
        Invoke("Change", 0.5f);
    }
}