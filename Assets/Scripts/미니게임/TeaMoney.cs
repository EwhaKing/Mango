using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeaMoney : MonoBehaviour
{

    public GameObject _gameObject;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Text>().text = TotalMoney.totalMoney.ToString();
        if (TotalMoney.totalMoney != 0)
            _gameObject.GetComponent<TextMeshProUGUI>().text = GetThousandCommaText(TotalMoney.totalMoney).ToString();
        else _gameObject.GetComponent<TextMeshProUGUI>().text = TotalMoney.totalMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }

}
