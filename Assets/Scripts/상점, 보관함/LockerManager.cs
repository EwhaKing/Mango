using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockerManager : MonoBehaviour
{
    BabyCustom babyCustom;
    List<Sprite> _hair;
    List<Sprite> _clothes;

    List<int> locker_clothes; //GameStaticData꺼 가져와서 저장
    public List<int> locker_sprites = new List<int>(); //보관함 옷의 스프라이트 번호

    public GameObject _item;

    private void Awake()
    {
        babyCustom = GameObject.Find("BabyCustom").GetComponent<BabyCustom>();
        _hair = babyCustom.hair;
        _clothes = babyCustom.clothes;
        locker_clothes = GameObject.Find("GameData").GetComponent<GameStaticData>().baby_clothes;
    }
    // Start is called before the first frame update
    void Start()
    {
        int cnt = 0;

        //보관 옷 뜨게
        for(int i=0;i<locker_clothes.Count;i++)
        {
            if(locker_clothes[i] == 1)
            {
                GameObject item;
                if (cnt == 0) //처음이라면 _item 사용해야함
                {
                    _item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _clothes[i];
                    locker_sprites.Add(i);
                    item = _item;
                }
                else
                {
                    item = GameObject.Instantiate(_item) as GameObject;
                    item.name = "item" + cnt.ToString();
                    item.transform.SetParent(_item.transform.parent);
                    //item.transform.localScale = Vector3.one;
                    //item.transform.localRotation = Quaternion.identity;
                    
                    item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _clothes[i];
                    locker_sprites.Add(i);
                }

                if (GameObject.Find("GameData").GetComponent<GameStaticData>().baby_custom[0] == i)
                {
                    item.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                }
                else item.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                cnt++;
            }
        }

    }
}
