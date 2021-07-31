using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderTimer : MonoBehaviour
{
    private Slider slTimer;
    public float success = 0;
    public float speed = 0.05f;

    private int currcus;
    private GameObject successArea;
    // Start is called before the first frame update
    void Start()
    {
        slTimer = GetComponent<Slider>();
        successArea = GameObject.FindGameObjectWithTag("SuccessArea");

        currcus = GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt;
        //부자 손님이 없을 경우만 손님수 카운트
        if (CustomerManager.tip_money[0] == 0 && CustomerManager.tip_money[1] == 0 && CustomerManager.tip_money[2] == 0) 
        {
            GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt = ++currcus;
        }
        Debug.Log("customer num: " + GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().customerCnt);

        if (currcus <= 5) //1단계
        {
            speed = 0.05f;
        }
        else if (currcus <= 8) //2단계
        {
            speed = 0.067f;
            Debug.Log("level2");
        }
        else if (currcus <= 11) //3단계
        {
            speed = 0.067f;
            successArea.transform.localScale = new Vector3(1, 0.66f, 1);
            Debug.Log("level3");
        }
        else if (currcus <= 15) //4단계
        {
            speed = 0.067f;
            successArea.transform.localScale = new Vector3(1, 0.66f, 1);
            GameObject.Find("Image_success").GetComponent<SuccessArea>().successSpeed = 1.1f;
            Debug.Log("level4");
        }
        else //5단계
        {
            speed = 0.083f;
            successArea.transform.localScale = new Vector3(1, 0.5f, 1);
            GameObject.Find("Image_success").GetComponent<SuccessArea>().successSpeed = 1.1f;
            Debug.Log("level5");
        }
    }

    void Update()
    {

        if (slTimer.value>0.0f)
        {
            slTimer.value -= speed * Time.deltaTime;
            slTimer.value += success;
            success = 0;
        }else
        {
            Debug.Log("Timeup!");
            int life = GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().lifecnt;
            if(life!=0)
            {
                GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().lifecnt -= 1;
            }
            SceneManager.LoadScene("maingameScene");
        }
        
    }
}
