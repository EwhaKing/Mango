using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ByeBye : MonoBehaviour
{
    int byeValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            byeValue++;
            if (!IsInvoking("disable_doubleClick"))
            {
                Invoke("disable_doubleClick", 0.5f);
            }  
        }

        if (byeValue == 2)
        {
            CancelInvoke("disable_doubleClick");
            GameObject.Find("bye_popup_big").transform.GetChild(0).gameObject.SetActive(true);

            byeValue = 0;
        }
    }

    void disable_doubleClick()
    {
        byeValue = 0;
    }

    public void realQuit()
    {
        Debug.LogWarning("게임 종료됨");
        Application.Quit();
    }

    public void cancelQuit()
    {
        GameObject.Find("bye_popup_big").transform.GetChild(0).gameObject.SetActive(false);
    }
}
