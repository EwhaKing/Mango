using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropPF : MonoBehaviour
{
    public Image Basic;
    public Sprite Success;
    public Sprite Fail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Basic.sprite = Success;
        }

        else
        {
            Basic.sprite = Fail;
        }
    }
}
