using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockerItemButton : MonoBehaviour
{
    public void onClickClothes()
    {
        int index = gameObject.transform.GetSiblingIndex(); //item 몇번째 자식인지

        for(int i=0;i<gameObject.transform.parent.transform.childCount; i++)
        {
            if (i != index) gameObject.transform.parent.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        int clothe_sprite_num = GameObject.Find("GameManager").GetComponent<LockerManager>().locker_sprites[index];

        GameObject.Find("GameData").GetComponent<GameStaticData>().baby_custom[0] = clothe_sprite_num; //0이 옷 정보
        GameObject.Find("BabyCustom").GetComponent<BabyCustom>().changeBabyCustom(); //아기 옷 입히기 적용
    }
}
