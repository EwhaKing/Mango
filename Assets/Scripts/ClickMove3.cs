using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMove3 : MonoBehaviour
{
    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        if (TeaMoney.plusMoney < 100)
        {
            gameObject.GetComponent<Text>().text = "+      " + TeaMoney.plusMoney.ToString();
        }

        else
        {
            gameObject.GetComponent<Text>().text = "+       " + TeaMoney.plusMoney.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
