using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBar : MonoBehaviour
{
    float MaxPos = 160f;
    float MinPos = -410f;
    float speed = 15f;
    float currPos;
    // Start is called before the first frame update
    void Start()
    {
        currPos = this.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        currPos = speed * Time.deltaTime;
        this.transform.Translate(new Vector3(0, currPos, 0));
        if (this.transform.localPosition.y >= MaxPos)
        {
            speed *= -1;
            transform.localPosition = new Vector3(695, MaxPos);
        } 
        else if(this.transform.localPosition.y <= MinPos)
        {
            speed *= -1;
            transform.localPosition = new Vector3(695, MinPos);
        }
    }
}
