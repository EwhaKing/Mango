using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< Updated upstream
using UnityEngine.EventSystems;

public class BabyChange : MonoBehaviour
{
    public Image Orig;
    public Sprite New;
    public Sprite New2;
//    private float DelayTime = 1f;
//    private float RealTime;

=======

public class BabyChange : MonoBehaviour
{

    // 거대한 아기 이미지
    public Image Orig; // 원본 거대 아기
    public Sprite New1; // 찡그린 거대 아기
    public Sprite New2; // 원본 거대 아기
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        
    }
    
    
=======

    }

>>>>>>> Stashed changes
    // Update is called once per frame

    void Update()
    {
<<<<<<< Updated upstream
        if (Input.GetMouseButtonDown(0))
        {
            Handheld.Vibrate();
            Orig.sprite = New;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            /*RealTime += Time.deltaTime;
            if (RealTime >= DelayTime)
            {*/
                Orig.sprite = New2;
            //}
=======
        // 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            Orig.sprite = New1; //찡그린 거대 아기로 이미지 변경
            Handheld.Vibrate(); // 진동

            //Invoke 함수 이용해서 시간 지연 구현하려고 했으나 실패
        }

        // 클릭에서 손 뗐을 때
        else if (Input.GetMouseButtonUp(0))
        {
            Orig.sprite = New2; // 원래 거대 아기로 이미지 변경
>>>>>>> Stashed changes
        }

    }

}
