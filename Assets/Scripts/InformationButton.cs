using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationButton : MonoBehaviour
{
    public void OnclickInform()
    {
        if (GameObject.Find("Button_information").transform.GetChild(0).gameObject.activeSelf == false)
        {
            GameObject.Find("Button_information").transform.GetChild(0).gameObject.SetActive(true);
        }

        else
        {
            GameObject.Find("Button_information").transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}