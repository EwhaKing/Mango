using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    BabyCustom babyCustom;
    List<Sprite> set_clothes; //진열되는 옷

    public List<int> shop = new List<int>(); //상점의 옷 번호

    public GameObject _item; //인스펙터뷰에서 설정해야 함

    private void Awake()
    {
        babyCustom = GameObject.Find("BabyCustom").GetComponent<BabyCustom>();
        set_clothes = babyCustom.clothes_set; //babyCustom 에 있는 진열되는 옷 스프라이트 가져오기
    }

    // Start is called before the first frame update
    void Start()
    {
        shopStart(); //상점 옷 나열 함수
    }

    public void shopStart()
    {
        bool check = true;

        int clothes_cnt = ShopDataScript.sd.item.Length; //현재 존재하는 옷세트 개수

        //보관 옷 뜨게
        for (int i = 0; i < clothes_cnt; i++)
        {
            GameObject item;

            if (check) //처음이라면 _item 사용해야함
            {
                item = _item;
                check = false;
            }
            else
            {
                item = GameObject.Instantiate(_item) as GameObject;
                item.name = "item" + i.ToString();
                item.transform.SetParent(_item.transform.parent);
                item.transform.localScale = Vector3.one;
                item.transform.localRotation = Quaternion.identity;
            }

            //item에 진열 옷 이미지 채우기
            item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = set_clothes[i];

            if (!ShopDataScript.sd.item[i].own) //해당 옷 세트를 가지고 있지 않다면
            {
                //가격
                item.transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<Text>().text = ShopDataScript.sd.item[i].item_cost.ToString();
            
                item.transform.GetChild(1).transform.gameObject.SetActive(true); //가격버튼 활성화
                item.transform.GetChild(2).transform.gameObject.SetActive(false); //보유중 버튼 비활성화
                item.transform.GetChild(0).gameObject.GetComponent<Button>().enabled = true; //옷 미리보기 활성화
            }
            else //가지고 있다면 -> 보유중 버튼 뜨게 하고 비활성화 하기
            {
                item.transform.GetChild(1).transform.gameObject.SetActive(false); //가격버튼은 비활성화
                item.transform.GetChild(2).transform.gameObject.SetActive(true); //보유중 버튼 활성화
                item.transform.GetChild(0).gameObject.GetComponent<Button>().enabled = false; //옷 미리보기 비활성화
            }

            //해당 옷 세트 번호 저장
            shop.Add(i);
        }

    }

}
