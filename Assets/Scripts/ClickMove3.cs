using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMove3 : MonoBehaviour
{
    public GameObject _gameObject;

    // Start is called before the first frame update
    void Start()
    {
        if (TotalMoney.plusMoney < 100)
        {
            _gameObject.GetComponent<Text>().text = "+      " + TotalMoney.plusMoney.ToString();
        }

        else
        {
            _gameObject.GetComponent<Text>().text = "+       " + TotalMoney.plusMoney.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
