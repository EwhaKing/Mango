using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaDrop : MonoBehaviour
{
    int speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yMove = speed * Time.deltaTime;
        this.transform.Translate(new Vector3(0, -yMove, 0));
    }
}
