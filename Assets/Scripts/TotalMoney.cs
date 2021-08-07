using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalMoney : MonoBehaviour
{
    public static int plusMoney = 0;

    public static int totalMoney = 0;

    //public GameObject gameObject;


    // Start is called before the first frame update
    void Start()
    {
        if (RhythmBar.success_count == 18)
        {
            plusMoney = 500;
        }

        else if (RhythmBar.success_count == 17)
        {
            plusMoney = 300;
        }

        else if (RhythmBar.success_count == 16)
        {
            plusMoney = 200;
        }

        else if (RhythmBar.success_count == 15)
        {
            plusMoney = 100;
        }

        else if (RhythmBar.success_count == 0)
        {
            plusMoney = 0;
        }

        else
        {
            plusMoney = 50;
        }

        totalMoney += plusMoney;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
