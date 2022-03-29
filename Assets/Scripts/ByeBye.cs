using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ByeBye : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameObject.Find("bye_popup_big").transform.GetChild(0).gameObject.SetActive(true);
        }   
    }

    public void realQuit()
    {
        Debug.LogWarning("게임 종료됨");
        Application.Quit();
    }

}
