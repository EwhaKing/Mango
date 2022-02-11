using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSuccessText : MonoBehaviour
{
    private float textPos = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float updatedTouchPos = GameObject.Find("Image_line").GetComponent<RhythmBar>().touchPos;
        if(textPos != updatedTouchPos)
        {
            textPos = updatedTouchPos;
            showTextEffect();
        }
    }

    void showTextEffect()
    {

    }
}
