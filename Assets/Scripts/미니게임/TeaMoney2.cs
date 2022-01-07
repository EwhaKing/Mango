using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeaMoney2 : MonoBehaviour
{
    //AudioSource audioSource;
    //public AudioClip CoinUp;
    
    public GameObject _gameObject;

    
    



    // Start is called before the first frame update
    void Start()
    {
        //audioSource = this.gameObject.GetComponent<AudioSource>();

        //gameObject.GetComponent<Text>().text = GetThousandCommaText(TotalMoney.totalMoney).ToString();

        //gameObject.GetComponent<Text>().text = TotalMoney.totalMoney.ToString();
        if (TotalMoney.totalMoney != 0)
            _gameObject.GetComponent<Text>().text = GetThousandCommaText(TotalMoney.totalMoney).ToString();
        else _gameObject.GetComponent<Text>().text = TotalMoney.totalMoney.ToString();

        //audioSource.clip = CoinUp;
        //audioSource.Play();

        //TeaMoney.totalMoney += TeaMoney.plusMoney;
        //gameObject.GetComponent<Text>().text = TeaMoney.totalMoney.ToString();
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
