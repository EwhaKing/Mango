using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BabyCustom : MonoBehaviour
{
    //bool baby_check = true;

    public List<Sprite> hair; //0: 기본 옷, 1: 꿀벌 옷, 2: 피크닉 옷, 3: 곰돌이 옷
    public List<Sprite> clothes;
    public List<Sprite> left_arm;
    public List<Sprite> right_arm;

    public List<Sprite> clothes_set; //진열되는 옷

    List<List<Sprite>> custom_sprites = new List<List<Sprite>>(4); //행: 옷, 왼팔, 오른팔, 머리 / 열: 종류

    GameObject babyObject;

    GameStaticData dataScript;

    private void Awake()
    {
        dataScript = GameObject.Find("GameData").GetComponent<GameStaticData>();
        babyObject = GameObject.Find("baby_object");

        //custom_sprites 각 행의 열 초기화
        custom_sprites.Add(new List<Sprite>());
        custom_sprites.Add(new List<Sprite>());
        custom_sprites.Add(new List<Sprite>());
        custom_sprites.Add(new List<Sprite>());

        //행에 대한 열 삽입
        custom_sprites[0] = clothes;     //옷 스프라이트
        custom_sprites[1] = left_arm;    //왼팔 스프라이트
        custom_sprites[2] = right_arm;   //오른팔 스프라이트
        custom_sprites[3] = hair;        //머리 스프라이트

        //if (babyObject.activeSelf) changeBabyCustom(GameStaticData.data.data_cloth);
        changeBabyCustom(GameStaticData.data.data_cloth); //시작하면 바로 현재 옷 입히기
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    /*void Update()
    {
        babyObject = GameObject.Find("baby_object");

        if (babyObject)
        {
            if (baby_check)
            {
                changeBabyCustom(GameStaticData.data.data_cloth); //현재 입고 있는 옷세트의 번호 매개변수로 보내기
                baby_check = false;
            }
        }
        else baby_check = true;
    }*/

    public void changeBabyCustom(int custom_data)
    {
        babyObject = GameObject.Find("baby_object");
        Debug.Log("현재 옷 세트: " + custom_data);
        int child_count = babyObject.transform.childCount;
        Debug.Log("자식수: " + child_count);
        if(child_count == 4) //상점 속 아기 이미지
        {
            for (int i = 0; i < child_count; i++)
            {
                babyObject.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = custom_sprites[i][custom_data];
            }
        }
        else if(child_count == 2) //미니게임 속 아기 이미지
        {
            Debug.Log(custom_sprites[0][0]);
            babyObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = custom_sprites[0][custom_data]; //옷
            babyObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = custom_sprites[3][custom_data]; //머리

            BabyChange babyScript = babyObject.GetComponent<BabyChange>();
            TeaManager teaScript = GameObject.Find("GameManager").GetComponent<TeaManager>();
            switch (teaScript.fruit_current)
            {
                case 1: case 2: case 3: case 4: case 7: case 8: case 9: //너비 넓은거
                    babyScript.left_arm_jamong.sprite = custom_sprites[1][custom_data];
                    babyScript.right_arm_jamong.sprite = custom_sprites[2][custom_data];
                    break;
                default:
                    babyScript.left_arm.sprite = custom_sprites[1][custom_data];
                    babyScript.right_arm.sprite = custom_sprites[2][custom_data];
                    break;
            }
        }
        else //시작화면 속 아기 이미지
        {
            babyObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = custom_sprites[0][custom_data]; //바디
            babyObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = custom_sprites[3][custom_data]; //머리
            babyObject.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = custom_sprites[1][custom_data]; //왼팔
            babyObject.transform.GetChild(4).gameObject.GetComponent<Image>().sprite = custom_sprites[2][custom_data]; //오른팔

        }
    }
}
