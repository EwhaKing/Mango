using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager_mini : MonoBehaviour
{
    //public int lifecnt = 3;
    public int customerCnt = 0;
    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("LifeManager_mini"));
    }
  

    // Update is called once per frame
    void Update()
    {
       
    }
}
