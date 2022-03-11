using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeNew : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
        for (int i=0; i<18; i++)
        {
            if (TeaDataScript.teaDex.item[i].is_read) //어떤 뉴 레시피가 존재한다면
            {
                if (TeaDataScript.teaDex.item[i].tea_type == 0) //tea일 때
                {
                    if (GameObject.Find("Viewport").transform.GetChild(0).gameObject.activeSelf == true) //Tea_Content가 활성화되어 있을 때
                    {
                        GameObject.Find("item_" + i.ToString()).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                    }
                    
                }

                else if (TeaDataScript.teaDex.item[i].tea_type == 1) //drink일 때
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
