using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeNew : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("레시피 획득 후" + TeaDataScript.teaDex.item[0].own);

        for (int i=0; i<18; i++)
        {
            if (GetTea.what_recipe[i] == 1) //어떤 뉴 레시피가 존재한다면
            {
                if (i == 0 || i== 5 || i==8 || i == 11) //tea일 때
                {
                    if (GameObject.Find("Viewport").transform.GetChild(0).gameObject.activeSelf == true) //Tea_Content가 활성화되어 있을 때
                    {
                        GameObject.Find("item_" + i.ToString()).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                    }
                    
                }

                else if (i == 2 || i == 3 || i == 4 || i == 6 || i == 7 || i == 9 || i == 10 || i == 12 || i == 17) //drink일 때
                {
                    if (GameObject.Find("Viewport").transform.GetChild(1).gameObject.activeSelf == true) //Drink_Content가 활성화되어 있을 때
                    {
                        GameObject.Find("item_" + i.ToString()).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                    }
                }

                else //special일 떄
                {
                    if (GameObject.Find("Viewport").transform.GetChild(2).gameObject.activeSelf == true)
                    {
                        GameObject.Find("item_" + i.ToString()).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                    }
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
