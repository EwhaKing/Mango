using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeIndexNew : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Recipe_list_title1").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Recipe_list_title2").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Recipe_list_title3").transform.GetChild(0).gameObject.SetActive(false);

        int[] A = new int[] { 0, 5, 8, 11 };
        int[] B = new int[] { 2, 3, 4, 6, 7, 9, 10, 12, 17 };
        int[] C = new int[] { 1, 13, 14, 15, 16 };

        for (int i = 0; i < 4; i++)
        {
            
            if (TeaDataScript.teaDex.item[A[i]].is_read == true)
            {
                Debug.Log("티타입 0일 때 i는 " + A[i]);
                GameObject.Find("Recipe_list_title1").transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        for (int j = 0; j < 9; j++)
        {
  
            if (TeaDataScript.teaDex.item[B[j]].is_read == true)
            {
                Debug.Log("티타입 1일 때 j는 " + B[j]);
                GameObject.Find("Recipe_list_title2").transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        for (int k = 0; k < 5; k++)
        {
            
            if (TeaDataScript.teaDex.item[C[k]].is_read == true)
            {
                Debug.Log("티타입 2일 때 k는 " + C[k]);
                GameObject.Find("Recipe_list_title3").transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newIndex()
    {
        GameObject.Find("Recipe_list_title1").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Recipe_list_title2").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Recipe_list_title3").transform.GetChild(0).gameObject.SetActive(false);

        int[] A = new int[] { 0, 5, 8, 11 };
        int[] B = new int[] { 2, 3, 4, 6, 7, 9, 10, 12, 17 };
        int[] C = new int[] { 1, 13, 14, 15, 16 };

        for (int i = 0; i < 4; i++)
        {

            if (TeaDataScript.teaDex.item[A[i]].is_read == true)
            {
                Debug.Log("티타입 0일 때 i는 " + A[i]);
                GameObject.Find("Recipe_list_title1").transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        for (int j = 0; j < 9; j++)
        {

            if (TeaDataScript.teaDex.item[B[j]].is_read == true)
            {
                Debug.Log("티타입 1일 때 j는 " + B[j]);
                GameObject.Find("Recipe_list_title2").transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        for (int k = 0; k < 5; k++)
        {

            if (TeaDataScript.teaDex.item[C[k]].is_read == true)
            {
                Debug.Log("티타입 2일 때 k는 " + C[k]);
                GameObject.Find("Recipe_list_title3").transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
