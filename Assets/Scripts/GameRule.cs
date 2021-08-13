using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRule : MonoBehaviour
{

    public GameObject ruleText;
    //public GameObject ruleExit;

    // Start is called before the first frame update
    void Start()
    {
        //ruleText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickGameRealRule()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        ruleText.SetActive(true);
    }

    public void OnClickGameRealRuleExit()
    {
        ButtonSound._buttonInstance.onButtonAudio();
        ruleText.SetActive(false);
    }
}
