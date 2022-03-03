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

    public GameObject teaContent;
    public GameObject drinkContent;
    public GameObject specialContent;

    public Sprite[] total_sprite = new Sprite[21]; //과일 이미지
    public Sprite[] bubble_sprite = new Sprite[21]; //방울 이미지

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
        GameObject.Find("Recipe_title").GetComponent<Text>().text = TeaDataScript.teaDex.item[0].tea_name; //레시피 이름
        GameObject.Find("Recipe_detail_text").GetComponent<Text>().text = TeaDataScript.teaDex.item[0].tea_description; //레시피 상세설명

        GameObject.Find("Recipe_content").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Recipe_content").transform.GetChild(4).gameObject.SetActive(true);
        GameObject.Find("Recipe_content").transform.GetChild(5).gameObject.SetActive(false);
        GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(false);
        GameObject.Find("Recipe_content").transform.GetChild(7).gameObject.SetActive(false);

        GameObject.Find("Ingredient_2").GetComponent<Image>().sprite = total_sprite[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

        GameObject.Find("bubble_2").GetComponent<Image>().sprite = bubble_sprite[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

        //GameObject.Find("bubble_name2").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

        GameObject.Find("bubble_num2").GetComponent<Text>().text = "x " + TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_amout.ToString() + "<b>↑</b>";
    }

    public void OnClickTea() //좌측 인덱스에서 차 선택
    {
        ButtonSound._buttonInstance.onButtonAudio();
        recipeDetailBg.GetComponent<Image>().sprite = TeaBg; //배경 이미지(색깔 박스)

        GameObject.Find("Recipe_title").GetComponent<Text>().text = TeaDataScript.teaDex.item[0].tea_name; //레시피 이름
        GameObject.Find("Recipe_detail_text").GetComponent<Text>().text = TeaDataScript.teaDex.item[0].tea_description; //레시피 상세설명

        GameObject.Find("Recipe_content").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Recipe_content").transform.GetChild(4).gameObject.SetActive(true);
        GameObject.Find("Recipe_content").transform.GetChild(5).gameObject.SetActive(false);
        GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(false);
        GameObject.Find("Recipe_content").transform.GetChild(7).gameObject.SetActive(false);

        GameObject.Find("Ingredient_2").GetComponent<Image>().sprite = total_sprite[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

        GameObject.Find("bubble_2").GetComponent<Image>().sprite = bubble_sprite[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

        //GameObject.Find("bubble_name2").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

        GameObject.Find("bubble_num2").GetComponent<Text>().text = "x " + TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_amout.ToString() + "<b>↑</b>";

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
        recipeDetailBg.GetComponent<Image>().sprite = DrinkBg; //배경 이미지(색깔 박스)

        GameObject.Find("Recipe_title").GetComponent<Text>().text = TeaDataScript.teaDex.item[2].tea_name; //레시피 이름
        GameObject.Find("Recipe_detail_text").GetComponent<Text>().text = TeaDataScript.teaDex.item[2].tea_description; //레시피 상세설명

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

        GameObject.Find("bubble_num4").GetComponent<Text>().text = "x " + TeaDataScript.teaDex.item[2].tea_recipe[0].ingredient_amout.ToString() + "<b>↑</b>";
        GameObject.Find("bubble_num5").GetComponent<Text>().text = "x " + TeaDataScript.teaDex.item[2].tea_recipe[1].ingredient_amout.ToString() + "<b>↑</b>";

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
        recipeDetailBg.GetComponent<Image>().sprite = SpecialBg; //배경 이미지(색깔 박스)

        GameObject.Find("Recipe_title").GetComponent<Text>().text = TeaDataScript.teaDex.item[1].tea_name; //레시피 이름
        GameObject.Find("Recipe_detail_text").GetComponent<Text>().text = TeaDataScript.teaDex.item[1].tea_description; //레시피 상세설명

        GameObject.Find("Recipe_content").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Recipe_content").transform.GetChild(4).gameObject.SetActive(true);
        GameObject.Find("Recipe_content").transform.GetChild(5).gameObject.SetActive(false);
        GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(false);
        GameObject.Find("Recipe_content").transform.GetChild(7).gameObject.SetActive(false);

        GameObject.Find("Ingredient_2").GetComponent<Image>().sprite = total_sprite[TeaDataScript.teaDex.item[1].tea_recipe[0].ingredient_num];

        GameObject.Find("bubble_2").GetComponent<Image>().sprite = bubble_sprite[TeaDataScript.teaDex.item[1].tea_recipe[0].ingredient_num];

        //GameObject.Find("bubble_name2").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[0].tea_recipe[0].ingredient_num];

        GameObject.Find("bubble_num2").GetComponent<Text>().text = "x " + TeaDataScript.teaDex.item[1].tea_recipe[0].ingredient_amout.ToString() + "<b>↑</b>";

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
