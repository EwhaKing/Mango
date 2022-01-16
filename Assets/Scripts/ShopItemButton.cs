using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public GameObject pay_popup; //구매 팝업창
    public GameObject pay_not; //구매 불가 팝업창


    public static int current_buy; //현재 구매 버튼 누른 옷

    public void onClickClothes() //옷 버튼 클릭 시 옷 미리보기
    {
        //효과음
        ButtonSound._buttonInstance.onButtonAudio();

        //현재 gameObject == item
        int index = gameObject.transform.GetSiblingIndex(); //item 몇번째 자식인지
        int clothe_sprite_num = GameObject.Find("GameManager").GetComponent<ShopManager>().shop[index]; //고른 옷의 스프라이트 넘버
        GameObject.Find("BabyCustom").GetComponent<BabyCustom>().changeBabyCustom(clothe_sprite_num); //아기 옷 미리보기
    }

    public void onClickPay()
    {
        //효과음
        ButtonSound._buttonInstance.onButtonAudio();

        int index = gameObject.transform.GetSiblingIndex(); //item 몇번째 자식인지

        int clothe_sprite_num = GameObject.Find("GameManager").GetComponent<ShopManager>().shop[index]; //고른 옷의 스프라이트 넘버

        string want_buy_text = ShopDataScript.sd.item[clothe_sprite_num].item_name + " 을(를) 구매하시겠습니까?"; //옷 이름 구매 문구
        string cost_text = ShopDataScript.sd.item[clothe_sprite_num].item_cost.ToString(); //옷 가격

        pay_popup.SetActive(true); //팝업창 띄우기

        //적용
        pay_popup.transform.GetChild(1).gameObject.GetComponent<Text>().text = want_buy_text;
        pay_popup.transform.GetChild(3).gameObject.GetComponent<Text>().text = cost_text;

        current_buy = clothe_sprite_num;
    }

    public void onBuyButton() //구매버튼 클릭시
    {
        //효과음
        ButtonSound._buttonInstance.onMoneyAudio();

        pay_popup.SetActive(false); //팝업창 닫기

        int cost = ShopDataScript.sd.item[current_buy].item_cost; //옷 돈

        Debug.Log("옷 번호: " + current_buy);

        //돈이 부족하다면 -> 구매 불가 팝업
        if (GameStaticData.data.data_money - cost < 0)
        {
            //효과음
            ButtonSound._buttonInstance.onPopUpAudio();

            pay_not.SetActive(true); //새 팝업창
        }

        else
        {
            //효과음
            ButtonSound._buttonInstance.onMoneyAudio();

            //구매 완료: 데이터베이스 소유 변경
            string str = File.ReadAllText(Application.dataPath + "/ShopData.json");
            ShopDataScript.sd.item[current_buy].own = true;

            //돈 변경
            GameStaticData.data.data_money -= cost;

            //상점 옷 보유중 변경
            gameObject.transform.parent.GetChild(current_buy).GetChild(1).transform.gameObject.SetActive(false); //가격버튼은 비활성화
            gameObject.transform.parent.GetChild(current_buy).GetChild(2).transform.gameObject.SetActive(true); //보유중 버튼 활성화
            gameObject.transform.parent.GetChild(current_buy).GetChild(0).gameObject.GetComponent<Button>().enabled = false; //옷 미리보기 비활성화

            //보관함에 옷 추가
            GameObject.Find("GameManager").GetComponent<LockerManager>().addClotheLocker(current_buy); //옷 보관함에 추가

            //데이터베이스에 다시 저장
            File.WriteAllText(Application.dataPath + "/ShopData.json", JsonUtility.ToJson(ShopDataScript.sd));
        }
    }

    public void onDeleteButton() //구매 취소 or 엑스 버튼 클릭 시
    {
        //효과음
        ButtonSound._buttonInstance.onMoneyAudio();

        pay_popup.SetActive(false); //팝업창 닫기
    }
}
