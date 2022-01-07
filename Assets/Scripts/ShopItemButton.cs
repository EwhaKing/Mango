using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public GameObject pay_popup; //구매 팝업창
    public GameObject pay_not; //구매 불가 팝업창


    public void onClickClothes() //옷 버튼 클릭 시 옷 미리보기
    {
        //현재 gameObject == item
        int index = gameObject.transform.GetSiblingIndex(); //item 몇번째 자식인지
        int clothe_sprite_num = GameObject.Find("GameManager").GetComponent<ShopManager>().shop[index]; //고른 옷의 스프라이트 넘버
        GameObject.Find("BabyCustom").GetComponent<BabyCustom>().changeBabyCustom(clothe_sprite_num); //아기 옷 미리보기
    }

    public void onClickPay()
    {
        int index = gameObject.transform.GetSiblingIndex(); //item 몇번째 자식인지

        int clothe_sprite_num = GameObject.Find("GameManager").GetComponent<ShopManager>().shop[index]; //고른 옷의 스프라이트 넘버

        string want_buy_text = ShopDataScript.sd.item[clothe_sprite_num].item_name + " 을(를) 구매하시겠습니까?"; //옷 이름 구매 문구
        string cost_text = ShopDataScript.sd.item[clothe_sprite_num].item_cost.ToString(); //옷 가격

        pay_popup.SetActive(true); //팝업창 띄우기

        //적용
        pay_popup.transform.GetChild(0).gameObject.GetComponent<Text>().text = want_buy_text;
        pay_popup.transform.GetChild(2).gameObject.GetComponent<Text>().text = cost_text;

    }

    public void onBuyButton() //구매버튼 클릭시
    {
        pay_popup.SetActive(false); //팝업창 닫기

        int index = gameObject.transform.GetSiblingIndex(); //item 몇번째 자식인지

        int clothe_sprite_num = GameObject.Find("GameManager").GetComponent<ShopManager>().shop[index]; //고른 옷의 스프라이트 넘버
        
        int cost = ShopDataScript.sd.item[clothe_sprite_num].item_cost; //옷 돈

        //돈이 부족하다면 -> 구매 불가 팝업
        if (GameObject.Find("GameData").GetComponent<GameStaticData>().game_money - cost < 0)
        {
            pay_not.SetActive(true); //새 팝업창
        }

        else
        {
            //구매 완료: 데이터베이스 소유 변경
            ShopDataScript.sd.item[clothe_sprite_num].own = true; 

            //돈 변경
            GameObject.Find("GameData").GetComponent<GameStaticData>().game_money -= cost;

            //상점 옷 삭제
            Destroy(gameObject); //상점 진열 삭제
            GameObject.Find("GameManager").GetComponent<ShopManager>().shop.RemoveAt(index); //상점 옷 스프라이트 삭제

            //보관함에 옷 추가
            GameObject.Find("GameManager").GetComponent<LockerManager>().addClotheLocker(clothe_sprite_num); //옷 보관함에 추가

            //데이터베이스에 다시 저장
            File.WriteAllText(Application.dataPath + "/ShopData.json", JsonUtility.ToJson(ShopDataScript.sd));
        }
    }

    public void onDeleteButton() //구매 취소 or 엑스 버튼 클릭 시
    {
        pay_popup.SetActive(false); //팝업창 닫기
    }
}
