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

    public void onClickRecipe() //레시피 버튼 클릭 시 상세설명
    {
        //효과음
        ButtonSound._buttonInstance.onButtonAudio();

        //현재 gameObject == item
        int index = gameObject.transform.GetSiblingIndex(); //item 몇번째 자식인지
        int recipe_sprite_num = GameObject.Find("GameManager").GetComponent<RecipeManager>().recipe_sprites[index]; //고른 레시피의 스프라이트 넘버
        //GameObject.Find("BabyCustom").GetComponent<BabyCustom>().changeBabyCustom(clothe_sprite_num); //아기 옷 미리보기

        //변수 설정
        string recipe_title_text = TeaDataScript.teaDex.item[recipe_sprite_num].tea_name; //레시피 제목
        string recipe_detail_text = TeaDataScript.teaDex.item[recipe_sprite_num].tea_name; //레시피 상세 설명 - 현재 제목으로 대체
        //Sprite recipe_image = TeaDataScript.teaDex.item[recipe_sprite_num].tea_image; //레시피 상세 이미지 - 우선 주석 처리
        //int fruit_amount_text = TeaDataScript.teaDex.item[recipe_sprite_num].tea_recipe[recipe_sprite_num].ingredient_amout; //레시피 상세 과일 방울 수 - 수정 필요함 잘못 짰음
        //Sprite fruit_image = TeaDataScript.teaDex.item[recipe_sprite_num].tea_recipe[recipe_sprite_num].ingredient_num; //레시피 상세 과일 이미지 - 수정 필요함 잘못 짰음

        //버튼 눌렀을 때 좌측 설명 및 이미지 바뀌게
        GameObject.Find("Recipe_title").GetComponent<Text>().text = recipe_title_text;
        GameObject.Find("Recipe_detail_text").GetComponent<Text>().text = recipe_detail_text;
        //GameObject.Find("Recipe_image").GetComponent<SpriteRenderer>().sprite = recipe_image;
        //GameObject.Find("bubble_num1").GetComponent<Text>().text = "x " + fruit_amount_text.ToString() + "↑"; //수정 필요함 잘못 짰음
        //GameObject.Find("bubble_num2").GetComponent<Text>().text = "x " + fruit_amount_text.ToString() + "↑"; //수정 필요함 잘못 짰음
        //GameObject.Find("bubble_num3").GetComponent<Text>().text = "x " + fruit_amount_text.ToString() + "↑"; //수정 필요함 잘못 짰음
        //GameObject.Find("bubble_1").GetComponent<SpriteRenderer>().sprite = fruit_image; //수정 필요함 잘못 짰음
        //GameObject.Find("bubble_2").GetComponent<SpriteRenderer>().sprite = fruit_image; //수정 필요함 잘못 짰음
        //GameObject.Find("bubble_3").GetComponent<SpriteRenderer>().sprite = fruit_image; //수정 필요함 잘못 짰음

        if (!TeaDataScript.teaDex.item[recipe_sprite_num].own) //해당 레시피를 소유하고 있지 않다면
        {
            GameObject.Find("Recipe_content").transform.GetChild(6).gameObject.SetActive(true); //레시피 해금 전 미공개 이미지 띄우기
        }

        if (TeaManager.get_recipe != -1) //레시피를 새로 얻었다면
        {
            GameObject.Find("Recipe_Button").transform.GetChild(1).gameObject.SetActive(true); //item 버튼에 new 이미지 띄우기
        }

    }
}
