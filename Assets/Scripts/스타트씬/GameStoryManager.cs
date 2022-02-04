using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStoryManager : MonoBehaviour
{
    public Image blackBg;
    public Image storyText;
    public GameObject black;
    public GameObject story;

    void Start()
    {
        StartCoroutine(FadeTextToFullAlpha());
    }

    //스토리 fade in fade out
    IEnumerator FadeTextToFullAlpha() // 알파값 0 -> 1
    {
        //text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        storyText.color = new Color(storyText.color.r, storyText.color.g, storyText.color.b, 0);

        while (storyText.color.a < 1.0f)
        {
            //text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            storyText.color = new Color(storyText.color.r, storyText.color.g, storyText.color.b, storyText.color.a + (Time.deltaTime / 3.0f));
            yield return null;

        }

        yield return new WaitForSeconds(5f);

        StartCoroutine(FadeTextToZeroAlpha());
    }

    IEnumerator FadeTextToZeroAlpha()  // 알파값 1 -> 0
    {
        //text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        storyText.color = new Color(storyText.color.r, storyText.color.g, storyText.color.b, 1);

        while (storyText.color.a > 0.0f)
        {
            //text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 2.0f));
            storyText.color = new Color(storyText.color.r, storyText.color.g, storyText.color.b, storyText.color.a - (Time.deltaTime / 3.0f));
            //yield return new WaitForSecondsRealtime(1);
            yield return null;
        }

        story.SetActive(false);
        black.SetActive(false);

        //스토리 모두 진행된 후 스타트 화면
        SceneManager.LoadScene("StartScene");
    }

}
