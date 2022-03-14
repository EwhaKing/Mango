using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNewIcon : MonoBehaviour
{
    // Start is called before the first frame update
    public Image recipe_new;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < TeaDataScript.teaDex.item.Length; i++)
        {
            if (TeaDataScript.teaDex.item[i].is_read == true)
            {
                recipe_new.transform.gameObject.SetActive(true);
                break;
            }
            else
            {
                recipe_new.transform.gameObject.SetActive(false);
            }
        }
        
    }
}
