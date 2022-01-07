using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMoney : MonoBehaviour
{
    //게임의 전체 돈을 나타내주는 스크립트
    public Text money_text;
    int money;

    // Start is called before the first frame update
    void Start()
    {
        money = GameObject.Find("GameData").GetComponent<GameStaticData>().game_money;
        if (money != 0)
            money_text.text = GetThousandCommaText(money).ToString();
        else money_text.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        money = GameObject.Find("GameData").GetComponent<GameStaticData>().game_money;
        if (money != 0)
            money_text.text = GetThousandCommaText(money).ToString();
        else money_text.text = money.ToString();
    }

    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
}
