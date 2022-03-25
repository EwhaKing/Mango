using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetTea : MonoBehaviour
{
    const int TEA_NUM = 18;

    public GameObject foundRecipe;
    public Image tea;

    public Sprite[] tea_sprites = new Sprite[TEA_NUM];

    //난이도에 따른 레시피 팁 저장
    int[] tip = { 0, 100, 150, 250, 350, 800 };

    // Start is called before the first frame update
    void Start()
    {
        if(TeaManager.get_recipe != -1) //새로운 도감 얻음
        {
            //보너스 추가
            CustomerManager.tip_money[CustomerManager.current_customer] += tip[GameStaticData.data.difficulty];

            //도감 얻었다는 팝업창 뜨도록
            foundRecipe.SetActive(true);

            tea.sprite = tea_sprites[TeaManager.get_recipe]; //이미지 보이게!

            //팝업창 텍스트 구성
            foundRecipe.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "'" + TeaDataScript.teaDex.item[TeaManager.get_recipe].tea_name +
                "' 레시피를 획득하였습니다!";

            //효과음
            ButtonSound._buttonInstance.onPopUpAudio();

            //출력
            Debug.Log("얻은 레시피: " + TeaManager.get_recipe);

            //레시피 new 띄우도록
            TeaDataScript.teaDex.item[TeaManager.get_recipe].is_read = true;
            File.WriteAllText(Application.persistentDataPath + "/TeaDex.json", JsonUtility.ToJson(TeaDataScript.teaDex));

            TeaManager.get_recipe = -1; //초기화
        }
        else if(TeaManager.origin_recipe != -1) //있는 도감 만듦
        {
            //보너스 추가
            CustomerManager.tip_money[CustomerManager.current_customer] += tip[GameStaticData.data.difficulty];

            Debug.Log("만든 레시피: " + TeaManager.origin_recipe);

            tea.sprite = tea_sprites[TeaManager.origin_recipe];

            TeaManager.origin_recipe = -1; //초기화
        }
    }
}
