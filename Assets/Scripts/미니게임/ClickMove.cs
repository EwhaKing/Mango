using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClickMove : MonoBehaviour
{
    int speed = 200;

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
        //Invoke("Bye", 0.2f); 
        //마지막에 0.1초 정도 멈춰있는걸로 수정했었는데 그냥 움직이다 사라지는게 더 자연스러운 것 같아 다시 주석처리했습니다. 
        //사라지는 코드는 CustomerTalkButton에 있습니다!

        
    }

    void Bye()
    {

        //audioSource.clip = CoinUp;
        //audioSource.Play();

        float time = 0f;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 23.8f, this.transform.localPosition.z);
            
        }

        

    }

}
