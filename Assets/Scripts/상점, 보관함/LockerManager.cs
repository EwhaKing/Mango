using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LockerManager : MonoBehaviour
{
    BabyCustom babyCustom;
    List<Sprite> set_clothes; //진열되는 옷

    public List<int> locker_sprites = new List<int>(); //보관함 옷의 스프라이트 번호

    public GameObject _item;

    private void Awake()
    {
        babyCustom = GameObject.Find("BabyCustom").GetComponent<BabyCustom>();
        set_clothes = babyCustom.clothes_set; //babyCustom 에 있는 진열되는 옷 스프라이트 가져오기

        //데이터베이스 불러오기
        string str = File.ReadAllText(Application.dataPath + "/ShopData.json");
        ShopDataScript.sd = JsonUtility.FromJson<ShopData>(str);
    }
    // Start is called before the first frame update
    void Start()
    {
        lockerStart();  //보관함 옷 나열 함수
    }

    public void lockerStart()
    {
        int cnt = 0;

        int clothes_cnt = ShopDataScript.sd.item.Length; //현재 존재하는 옷세트 개수

        //보관 옷 뜨게
        for (int i = 0; i < clothes_cnt; i++)
        {
            if (ShopDataScript.sd.item[i].own) //해당 옷 세트를 가지고 있다면
            {
                GameObject item;
                if (cnt == 0) //처음이라면 _item 사용해야함
                {
                    _item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = set_clothes[i];
                    locker_sprites.Add(i);
                    item = _item;
                }
                else
                {
                    item = GameObject.Instantiate(_item) as GameObject;
                    item.name = "item" + cnt.ToString();
                    item.transform.SetParent(_item.transform.parent);
                    item.transform.localScale = Vector3.one;
                    item.transform.localRotation = Quaternion.identity;

                    item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = set_clothes[i];
                    locker_sprites.Add(i);
                }

                if (GameObject.Find("GameData").GetComponent<GameStaticData>().baby_custom == i)
                {
                    item.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                }
                else item.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                cnt++;
            }
        }

    }

    public void addClotheLocker(int clothe_sprite)
    {
        GameObject item = GameObject.Instantiate(_item) as GameObject;
        item.name = "item" + (locker_sprites.Count + 1).ToString();
        item.transform.SetParent(_item.transform.parent);
        //item.transform.localScale = Vector3.one;
        //item.transform.localRotation = Quaternion.identity;

        item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = set_clothes[clothe_sprite];
        locker_sprites.Add(clothe_sprite);
    }

}
