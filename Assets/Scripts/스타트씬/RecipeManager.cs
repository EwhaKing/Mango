using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    public GameObject teaIndex;
    public GameObject juiceIndex;
    public GameObject specialIndex;

    public Sprite TeaBg1;
    public Sprite DrinkBg1;
    public Sprite SpecialBg1;

    public Sprite TeaBg2;
    public Sprite DrinkBg2;
    public Sprite SpecialBg2;

    public GameObject teaContent;
    public GameObject drinkContent;
    public GameObject specialContent;

    public Sprite[] total_sprite = new Sprite[21]; //과일 이미지
    public Sprite[] bubble_sprite = new Sprite[21]; //방울 이미지

    public Sprite[] gc_recipe = new Sprite[36];
    
    Sprite[] real_recipe = new Sprite[18];

    // Start is called before the first frame update
    void Start()
    {
        teaStart();
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

    public void OnClickRecipe()
    {
        changeSprite();

        teaIndex.GetComponent<Image>().sprite = TeaBg1; //배경 이미지(색깔 박스)
        juiceIndex.GetComponent<Image>().sprite = DrinkBg2;
        specialIndex.GetComponent<Image>().sprite = SpecialBg2;

        GameObject.Find("Recipe_title").GetComponent<TextMeshProUGUI>().text = TeaDataScript.teaDex.item[0].tea_name; //레시피 이름
        GameObject.Find("Recipe_detail_text").GetComponent<TextMeshProUGUI>().text = TeaDataScript.teaDex.item[0].tea_description; //레시피 상세설명

        for (int i=0; i<18; i++)
        {
            if (i == 0 || i == 5 || i == 8 || i == 11) //tea일 때
            {
                //GameObject.Find("item_" + i.ToString()).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = real_recipe[i];
            }
        }

        GameObject.Find("Recipe_image").GetComponent<Image>().sprite = real_recipe[0]; //레시피 상세 이미지

        //최종 때 주석 지울 것 - 현재 확인하느라

        if (!TeaDataScript.teaDex.item[0].own) //해당 레시피를 소유하고 있지 않다면
        {
            GameObject.Find("Recipe_content").transform.GetChild(8).gameObject.SetActive(true); //레시피 해금 전 미공개 이미지 띄우기
        }

        else
        {
            GameObject.Find("Recipe_content").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(4).gameObject.SetActive(true);
            GameObject.Find("Recipe_content").transform.GetChild(5).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(7).gameObject.SetActive(false);

            GameObject.Find("Ingredient_2").GetComponent<Image>().sprite = total_sprite[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

            GameObject.Find("bubble_2").GetComponent<Image>().sprite = bubble_sprite[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

            //GameObject.Find("bubble_name2").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

            GameObject.Find("bubble_num2").GetComponent<TextMeshProUGUI>().text = "x " + TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_amout.ToString() + "<b>↑</b>";
        }

    }

    public void changeSprite()
    {
        for (int i = 0; i < 18; i++)
        {
            if (TeaDataScript.teaDex.item[i].own) //해당 레시피를 소유하고 있다면
            {
                //레시피 버튼 회색에서 컬러 되도록
                real_recipe[i] = gc_recipe[i + 18];
            }

            else
            {
                real_recipe[i] = gc_recipe[i];
            }
        }
    }

    public void OnClickTea() //좌측 인덱스에서 차 선택
    {
        ButtonSound._buttonInstance.onButtonAudio();
        
        teaIndex.GetComponent<Image>().sprite = TeaBg1; //배경 이미지(색깔 박스)
        juiceIndex.GetComponent<Image>().sprite = DrinkBg2;
        specialIndex.GetComponent<Image>().sprite = SpecialBg2;

        GameObject.Find("Recipe_title").GetComponent<TextMeshProUGUI>().text = TeaDataScript.teaDex.item[0].tea_name; //레시피 이름
        GameObject.Find("Recipe_detail_text").GetComponent<TextMeshProUGUI>().text = TeaDataScript.teaDex.item[0].tea_description; //레시피 상세설명

        for (int i = 0; i < 18; i++)
        {
            if (i == 0 || i == 5 || i == 8 || i == 11) //tea일 때
            {
                //GameObject.Find("item_" + i.ToString()).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = real_recipe[i];
            }
        }

        GameObject.Find("Recipe_image").GetComponent<Image>().sprite = real_recipe[0];

        if (!TeaDataScript.teaDex.item[0].own) //해당 레시피를 소유하고 있지 않다면
        {
            GameObject.Find("Recipe_content").transform.GetChild(8).gameObject.SetActive(true); //레시피 해금 전 미공개 이미지 띄우기
        }

        else
        {
            GameObject.Find("Recipe_content").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(4).gameObject.SetActive(true);
            GameObject.Find("Recipe_content").transform.GetChild(5).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(7).gameObject.SetActive(false);

            GameObject.Find("Ingredient_2").GetComponent<Image>().sprite = total_sprite[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

            GameObject.Find("bubble_2").GetComponent<Image>().sprite = bubble_sprite[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

            //GameObject.Find("bubble_name2").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

            GameObject.Find("bubble_num2").GetComponent<TextMeshProUGUI>().text = "x " + TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_amout.ToString() + "<b>↑</b>";
        }
        
        teaStart(); //상세 레시피 활성화
    }

    public void teaStart()
    {
        for (int i = 0; i < 4; i++)
        {
            teaContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Button>().enabled = true;
        }
    }

    public void OnClickDrink() //좌측 인덱스에서 음료 선택
    {
        ButtonSound._buttonInstance.onButtonAudio();

        teaIndex.GetComponent<Image>().sprite = TeaBg2; //배경 이미지(색깔 박스)
        juiceIndex.GetComponent<Image>().sprite = DrinkBg1;
        specialIndex.GetComponent<Image>().sprite = SpecialBg2;

        GameObject.Find("Recipe_title").GetComponent<TextMeshProUGUI>().text = TeaDataScript.teaDex.item[2].tea_name; //레시피 이름
        GameObject.Find("Recipe_detail_text").GetComponent<TextMeshProUGUI>().text = TeaDataScript.teaDex.item[2].tea_description; //레시피 상세설명

        for (int i = 0; i < 18; i++)
        {
            if (i == 2 || i == 3 || i == 4 || i == 6 || i == 7 || i == 9 || i == 10 || i == 12 || i == 17)
            {
                //GameObject.Find("item_" + i.ToString()).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = real_recipe[i];
            }
        }

        GameObject.Find("Recipe_image").GetComponent<Image>().sprite = real_recipe[2];

        if (!TeaDataScript.teaDex.item[2].own) //해당 레시피를 소유하고 있지 않다면
        {
            GameObject.Find("Recipe_content").transform.GetChild(8).gameObject.SetActive(true); //레시피 해금 전 미공개 이미지 띄우기
        }

        else
        {
            GameObject.Find("Recipe_content").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(4).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(5).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(true);
            GameObject.Find("Recipe_content").transform.GetChild(7).gameObject.SetActive(true);

            GameObject.Find("Ingredient_4").GetComponent<Image>().sprite = total_sprite[TeaDataScript.teaDex.item[2].tea_recipe[0].ingredient_num];
            GameObject.Find("Ingredient_5").GetComponent<Image>().sprite = total_sprite[TeaDataScript.teaDex.item[2].tea_recipe[1].ingredient_num];

            GameObject.Find("bubble_4").GetComponent<Image>().sprite = bubble_sprite[TeaDataScript.teaDex.item[2].tea_recipe[0].ingredient_num];
            GameObject.Find("bubble_5").GetComponent<Image>().sprite = bubble_sprite[TeaDataScript.teaDex.item[2].tea_recipe[1].ingredient_num];

            //GameObject.Find("bubble_name4").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[num].tea_recipe[0].ingredient_num];
            //GameObject.Find("bubble_name5").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[num].tea_recipe[1].ingredient_num];

            GameObject.Find("bubble_num4").GetComponent<TextMeshProUGUI>().text = "x " + TeaDataScript.teaDex.item[2].tea_recipe[0].ingredient_amout.ToString() + "<b>↑</b>";
            GameObject.Find("bubble_num5").GetComponent<TextMeshProUGUI>().text = "x " + TeaDataScript.teaDex.item[2].tea_recipe[1].ingredient_amout.ToString() + "<b>↑</b>";

        }



        drinkStart();
    }

    public void drinkStart()
    {
        for (int i = 0; i < 9; i++)
        {
            drinkContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Button>().enabled = true;
        }
    }

    public void OnClickSpecial() //좌측 인덱스에서 스페셜 선택
    {
        ButtonSound._buttonInstance.onButtonAudio();

        teaIndex.GetComponent<Image>().sprite = TeaBg2; //배경 이미지(색깔 박스)
        juiceIndex.GetComponent<Image>().sprite = DrinkBg2;
        specialIndex.GetComponent<Image>().sprite = SpecialBg1;

        GameObject.Find("Recipe_title").GetComponent<TextMeshProUGUI>().text = TeaDataScript.teaDex.item[1].tea_name; //레시피 이름
        GameObject.Find("Recipe_detail_text").GetComponent<TextMeshProUGUI>().text = TeaDataScript.teaDex.item[1].tea_description; //레시피 상세설명

        for (int i = 0; i < 18; i++)
        {
            if (i == 1 || i == 13 || i == 14 || i == 15 || i == 16)
            {
                //GameObject.Find("item_" + i.ToString()).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = real_recipe[i];
            }
        }

        GameObject.Find("Recipe_image").GetComponent<Image>().sprite = real_recipe[1];

        if (!TeaDataScript.teaDex.item[1].own) //해당 레시피를 소유하고 있지 않다면
        {
            GameObject.Find("Recipe_content").transform.GetChild(8).gameObject.SetActive(true); //레시피 해금 전 미공개 이미지 띄우기
        }

        else
        {
            GameObject.Find("Recipe_content").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(4).gameObject.SetActive(true);
            GameObject.Find("Recipe_content").transform.GetChild(5).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(7).gameObject.SetActive(false);

            GameObject.Find("Ingredient_2").GetComponent<Image>().sprite = total_sprite[TeaDataScript.teaDex.item[1].tea_recipe[0].ingredient_num];

            GameObject.Find("bubble_2").GetComponent<Image>().sprite = bubble_sprite[TeaDataScript.teaDex.item[1].tea_recipe[0].ingredient_num];

            //GameObject.Find("bubble_name2").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

            GameObject.Find("bubble_num2").GetComponent<TextMeshProUGUI>().text = "x " + TeaDataScript.teaDex.item[1].tea_recipe[0].ingredient_amout.ToString() + "<b>↑</b>";

        }



        specialStart();
    }

    public void specialStart()
    {
        for (int i = 0; i < 5; i++)
        {
            specialContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Button>().enabled = true;
        }
    }
}
