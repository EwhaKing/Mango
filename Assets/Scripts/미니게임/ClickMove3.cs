using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickMove3 : MonoBehaviour
{
    //얻은 돈 표시하는 스크립트

    public GameObject _gameObject;

    // Start is called before the first frame update
    void Start()
    {
        if (TotalMoney.plusMoney < 100)
        {
            _gameObject.GetComponent<TextMeshProUGUI>().text = "+        " + TotalMoney.plusMoney.ToString();
        }

        else
        {
            _gameObject.GetComponent<TextMeshProUGUI>().text = "+         " + TotalMoney.plusMoney.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
