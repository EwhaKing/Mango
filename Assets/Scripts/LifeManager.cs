using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int lifecnt = 3;
    public Sprite life_full; //꽉 찬 하트
    public Sprite life_broken; //빈 하트
    
    //오브젝트 리스트 lifeLife 선언
    public GameObject[] lifeLife;

    void Start()
    {
        lifecnt = GameObject.Find("LifeManager_mini").GetComponent<LifeManager_mini>().lifecnt;
        Debug.Log("scene main life" + lifecnt);
        lifeChange();
    }

    public void lifeChange()
    {
        if(lifecnt == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                lifeLife[i].GetComponent<Image>().sprite = life_full;
            }
        }
        else if (lifecnt == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                lifeLife[i].GetComponent<Image>().sprite = life_full;
            }
            lifeLife[2].GetComponent<Image>().sprite = life_broken;
        }
        else if (lifecnt == 1)
        {
            for (int i = 1; i < 3; i++)
            {
                lifeLife[i].GetComponent<Image>().sprite = life_broken;
            }
            lifeLife[0].GetComponent<Image>().sprite = life_full;
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                lifeLife[i].GetComponent<Image>().sprite = life_broken;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
