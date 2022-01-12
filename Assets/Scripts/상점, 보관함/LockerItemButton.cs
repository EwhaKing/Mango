using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockerItemButton : MonoBehaviour
{
    public void onClickClothes()
    {
        //현재 gameObject == item

        int index = gameObject.transform.GetSiblingIndex(); //item 몇번째 자식인지

        //이미 색칠되어 있다면
        if (gameObject.transform.parent.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color.r == 0.8f)
        {
            //클릭 색 다시 없애기
            gameObject.transform.parent.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            //기본옷으로 바꾸기
            GameStaticData.data.data_cloth = 0; //옷세트 번호
            GameObject.Find("BabyCustom").GetComponent<BabyCustom>().changeBabyCustom(0); //아기 옷 입히기 적용

        }
        else //색칠 안됐더라면
        {
            //클릭한 버튼 색 바꾸기
            gameObject.transform.parent.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);

            for (int i = 0; i < gameObject.transform.parent.transform.childCount; i++)
            {
                if (i != index) gameObject.transform.parent.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }

            int clothe_sprite_num = GameObject.Find("GameManager").GetComponent<LockerManager>().locker_sprites[index]; //옷의 스프라이트 넘버

            GameStaticData.data.data_cloth = clothe_sprite_num; //옷세트 번호
            GameObject.Find("BabyCustom").GetComponent<BabyCustom>().changeBabyCustom(clothe_sprite_num); //아기 옷 입히기 적용
        }
    }
}
