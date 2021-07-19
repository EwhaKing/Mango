using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeaManager : MonoBehaviour
{
    public const int INDEX = 3; //총 과일 개수
    public Text[] fruits_text = new Text[INDEX]; //각각 과일의 수 텍스트 UI
    public Image[] fruits_image = new Image[INDEX]; //각각 반과일 이미지 UI
    public int[] fruits_num = new int[INDEX]; //각각 과일의 개수 저장하는 배열
    public int num = 6; //각 과일 개수의 합
    public int finish_condition; //종료조건 검사하기 위해서
    private bool first_fruit = true; //처음 뜨게 하기 위한 체크 변수
    private int fruit_current = 0; //현재 과일 인덱스

    private int finish = 0; //임의로 과일 하나 완료된거 판단하기 위한 변수

    public Image left_arm, right_arm, jamong_left_arm, jamong_right_arm;

    // Start is called before the first frame update
    void Start()
    {
        left_arm.GetComponent<Animator>().enabled = false;
        right_arm.GetComponent<Animator>().enabled = false;
        jamong_left_arm.GetComponent<Animator>().enabled = false;
        jamong_right_arm.GetComponent<Animator>().enabled = false;

        finish_condition = num; //처음에 각 과일 개수 합으로 초기화 한 후, 0이 되면 속게임 종료

        for(int i = 0; i < INDEX; i++)
        {
            if (i == INDEX - 1) fruits_num[i] = num; //마지막 과일은 무조건 남은 개수, 총 개수가 num 개가 되어야 하므로
            else fruits_num[i] = Random.Range(0, num + 1); //과일 개수 랜덤으로 배정
            num -= fruits_num[i]; //배정되어야 할 남은 과일 개수
            fruits_text[i].text = fruits_num[i].ToString(); //텍스트에 표시

            //처음 과일 뜨게 하기
            if(fruits_num[i] != 0 && first_fruit)
            {
                first_fruit = false;
                fruit_current = i;
                changeFruit(i);
            }
        }


    }

    public void changeFruit(int fruit) //fruit 로 과일 바꾸기
    {
        for (int i = 0; i < INDEX; i++)
        {
            if (i == fruit) fruits_image[i].gameObject.SetActive(true);
            else fruits_image[i].gameObject.SetActive(false);
        }
        
        if(fruit == 1) //자몽이라면, 자몽이 너비가 넓어서 아기 팔의 위치를 조금 다르게 설정
        {
            left_arm.gameObject.SetActive(false);
            right_arm.gameObject.SetActive(false);
            jamong_left_arm.gameObject.SetActive(true);
            jamong_right_arm.gameObject.SetActive(true);
        }
        else
        {
            left_arm.gameObject.SetActive(true);
            right_arm.gameObject.SetActive(true);
            jamong_left_arm.gameObject.SetActive(false);
            jamong_right_arm.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(finish == 3 && finish_condition != 0)
        {
            finish_condition--;
            finish = 0;
            if(fruits_num[fruit_current] > 0)
            {
                fruits_num[fruit_current]--;
                fruits_text[fruit_current].text = fruits_num[fruit_current].ToString();
                if(fruits_num[fruit_current] == 0)
                {
                    for (int i = fruit_current + 1; i < INDEX; i++)
                    {
                        if (fruits_num[i] != 0)
                        {
                            fruit_current = i;
                            break;
                        }
                    }
                    changeFruit(fruit_current);
                }
            }
         
        }
        if (Input.GetMouseButtonDown(0)) //터치 시 팔 움직임
        {
            if(fruit_current == 1) //자몽이라면
            {
                if(jamong_left_arm.GetComponent<Animator>().isActiveAndEnabled == false)
                {
                    jamong_left_arm.GetComponent<Animator>().enabled = true;
                    jamong_right_arm.GetComponent<Animator>().enabled = true;
                }
                else
                {
                    jamong_left_arm.GetComponent<Animator>().Play("arm_left_anim_jamong", -1, 0f);
                    jamong_right_arm.GetComponent<Animator>().Play("arm_right_anim_jamong", -1, 0f);
                }
            }
            else //레몬, 꿀이라면
            {
                if(left_arm.GetComponent<Animator>().isActiveAndEnabled == false)
                {
                    left_arm.GetComponent<Animator>().enabled = true;
                    right_arm.GetComponent<Animator>().enabled = true;
                }
                else
                {
                    left_arm.GetComponent<Animator>().Play("arm_left_anim", -1, 0f);
                    right_arm.GetComponent<Animator>().Play("arm_right_anim_lemon", -1, 0f);
                }
            }
            finish++; //터치 판단
        }

        if (finish_condition == 0) fruits_image[0].gameObject.SetActive(true);
    }
}
