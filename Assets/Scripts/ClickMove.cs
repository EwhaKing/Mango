using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClickMove : MonoBehaviour
{
    int speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yMove = speed * Time.deltaTime; //속도 설정
        this.transform.Translate(new Vector3(0, yMove, 0));
        Invoke("Bye", 0.2f);
    }

    void Bye()
    {
        float time = 0f;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 48.9f, this.transform.localPosition.z);
        }
    }

}
