using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BabyCustom : MonoBehaviour
{
    bool baby_check = true;

    public List<Sprite> hair; //0: 기본 옷, 1: 꿀벌 옷, 2: 피크닉 옷, 3: 곰돌이 옷
    public List<Sprite> clothes;
    public List<Sprite> left_arm;
    public List<Sprite> right_arm;

    public List<Sprite> clothes_set; //진열되는 옷

    List<List<Sprite>> custom_sprites = new List<List<Sprite>>(4); //행: 옷, 머리, 왼팔, 오른팔 / 열: 종류

    GameObject babyObject;

    GameStaticData dataScript;

    private void Awake()
    {
        dataScript = GameObject.Find("GameData").GetComponent<GameStaticData>();
        babyObject = GameObject.Find("baby_object");

        DontDestroyOnLoad(GameObject.Find("BabyCustom"));
    }

    // Start is called before the first frame update
    void Start()
    {
        //custom_sprites 각 행의 열 초기화
        custom_sprites.Add(new List<Sprite>());
        custom_sprites.Add(new List<Sprite>());
        custom_sprites.Add(new List<Sprite>());
        custom_sprites.Add(new List<Sprite>());

        //행에 대한 열 삽입
        custom_sprites[0] = clothes;     //옷 스프라이트
        custom_sprites[1] = hair;        //머리 스프라이트
        custom_sprites[2] = left_arm;    //왼팔 스프라이트
        custom_sprites[3] = right_arm;   //오른팔 스프라이트

        if (babyObject.activeSelf) changeBabyCustom(dataScript.baby_custom);
        
    }

    // Update is called once per frame
    void Update()
    {
        babyObject = GameObject.Find("baby_object");

        if (babyObject)
        {
            if (baby_check)
            {
                changeBabyCustom(dataScript.baby_custom); //현재 입고 있는 옷세트의 번호 매개변수로 보내기
                baby_check = false;
            }
        }
        else baby_check = true;
    }

    public void changeBabyCustom(int custom_data)
    {
        babyObject = GameObject.Find("baby_object");
        Debug.Log("현재 옷 세트: " + custom_data);
        for (int i = 0; i < 4; i++)
        {
            babyObject.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = custom_sprites[i][custom_data];
        }
    }

    public void lockerStart()
    {
        changeBabyCustom(dataScript.baby_custom); //현재 입고 있는 옷세트의 번호 매개변수로 보내기
    }

}
