using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BabyChange : MonoBehaviour
{

    // 거대한 아기 이미지
    public Image norm_baby_face;
    public Image wry_baby_face;

    public Image left_arm;
    public Image right_arm;

    public Image left_arm_jamong;
    public Image right_arm_jamong;

    // Start is called before the first frame update
    void Start()
    {
        norm_baby_face.gameObject.SetActive(true);
        wry_baby_face.gameObject.SetActive(false);
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
        wry_baby_face.gameObject.SetActive(false);
        norm_baby_face.gameObject.SetActive(true);
    }

    //특정 영역 클릭시 아기 표정 바꾸기
    public void OnClickBaby_BabyChange()
    {
        norm_baby_face.gameObject.SetActive(false);
        wry_baby_face.gameObject.SetActive(true);
        //Handheld.Vibrate(); // 진동
        Invoke("Change", 0.5f);
    }
}