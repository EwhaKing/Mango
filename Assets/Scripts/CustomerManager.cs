using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomerManager : MonoBehaviour
{
    //public static int customer = 0; //누적 손님수
    public static bool check = false; //미니게임 씬 진행하고 왔는지 체크하는 변수
    public static int current_customer = -1; //현재 손님 인덱스
    public static int[] customer_img_idx = new int[3];

    public Sprite deliver; //건네기 이미지
    public GameObject[] customer_obj = new GameObject[3]; //손님 오브젝트

    public Sprite[] customer_img; //손님 이미지
    public GameObject tea_img; //건네는 차

    int leng; //손님 이미지 배열 길이



    // Start is called before the first frame update
    void Start()
    {
        leng = customer_img.Length;
        
        if(current_customer == -1)
        {
            /* //손님 이미지 중복 x 버전
            bool img_check = true;    
            for(int i=0;i<3;)
            {
                customer_img_idx[i] = Random.Range(0, leng);
                img_check = true;
                for(int j=i-1;j>=0;j--)
                {
                    if(customer_img_idx[i] == customer_img_idx[j])
                    {
                        img_check = false;
                        break;
                    }
                }
                if (img_check) i++;
            }
            */
            for(int i=0;i<3;i++)
            {
                customer_img_idx[i] = Random.Range(0, leng);
            }
        }

        if(check)
        {
            //check = false;
            //해당 손님 인덱스 말풍선 건네기로 바뀌게
            customer_obj[current_customer].GetComponent<Image>().sprite = deliver;
            tea_img.SetActive(true);
        }

        for (int i = 0; i < 3; i++)
        {
            customer_obj[i].transform.GetChild(0).GetComponent<Image>().sprite = customer_img[customer_img_idx[i]]; //손님 이미지
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!check && current_customer != -1)
        {
            customer_img_idx[current_customer] = Random.Range(0, leng);
        }
    }

}
