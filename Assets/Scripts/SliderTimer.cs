using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderTimer : MonoBehaviour
{
    private Slider slTimer;
    public float success = 0;
    // Start is called before the first frame update
    void Start()
    {
        slTimer = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(slTimer.value>0.0f)
        {
            slTimer.value -= 0.05f * Time.deltaTime;
            //success = GameObject.Find("Image_line").GetComponent<RhythmBar>().success;
            slTimer.value += success;
            success = 0;
        }else
        {
            Debug.Log("Timeup!");
            GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().lifecnt -= 1;
            Debug.Log("scene mini life" + GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().lifecnt);
            SceneManager.LoadScene("maingameScene");
        }
        
    }
}
