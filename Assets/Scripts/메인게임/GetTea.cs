using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GetTea : MonoBehaviour
{
    const int TEA_NUM = 18;

    public GameObject foundRecipe;
    public Image tea;

    public Sprite[] tea_sprites = new Sprite[TEA_NUM];

    //0으로 초기화된 뉴 레시피 확인 배열
    public static int[] what_recipe = Enumerable.Repeat<int>(0, 18).ToArray<int>();

    // Start is called before the first frame update
    void Start()
    {
        if(TeaManager.get_recipe != -1) //새로운 도감 얻음
        {
            tea.sprite = tea_sprites[TeaManager.get_recipe];

            //도감 얻었다는 팝업창 뜨도록
            foundRecipe.SetActive(true);

            //팝업창 텍스트 구성
            foundRecipe.transform.GetChild(0).GetComponent<Text>().text = "'" + TeaDataScript.teaDex.item[TeaManager.get_recipe].tea_name +
                "' 레시피를 획득하였습니다!";

            //효과음
            ButtonSound._buttonInstance.onPopUpAudio();

            //출력
            Debug.Log("얻은 레시피: " + TeaManager.get_recipe);

            //스타트씬으로 변수 가져가기
            what_recipe[TeaManager.get_recipe] = 1;

            TeaManager.get_recipe = -1; //초기화
        }
    }
}
