using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    BabyCustom babyCustom;
    List<Sprite> set_clothes; //진열되는 옷

    public List<int> shop = new List<int>(); //상점의 옷 스프라이트 번호

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
        int clothes_cnt = ShopDataScript.sd.item.Length; //현재 존재하는 옷세트 개수

        //기본 옷 빼고 시작
        //보관 안한 옷 먼저
        for (int i = 1; i < clothes_cnt; i++)
        {
            if (!ShopDataScript.sd.item[i].own) //해당 옷 세트를 가지고 있지 않다면
            {
                GameObject item;

                if (shop.Count == 0) //처음이라면 _item 사용해야함
                {
                    item = _item;
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
                //가격
                item.transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<Text>().text = ShopDataScript.sd.item[i].item_cost.ToString();

                item.transform.GetChild(1).transform.gameObject.SetActive(true); //가격버튼 활성화
                item.transform.GetChild(2).transform.gameObject.SetActive(false); //보유중 버튼 비활성화
                item.transform.GetChild(0).gameObject.GetComponent<Button>().enabled = true; //옷 미리보기 활성화

                //해당 옷 세트 번호 저장
                shop.Add(i);
            }
        }

        //보유중 뜨게
        for (int i = 1; i < clothes_cnt; i++)
        {
            if (ShopDataScript.sd.item[i].own) //해당 옷 세트를 가지고 있다면
            {
                GameObject item;

                if (shop.Count == 0) //처음이라면 _item 사용해야함
                {
                    item = _item;
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

                item.transform.GetChild(1).transform.gameObject.SetActive(false); //가격버튼은 비활성화
                item.transform.GetChild(2).transform.gameObject.SetActive(true); //보유중 버튼 활성화
                item.transform.GetChild(0).gameObject.GetComponent<Button>().enabled = false; //옷 미리보기 비활성화

                //해당 옷 세트 번호 저장
                shop.Add(i);
            }
            
        }

    }

    //기존 샾 정보 있을 때 덮어씌기 (구매한 후 보유중 리프레쉬)
    public void onShopLoad()
    {
        int clothes_cnt = ShopDataScript.sd.item.Length; //현재 존재하는 옷세트 개수
        int index = 0; //상점에 나열되는 인덱스

        //기본 옷 빼고 시작
        //보관 안한 옷 먼저
        for (int i = 1; i < clothes_cnt; i++)
        {
            if (!ShopDataScript.sd.item[i].own) //해당 옷 세트를 가지고 있지 않다면
            {
                GameObject item = _item.transform.parent.transform.GetChild(index).gameObject;

                //item에 진열 옷 이미지 채우기
                item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = set_clothes[i];
                //가격
                item.transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<Text>().text = ShopDataScript.sd.item[i].item_cost.ToString();

                item.transform.GetChild(1).transform.gameObject.SetActive(true); //가격버튼 활성화
                item.transform.GetChild(2).transform.gameObject.SetActive(false); //보유중 버튼 비활성화
                item.transform.GetChild(0).gameObject.GetComponent<Button>().enabled = true; //옷 미리보기 활성화

                shop[index++] = i; //새로운 옷 스프라이트로 갱신
            }
        }

        //보유중 뜨게
        for (int i = 1; i < clothes_cnt; i++)
        {
            if (ShopDataScript.sd.item[i].own) //해당 옷 세트를 가지고 있다면
            {
                GameObject item = _item.transform.parent.transform.GetChild(index).gameObject;

                //item에 진열 옷 이미지 채우기
                item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = set_clothes[i];

                item.transform.GetChild(1).transform.gameObject.SetActive(false); //가격버튼은 비활성화
                item.transform.GetChild(2).transform.gameObject.SetActive(true); //보유중 버튼 활성화
                item.transform.GetChild(0).gameObject.GetComponent<Button>().enabled = false; //옷 미리보기 비활성화

                shop[index++] = i; //새로운 옷으로 갱신
            }
        }
    }

}
