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
        ruleText.SetActive(true);
    }

    public void OnClickGameRealRuleExit()
    {
        ruleText.SetActive(false);
    }
}
