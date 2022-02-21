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

    public void recipeStart()
    {
        int cnt = 0;

        int recipe_cnt = TeaDataScript.teaDex.item.Length; //현재 존재하는 레시피 개수

        //레시피 뜨도록
        for (int i = 0; i < recipe_cnt; i++)
        {
            if (TeaDataScript.teaDex.item[i].own == true) //해당 레시피를 가지고 있다면 ~추후 수정
            {
                GameObject item;

                if (cnt == 0) //처음이라면 _item 사용해야함
                {
                    _item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = set_recipe[i];
                    recipe_sprites.Add(i);
                    item = _item;
                }

                else
                {
                    item = GameObject.Instantiate(_item) as GameObject;
                    item.name = "item" + cnt.ToString();
                    item.transform.SetParent(_item.transform.parent);
                    item.transform.localScale = Vector3.one;
                    item.transform.localRotation = Quaternion.identity;

                    item.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = set_recipe[i];
                    recipe_sprites.Add(i);
                }

                cnt++;
            }
        }

    }

    public void OnClickDrink()
    {
        recipeDetailBg.GetComponent<Image>().sprite = DrinkBg;
    }

    public void OnClickTea()
    {
        recipeDetailBg.GetComponent<Image>().sprite = TeaBg;
    }

    public void OnClickSpecial()
    {
        recipeDetailBg.GetComponent<Image>().sprite = SpecialBg;
    }
}
