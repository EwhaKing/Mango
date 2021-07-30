using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaMoney : MonoBehaviour
{
    // 돈 합계 계산 스크립트
    // 씬 2개에 모두 넣어야 하지 않을까?

    private int plusMoney = 0;

    static int totalMoney = 0;

    public GameObject gameObject; 

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Text>().text = totalMoney;
    }

    // Update is called once per frame
    void Update()
    {
        if (success_count == 18)
        {
            plusMoney = 500;
        }

        else if (success_count == 17)
        {
            plusMoney = 300;
        }

        else if (success_count == 16)
        {
            plusMoney = 200;
        }

        else if (success_count == 15)
        {
            plusMoney = 100;
        }

        else if (success_count == 0)
        {
            plusMoney = 0;
        }

        else
        {
            plusMoney = 50;
        }

        totalMoney += plusMoney;

        gameObject.GetComponent<Text>().text = totalMoney;
    }


}
