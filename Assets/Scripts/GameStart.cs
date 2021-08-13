using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    //public Text text;
    public Image blackBg;
    public Image storyText;
    public GameObject black;
    public GameObject story;

    public Image startButton;
    public Image ruleButton;


    void Start()
    {
        //text = GetComponent<Text>();

        StartCoroutine(FadeTextToFullAlpha());
    }

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
            yield return null;
        }
        
        story.SetActive(false);
        black.SetActive(false);

        StartCoroutine(FadeTextToFullAlpha2());
    }

    IEnumerator FadeTextToFullAlpha2() // 알파값 0 -> 1
    {
        startButton.color = new Color(startButton.color.r, startButton.color.g, startButton.color.b, 0);
        ruleButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, 0);

        while (startButton.color.a < 1.0f)
        {
            startButton.color = new Color(startButton.color.r, startButton.color.g, startButton.color.b, startButton.color.a + (Time.deltaTime / 3.0f));
            ruleButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, ruleButton.color.a + (Time.deltaTime / 3.0f));
            yield return null;
        }

        //yield return new WaitForSeconds(5f);

    }

    public void OnClickGameRealStart()
    {
        ButtonSound._buttonInstance.onButtonAudio();

        SceneManager.LoadScene("maingameScene");

        //Debug.Log("버튼 작동 잘 되니?");
    }

    
}
