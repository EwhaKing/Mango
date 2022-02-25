using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{

    public GameObject recipeDetailBg;

    public Sprite DrinkBg;
    public Sprite TeaBg;
    public Sprite SpecialBg;

    List<Sprite> set_recipe; //레시피

    public List<int> recipe_sprites = new List<int>(); //레시피 스프라이트 번호

    public GameObject _item;

    // Start is called before the first frame update
    void Start()
    {
        recipeStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        //데이터베이스 불러오기
        string str = File.ReadAllText(Application.persistentDataPath + "/TeaDex.json");
        TeaDataScript.teaDex = JsonUtility.FromJson<TeaDex>(str);
    }

    //오류 발생 - 아이템 18개만 있어야 하는데 18 * 3개 생기고 스크롤바로 일부만 확인 가능함
    public void recipeStart() //현재 모든 음료수가 뜨도록 되어 있음 //음료, 차, 스페셜로 분류되도록 해야 함
    {
        int recipe_cnt = TeaDataScript.teaDex.item.Length; //현재 존재하는 레시피 개수

        for (int i = 0; i < recipe_cnt; i++)
        {
            GameObject item;

            if (recipe_sprites.Count == 0) //처음이라면 _item 사용해야함
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

            //item에 레시피 이미지 채우기 - 현재 이미지 없어서 오류 나므로 우선 주석 처리
            //item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = set_recipe[i];

            //상세 레시피 활성화
            item.transform.GetChild(0).gameObject.GetComponent<Button>().enabled = true;

            //해당 옷 세트 번호 저장
            recipe_sprites.Add(i);

        }

    }

    public void OnClickDrink() //좌측 인덱스에서 음료 선택
    {
        ButtonSound._buttonInstance.onButtonAudio();
        recipeDetailBg.GetComponent<Image>().sprite = DrinkBg; //배경 이미지(색깔 박스)
    }

    public void OnClickTea() //좌측 인덱스에서 차 선택
    {
        ButtonSound._buttonInstance.onButtonAudio();
        recipeDetailBg.GetComponent<Image>().sprite = TeaBg; //배경 이미지(색깔 박스)
    }

    public void OnClickSpecial() //좌측 인덱스에서 스페셜 선택
    {
        ButtonSound._buttonInstance.onButtonAudio();
        recipeDetailBg.GetComponent<Image>().sprite = SpecialBg; //배경 이미지(색깔 박스)
    }
}
