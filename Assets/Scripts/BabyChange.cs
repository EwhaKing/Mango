using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyChange : MonoBehaviour
{
    public GameObject Baby;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(Baby);
            Handheld.Vibrate();
            Debug.Log("+++ TOUCH START +++");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("--- TOUCH END ---");
        }
    }
}
