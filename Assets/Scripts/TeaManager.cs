using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeaManager : MonoBehaviour
{
    public const int INDEX = 3;
    public Text[] fruits_text = new Text[INDEX];
    public Image[] fruits_image = new Image[INDEX];
    public int[] fruit_num = new int[INDEX];
    public int num = 6; //총 과일 합의 개수
    private int first_fruit;

    // Start is called before the first frame update
    void Start()
    { 
        for(int i = 0; i < INDEX; i++)
        {
            if (i == INDEX - 1) fruit_num[i] = num;
            else fruit_num[i] = Random.Range(0, num + 1);
            num -= fruit_num[i];
            fruits_text[i].text = fruit_num[i].ToString();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
