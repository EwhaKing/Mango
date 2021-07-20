using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropPF : MonoBehaviour
{


    // RhythmBar 스크립트에 이 스크립트를 낑겨 넣은 상태임
    // 다만 RhythmBar 스크립트에서 오류로 정상 실행이 되지 않아서 남긴 스크립트

    // 우측 바 상단 방울 이미지
    public Image Basic;
    public Sprite Success;
    public Sprite Fail;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 성공했다!를 어떻게 하지 - 일단 실행되는지 확인하게 조건에 아무거나 넣어야지 - 클릭 넣기로 함
        if (Input.GetMouseButtonDown(0))
        {
            Basic.sprite = Success; // 성공 이미지로 바꾸기
        }

        else
        {
            Basic.sprite = Fail; // 실패 이미지로 바꾸기
        }
    }
}
