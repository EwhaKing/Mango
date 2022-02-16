using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOutAnimation : MonoBehaviour
{
    float time;
    public float _fadeTime = 2f;
    Vector3 dir = new Vector3(0, 1f, 0).normalized;

    //start와 awake는 최초 1회만 호출
    //onenable은 setactive(true)될때마다 호출!!!
    void OnEnable()
    {
        resetAnim();
    }
    void Update()
    {
        

        if (time < _fadeTime)
        {
            transform.Translate(dir * Time.deltaTime * 3);
            this.GetComponent<TextMeshProUGUI>().color = new Color(0.345098f, 0.2745098f, 0.1686275f, 1f - time / _fadeTime);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        time += Time.deltaTime;
        Debug.Log("time: " + time);
    }

    public void resetAnim()
    {
        time = 0;  _fadeTime = 2f;
        this.GetComponent<TextMeshProUGUI>().color = new Color(0.345098f, 0.2745098f, 0.1686275f, 1f);
    }
}
