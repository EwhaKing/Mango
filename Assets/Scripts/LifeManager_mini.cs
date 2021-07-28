using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager_mini : MonoBehaviour
{
    public int lifecnt = 3;

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("LifeManager_mini"));
        Debug.Log("scene mini life" + lifecnt);
    }

   

    // Update is called once per frame
    void Update()
    {

    }
}
