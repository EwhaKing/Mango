using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBar : MonoBehaviour
{
    float startPos = -104f;
    float MaxPos = 180f;
    float MinPos = -395f;

    float MaxSuccess;
    float MinSuccess;
    float speed = 15f;
    float currPos;

    public float success = 0;
    private int successCnt = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        currPos = this.transform.localPosition.y;
        GameObject successArea = GameObject.FindGameObjectWithTag("SuccessArea");
        float successHeight = successArea.GetComponent<RectTransform>().rect.width;

        MaxSuccess = startPos + (successHeight / 2f);
        MinSuccess = startPos - (successHeight / 2f);
    }

    // Update is called once per frame
    void Update()
    {
        success = 0;
        currPos = speed * Time.deltaTime;
        this.transform.Translate(new Vector3(0, currPos, 0));
        if(Input.GetMouseButtonDown(0))
        {
            float now = this.transform.localPosition.y;
            if ( now >= MinSuccess && now <= MaxSuccess)
            {
                Debug.Log("success pos: " + now);
                success = 0.10f;
            }
            else
            {
                Debug.Log("fail pos: " + now);
                success = -0.05f;
            }
        }
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
