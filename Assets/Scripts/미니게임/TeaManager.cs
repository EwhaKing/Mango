using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeaManager : MonoBehaviour
{
    public const int TEA = 18; //총 차(주스) 개수
    public const int INDEX = 21; //총 과일 개수
    public const int NUM = 6; //각 과일 개수의 합

    public Text[] fruits_text = new Text[3]; //각각 과일의 수 텍스트 UI
    public Image[] fruits_image = new Image[3]; //각각 과일의 이미지
    public Sprite[] fruits_recipe_sprite = new Sprite[INDEX]; //각각 과일 레시피 이미지
    public Sprite[] fruits_sprite = new Sprite[INDEX]; //각각 반과일 이미지 UI 스프라이트
    public int[] fruits_num = new int[3]; //각각 과일의 개수 저장하는 배열
    public int[] fruits_index = { -1, -1, -1}; //각 과일 순서에 과일 인덱스 저장
    public int finish_condition; //종료조건 검사하기 위해서
    public int fruit_current = 0; //현재 과일 인덱스

    int[] tea_prob = new int[TEA]; //차 레시피 확률
    public Image fruits_mini; //반 자른 과일 오브젝트
    public int[] success_num = new int[3]; //각각 과일마다 방울 성공한 개수 (리듬바.cs에서 증가)
    int recipe = 0; //뜨게 할 레시피(과일 조합)

    public GameObject button_touch;
    public GameObject lemonCircle;
    public Sprite[] circleSprite = new Sprite[INDEX];

    private int finish = 0; //과일 하나 완료된거 판단하기 위한 변수 (방울 3개)

    public Image left_arm, right_arm, jamong_left_arm, jamong_right_arm;

    public Image clear_img;

    public static int get_recipe = -1; //얻은 레시피 번호

    // Start is called before the first frame update
    void Start()
    {
        left_arm.GetComponent<Animator>().enabled = false;
        right_arm.GetComponent<Animator>().enabled = false;
        jamong_left_arm.GetComponent<Animator>().enabled = false;
        jamong_right_arm.GetComponent<Animator>().enabled = false;

        finish_condition = NUM; //처음에 각 과일 개수 합으로 초기화 한 후, 0이 되면 속게임 종료

        for(int i = 0; i < 3; i++) //처음 과일 3개 이미지 안보이게(1개나 2개일 수도 있기때문)
        {
            fruits_image[i].gameObject.SetActive(false); 
        }

        //레시피 랜덤으로 과일 최대 3개 뜨게 하기
        randomRecipe();
    }

    public void randomRecipe()
    {
        //스페셜 옷인지 아닌지 검사
        bool is_special = false;

        //현재 아기가 입고 있는 옷이 스페셜 옷인지 먼저 판단 (1: 꽃무늬옷(꽃)_13, 2: 트레이닝복(닭가슴살)_16, 7:꿀벌(꿀)_1, 8:곰돌이(쑥&마늘)_14,15)
        //스페셜 차들은 0%이다가 옷 잎으면 60% 확률로 뜨도록, 나머지 차들 3%로 뜸
        //나머지 차들 8%확률로 각각 뜨도록
        //망고(17)뜨는확률 항상 4%
        tea_prob[17] = 4;

        if (GameStaticData.data.data_cloth == 1) //꽃무늬 옷
        {
            is_special = true;
            tea_prob[13] = 60;
        }
        else if(GameStaticData.data.data_cloth == 2) //트레이닝 복
        {
            is_special = true;
            tea_prob[16] = 60;
        }
        else if(GameStaticData.data.data_cloth == 7) //꿀벌 옷
        {
            is_special = true;
            tea_prob[1] = 60;
        }
        else if(GameStaticData.data.data_cloth == 8) //곰돌이 옷
        {
            is_special = true;
            tea_prob[14] = 30;
            tea_prob[15] = 30;
        }
        else
        {
            if (is_special)
            {
                for(int i = 0; i < TEA; i++)
                {
                    if (tea_prob[i] != 0) continue;
                    tea_prob[i] = 3;
                }
            }
            else
            {
                for(int i = 0; i < 13; i++) //스페셜 전까지
                {
                    tea_prob[i] = 8;
                }
                tea_prob[1] = 0; //스페셜 레시피 확률 0으로
            }
        }

        for(int i = 1; i < TEA; i++) //랜덤함수에 따라 확률 정하기 위해 누적합
        {
            tea_prob[i] += tea_prob[i - 1];
        }

        //랜덤함수로 1~100중 난수 구하기
        int rand = Random.Range(0, 100);

        //무슨 레시피 뜨게 하는지 결정
        for (int i = 0; i < TEA; i++)
        {
            if(rand < tea_prob[i]) //결정!
            {
                recipe = i;
                break;
            }
        }

        Debug.Log("뜨는 레시피 번호: " + recipe);
        Debug.Log("레시피에 있는 과일 개수: " + TeaDataScript.teaDex.item[recipe].tea_recipe.Length);

        //몇번째 과일인지 저장하고 과일 나타내기
        for(int i = 0; i < TeaDataScript.teaDex.item[recipe].tea_recipe.Length; i++)
        {
            int index = TeaDataScript.teaDex.item[recipe].tea_recipe[i].ingredient_num;
            fruits_index[i] = index; //몇번째 과일인지 저장
            fruits_image[i].gameObject.SetActive(true);
            fruits_image[i].sprite = fruits_recipe_sprite[index];
        }
        //레시피마다 과일 개수 정하기
        switch (recipe)
        {
            case 0:
                fruits_num[0] = NUM;
                break;
            case 1:
                fruits_num[0] = NUM;
                break;
            case 2:
                fruits_num[0] = NUM/2;
                fruits_num[1] = NUM/2;
                break;
            case 3:
                fruits_num[0] = Random.Range(1, 3);
                fruits_num[1] = Random.Range(1, 3);
                fruits_num[2] = NUM - (fruits_num[0] + fruits_num[1]);
                break;
            case 4:
                fruits_num[0] = Random.Range(1, 3);
                fruits_num[1] = 2;
                fruits_num[2] = NUM - (fruits_num[0] + fruits_num[1]);
                break;
            case 5:
                fruits_num[2] = 1;
                fruits_num[0] = Random.Range(2, 4);
                fruits_num[1] = NUM - (fruits_num[0] + fruits_num[2]);
                break;
            case 6:
                fruits_num[1] = Random.Range(1, 3);
                fruits_num[2] = Random.Range(1, 3);
                fruits_num[0] = NUM - (fruits_num[1] + fruits_num[2]);
                break;
            case 7:
                fruits_num[0] = NUM / 2;
                fruits_num[1] = NUM / 2;
                break;
            case 8:
                fruits_num[0] = NUM / 2;
                fruits_num[1] = NUM / 2;
                break;
            case 9:
                fruits_num[0] = 1;
                fruits_num[1] = 2;
                fruits_num[2] = 3;
                break;
            case 10:
                fruits_num[0] = Random.Range(1, 3);
                fruits_num[1] = Random.Range(1, 3);
                fruits_num[2] = NUM - (fruits_num[0] + fruits_num[1]);
                break;
            case 11:
                fruits_num[0] = Random.Range(2, 4);
                fruits_num[1] = 1;
                fruits_num[2] = NUM - (fruits_num[0] + fruits_num[1]);
                break;
            case 12:
                fruits_num[0] = Random.Range(2, 4);
                fruits_num[1] = Random.Range(1, 3);
                fruits_num[2] = NUM - (fruits_num[0] + fruits_num[1]);
                break;
            case 13:
                fruits_num[0] = NUM;
                break;
            case 14:
                fruits_num[0] = NUM/3;
                fruits_num[1] = (NUM/3)*2;
                break;
            case 15:
                fruits_num[0] = NUM / 3;
                fruits_num[1] = (NUM / 3) * 2;
                break;
            case 16:
                fruits_num[1] = NUM / 3;
                fruits_num[0] = (NUM / 3) * 2;
                break;
            case 17:
                fruits_num[0] = NUM;
                break;
        }
        //레시피에 과일 개수 텍스트에 표시
        for(int i = 0; i < 3; i++)
        {
            if (fruits_index[i] == -1)
                break;
            fruits_text[i].text = fruits_num[i].ToString();
        }
        //첫 과일 표시
        fruit_current = 0;
        changeFruit();
    }

    public void changeFruit() //fruit_current 로 과일 바꾸기
    {
        //아기 팔 바꾸기 (자몽만 팔 달라서 필요한 코드)
        GameObject.Find("BabyCustom").GetComponent<BabyCustom>().changeBabyCustom(GameStaticData.data.data_cloth);

        //아기가 들고있는 과일 이미지 바꾸기
        fruits_mini.sprite = fruits_sprite[fruits_index[fruit_current]];
        
        if(fruits_index[fruit_current] == 1) //자몽이라면, 자몽이 너비가 넓어서 아기 팔의 위치를 조금 다르게 설정
        {
            left_arm.gameObject.SetActive(false);
            right_arm.gameObject.SetActive(false);
            jamong_left_arm.gameObject.SetActive(true);
            jamong_right_arm.gameObject.SetActive(true);
        }
        else
        {
            left_arm.gameObject.SetActive(true);
            right_arm.gameObject.SetActive(true);
            jamong_left_arm.gameObject.SetActive(false);
            jamong_right_arm.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(finish == 3 && finish_condition != 0)
        {
            finish_condition--;
            finish = 0;
            if(fruits_num[fruit_current] > 0)
            {
                fruits_num[fruit_current]--;
                fruits_text[fruit_current].text = fruits_num[fruit_current].ToString();
                if(fruits_num[fruit_current] == 0)
                {
                    fruit_current++;
                    if (fruit_current < 3)
                    {
                        changeFruit();
                    }
                }
            }
         
        }

        //6개 과일 모두 만든 경우
        if (finish_condition == 0)
        {
            //ButtonSound._buttonInstance.onBabyAudio();
            button_touch.SetActive(false);
            GameObject.Find("TimeSlider").GetComponent<SliderTimer>().enabled = false;
            Invoke("gameClear", 0.5f);
        }
    }

    public void gameClear()
    {
        GameObject.Find("Image_line").GetComponent<RhythmBar>().enabled = false;
        if (GameObject.Find("Image_babyarm_left")) GameObject.Find("Image_babyarm_left").GetComponent<Animator>().enabled = false;
        else GameObject.Find("Image_babyarm_left_jamong").GetComponent<Animator>().enabled = false;
        if (GameObject.Find("Image_babyarm_right")) GameObject.Find("Image_babyarm_right").GetComponent<Animator>().enabled = false;
        else GameObject.Find("Image_babyarm_right_jamong").GetComponent<Animator>().enabled = false;
        clear_img.gameObject.SetActive(true);
        Invoke("mainLoad", 0.2f);
    }

    public void mainLoad()
    {
        checkIsRecipeTrue(); //도감 얻었는지 확인하는
        SceneManager.LoadScene("maingameScene");
    }

    //도감 얻었는지 확인하는
    public void checkIsRecipeTrue()
    {
        bool check = true;
        for (int i = 0; i < TeaDataScript.teaDex.item[recipe].tea_recipe.Length; i++)
        {
            int cnt = TeaDataScript.teaDex.item[recipe].tea_recipe[i].ingredient_amout;
            if(success_num[i] < cnt) //하나라도 만족 못하면
            {
                check = false;
                break;
            }
        }
        if (check && TeaDataScript.teaDex.item[recipe].own == false) //기존에 없고 조건 만족 -> 도감 얻음
        {
            Debug.Log("레시피 획득! " + recipe);

            get_recipe = recipe;
            TeaDataScript.teaDex.item[recipe].own = true;
        }
    }

    //특정영역 터치 시 수행: 아기 팔 모션 애니메이션, 과일 바꾸기 등과 관련, 과일 방울 떨어뜨리기
    public void OnClickBaby_TeaManager()
    {
        lemonCircle.GetComponent<Image>().sprite = circleSprite[fruit_current];
        lemonCircle.transform.localPosition = new Vector3(163f, -294f, 0f);
        lemonCircle.SetActive(true);

        if (fruit_current == 1) //자몽이라면
        {
            if (jamong_left_arm.GetComponent<Animator>().isActiveAndEnabled == false)
            {
                jamong_left_arm.GetComponent<Animator>().enabled = true;
                jamong_right_arm.GetComponent<Animator>().enabled = true;
            }
            else
            {
                jamong_left_arm.GetComponent<Animator>().Play("arm_left_anim_jamong", -1, 0f);
                jamong_right_arm.GetComponent<Animator>().Play("arm_right_anim_jamong", -1, 0f);
            }
        }
        else //레몬, 꿀이라면
        {
            if (left_arm.GetComponent<Animator>().isActiveAndEnabled == false)
            {
                left_arm.GetComponent<Animator>().enabled = true;
                right_arm.GetComponent<Animator>().enabled = true;
            }
            else
            {
                left_arm.GetComponent<Animator>().Play("arm_left_anim", -1, 0f);
                right_arm.GetComponent<Animator>().Play("arm_right_anim_lemon", -1, 0f);
            }
        }
        finish++; //터치 판단

        button_touch.SetActive(false);
        Invoke("button_touch_true", 0.5f);
    }

    void button_touch_true()
    {
        button_touch.SetActive(true);
    }
}
