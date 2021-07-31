using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomerTalkButton : MonoBehaviour
{
    public GameObject tea_img;

    public void OnClickCustomerTalk()
    {
        if(!CustomerManager.check) //느낌표 클릭
        {
            //CustomerManager.customer++; 
            CustomerManager.check = true; //미니게임 넘어갔음을 체크 (다시 돌아왔을 때 메인씬에 건네기+차 뜨게하기 위한 확인변수
            CustomerManager.current_customer = this.gameObject.transform.GetSiblingIndex(); //현재 손님 몇번째인지 저장
            //Debug.Log(CustomerManager.current_customer);
            SceneManager.LoadScene("minigameSceneFinish");
        }
        else if(CustomerManager.current_customer == this.gameObject.transform.GetSiblingIndex()) //건네기 클릭
        {
            CustomerManager.check = false;
            //차 드래그 애니메이션
            tea_img.GetComponent<ClickMove2>().enabled = true;
            //다른 말풍선 클릭 안되게
            for (int i = 0; i < 3; i++)
            {
                GameObject.Find("GameManager").GetComponent<CustomerManager>().customer_obj[i].GetComponent<Button>().enabled = false;
            }

            if(CustomerManager.tip_money[CustomerManager.current_customer] != 0)
            {
                TeaMoney.totalMoney += CustomerManager.tip_money[CustomerManager.current_customer];
                CustomerManager.tip_money[CustomerManager.current_customer] = 0;
            }

            //손님+말풍선 삭제, 차 드래그 되는 시간 기다리기
            Invoke("deleteObject", 2.5f);
        }

    }

    public void deleteObject()
    {
        //손님+말풍선 삭제
        tea_img.SetActive(false);
        gameObject.SetActive(false);

        //부자손님 돈 올라가게
        GameObject.Find("Text_money").GetComponent<Text>().text = TeaMoney.totalMoney.ToString();

        //다른 말풍선 클릭 가능하게
        for (int i = 0; i < 3; i++)
        {
            GameObject.Find("GameManager").GetComponent<CustomerManager>().customer_obj[i].GetComponent<Button>().enabled = true;
        }
    }
}
