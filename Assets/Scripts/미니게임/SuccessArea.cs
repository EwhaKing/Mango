using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessArea : MonoBehaviour
{

    public float successSpeed = 0;
    private float currsuccessArea;
    private float currSuccessHeight;
    float MaxPos = 140f;
    float MinPos = -355f;

    private float successPos;
    // Start is called before the first frame update
    void Start()
    {
        successPos = this.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        currsuccessArea = this.transform.localPosition.y;
        currSuccessHeight = this.GetComponent<RectTransform>().rect.height;
        GameObject.Find("Image_line").GetComponent<RhythmBar>().MaxSuccess = currsuccessArea + (currSuccessHeight / 2f);
        GameObject.Find("Image_line").GetComponent<RhythmBar>().MinSuccess = currsuccessArea - (currSuccessHeight / 2f);
        
        Debug.Log("SuccessArea: " + currsuccessArea);

        successPos = successSpeed * Time.deltaTime;
        this.transform.Translate(new Vector3(0, successPos, 0));

        //성공영역 움직이는 부분
        if (this.transform.localPosition.y >= MaxPos)
        {
            successSpeed *= -1;
            transform.localPosition = new Vector3(transform.localPosition.x, MaxPos);
        }
        else if (this.transform.localPosition.y <= MinPos)
        {
            successSpeed *= -1;
            transform.localPosition = new Vector3(transform.localPosition.x, MinPos);
        }
    }
}
