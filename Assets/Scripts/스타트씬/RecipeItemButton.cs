using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeItemButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickRecipeTea() //차 버튼 클릭
    {
        ButtonSound._buttonInstance.onButtonAudio(); //효과음

        int num = int.Parse(gameObject.name.Split("_"[0])[1]); //item_num 뒤에 num 불러오기
        showDetail(num); //버튼별 상세 설명
    }

    public void showDetail(int num) //레시피 버튼 클릭 시 상세 설명
    {
        GameObject.Find("Recipe_title").GetComponent<Text>().text = TeaDataScript.teaDex.item[num].tea_name; //레시피 이름
        GameObject.Find("Recipe_detail_text").GetComponent<Text>().text = TeaDataScript.teaDex.item[num].tea_name; //레시피 상세설명
        
        GameObject.Find("Recipe_image").GetComponent<Image>().sprite = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite; //레시피 상세 이미지

        Debug.Log("과일 몇 종류? " + TeaDataScript.teaDex.item[num].tea_recipe.Length);
        Debug.Log("무슨 과일? " + TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[num].tea_recipe[0].ingredient_num]);

        if (TeaDataScript.teaDex.item[num].tea_recipe.Length == 1)
        {
            GameObject.Find("Recipe_content").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(4).gameObject.SetActive(true);
            GameObject.Find("Recipe_content").transform.GetChild(5).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(7).gameObject.SetActive(false);

            GameObject.Find("bubble_name2").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[num].tea_recipe[0].ingredient_num];
            
            GameObject.Find("bubble_num2").GetComponent<Text>().text = " x" + TeaDataScript.teaDex.item[num].tea_recipe[0].ingredient_amout.ToString() + "↑";
        }

        else if (TeaDataScript.teaDex.item[num].tea_recipe.Length == 2)
        {
            GameObject.Find("Recipe_content").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(4).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(5).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(true);
            GameObject.Find("Recipe_content").transform.GetChild(7).gameObject.SetActive(true);

            GameObject.Find("bubble_name4").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[num].tea_recipe[0].ingredient_num];
            GameObject.Find("bubble_name5").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[num].tea_recipe[1].ingredient_num];

            GameObject.Find("bubble_num4").GetComponent<Text>().text = " x" + TeaDataScript.teaDex.item[num].tea_recipe[0].ingredient_amout.ToString() + "↑";
            GameObject.Find("bubble_num5").GetComponent<Text>().text = " x" + TeaDataScript.teaDex.item[num].tea_recipe[1].ingredient_amout.ToString() + "↑";
        }

        else
        {
            GameObject.Find("Recipe_content").transform.GetChild(3).gameObject.SetActive(true);
            GameObject.Find("Recipe_content").transform.GetChild(4).gameObject.SetActive(true);
            GameObject.Find("Recipe_content").transform.GetChild(5).gameObject.SetActive(true);
            GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(false);
            GameObject.Find("Recipe_content").transform.GetChild(7).gameObject.SetActive(false);

            GameObject.Find("bubble_name1").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[num].tea_recipe[0].ingredient_num];
            GameObject.Find("bubble_name2").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[num].tea_recipe[1].ingredient_num];
            GameObject.Find("bubble_name3").GetComponent<Text>().text = TeaDataScript.ingredient_name[TeaDataScript.teaDex.item[num].tea_recipe[2].ingredient_num];

            GameObject.Find("bubble_num1").GetComponent<Text>().text = " x" + TeaDataScript.teaDex.item[num].tea_recipe[0].ingredient_amout.ToString() + "↑";
            GameObject.Find("bubble_num2").GetComponent<Text>().text = " x" + TeaDataScript.teaDex.item[num].tea_recipe[1].ingredient_amout.ToString() + "↑";
            GameObject.Find("bubble_num3").GetComponent<Text>().text = " x" + TeaDataScript.teaDex.item[num].tea_recipe[2].ingredient_amout.ToString() + "↑";
        }

        /*for (int i=0; i<3; i++)
        {
            try
            {
                GameObject.Find("bubble_num" + (i + 1).ToString()).GetComponent<Text>().text = "x " + TeaDataScript.teaDex.item[num].tea_recipe[i].ingredient_amout.ToString() + "↑";
            }
            catch(System.IndexOutOfRangeException exception)
            {
                GameObject.Find("bubble_num" + (i + 1).ToString()).GetComponent<Text>().text = "x";
            }
        }*/

        //TeaDataScript.teaDex.item[recipe].tea_recipe.Length;
        //TeaDataScript.teaDex.item[recipe].tea_recipe[i].ingredient_num;

        /*if (!TeaDataScript.teaDex.item[num].own) //해당 레시피를 소유하고 있지 않다면
        {
            GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(true); //레시피 해금 전 미공개 이미지 띄우기
        }

        if (TeaManager.get_recipe != -1) //레시피를 새로 얻었다면
        {
            GameObject.Find("Recipe_Button").transform.GetChild(1).gameObject.SetActive(true); //item 버튼에 new 이미지 띄우기
        }*/
    }
}
