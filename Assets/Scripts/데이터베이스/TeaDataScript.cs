using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class TeaDex
{
    public TeaData[] item = new TeaData[18];
}

[System.Serializable]
public class TeaData
{
    public int tea_num;
    public string tea_name;
    public int tea_type;
    public string tea_description;
    public IngredientData[] tea_recipe;
    public bool own;

    public TeaData (int num, string name, int type, string description, IngredientData[] recipe, bool own)
    {
        this.tea_num = num;
        this.tea_name = name;
        this.tea_type = type;
        this.tea_description = description;
        this.tea_recipe = recipe;
        this.own = own;
    }
}

[System.Serializable]

public class IngredientData
{
    public int ingredient_num;
    public int ingredient_amout;

    public IngredientData(int num, int amount)
    {
        this.ingredient_num = num;
        this.ingredient_amout = amount;
    }
}

public class TeaDataScript : MonoBehaviour
{
    public static TeaDex teaDex = new TeaDex();

    //teaDex.json 속의 ingredientdata 속의 ingredient_num이 어떤 과일인지 알 수 있는 string 배열
    public static string[] ingredient_name = { "레몬", "자몽", "딸기", "바나나", "수박", "파인애플", "키위", "블루베리", "코코넛", "메론", "홍차", "민트", "녹차", "꿀", "초코시럽", "우유", "쑥", "마늘", "닭가슴살", "꽃", "망고"};
}
