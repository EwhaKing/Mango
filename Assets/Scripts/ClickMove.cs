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
        Destroy(gameObject);
    }

}
