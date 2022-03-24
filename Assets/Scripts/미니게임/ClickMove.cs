using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClickMove : MonoBehaviour
{
    public int speed = 200;
    public GameObject credit_bg;

    //AudioSource audioSource;
    //public AudioClip CoinUp;

    // Start is called before the first frame update
    void Start()
    {
        //this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float yMove = speed * Time.deltaTime; //속도 설정
        this.transform.Translate(new Vector3(0, yMove, 0));
        
        //사라지는 코드는 CustomerTalkButton에 있습니다!

        
    }

    public void onClickCredit()
    {
        credit_bg.transform.localPosition = new Vector2(0f, 0f);
    }

    public void onClickCopyRight()
    {
        credit_bg.GetComponent<ClickMove>().enabled = true;
    }

    public void onClickCopyRightStop()
    {
        if (credit_bg.GetComponent<ClickMove>().enabled)
        {
            credit_bg.GetComponent<ClickMove>().enabled = false;
        }
        else
        {
            credit_bg.GetComponent<ClickMove>().enabled = true;
        }
    }

}
