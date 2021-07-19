using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BabyChange : MonoBehaviour
{
    public Image Orig;
    public Sprite New;
    public Sprite New2;
//    private float DelayTime = 1f;
//    private float RealTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    
    // Update is called once per frame

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Handheld.Vibrate();
            Orig.sprite = New;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            /*RealTime += Time.deltaTime;
            if (RealTime >= DelayTime)
            {*/
                Orig.sprite = New2;
            //}
        }

    }

}
