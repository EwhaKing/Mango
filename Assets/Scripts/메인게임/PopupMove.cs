using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMove : MonoBehaviour
{
    public Image popupImage;
    public Text popupText;

    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeTextToFullAlpha());
    }

    //페이드 인, 아웃
    IEnumerator FadeTextToFullAlpha() // 알파값 0 -> 1
    {
        popupImage.color = new Color(popupImage.color.r, popupImage.color.g, popupImage.color.b, 0);
        popupText.color = new Color(popupText.color.r, popupText.color.g, popupText.color.b, 0);

        while (popupImage.color.a < 1.0f)
        {
            popupImage.color = new Color(popupImage.color.r, popupImage.color.g, popupImage.color.b, popupImage.color.a + (Time.deltaTime / speed));
            popupText.color = new Color(popupText.color.r, popupText.color.g, popupText.color.b, popupText.color.a + (Time.deltaTime / speed));
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeTextToZeroAlpha());
    }

    IEnumerator FadeTextToZeroAlpha()  // 알파값 1 -> 0
    {
        this.GetComponent<Animator>().SetBool("isUp", true);

        Debug.Log("0으로 된다.");

        popupImage.color = new Color(popupImage.color.r, popupImage.color.g, popupImage.color.b, 1f);
        popupText.color = new Color(popupText.color.r, popupText.color.g, popupText.color.b, 1f);

        while (popupImage.color.a > 0.0f)
        {
            Debug.Log("현재 투명도: " + popupImage.color.a);
            popupImage.color = new Color(popupImage.color.r, popupImage.color.g, popupImage.color.b, popupImage.color.a - (Time.deltaTime / speed));
            popupText.color = new Color(popupText.color.r, popupText.color.g, popupText.color.b, popupText.color.a - (Time.deltaTime / speed));
            yield return null;
        }

        yield return null;
    }
}
