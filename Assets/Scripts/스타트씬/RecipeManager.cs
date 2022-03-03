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

    public void OnClickTea() //좌측 인덱스에서 차 선택
    {
        ButtonSound._buttonInstance.onButtonAudio();
        recipeDetailBg.GetComponent<Image>().sprite = TeaBg; //배경 이미지(색깔 박스)

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
