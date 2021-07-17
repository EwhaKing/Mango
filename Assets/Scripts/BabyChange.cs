using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyChange : MonoBehaviour
{
    public GameObject Baby;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Baby.SetActive(false);
            Handheld.Vibrate();
            Debug.Log("+++ TOUCH START +++");
        } 
        else if(Input.GetMouseButton(0))
        {
            Baby.SetActive(false);
            Debug.Log("+++ TOUCHING +++");
        }
    }
}
