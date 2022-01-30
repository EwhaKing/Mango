using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class GameStory : MonoBehaviour
{
    //public Text text;
    public Image blackBg;
    public Image storyText;
    public GameObject black;
    public GameObject story;
    public GameObject nameScreen;
    public GameObject nameBackground;

    public Image startButton;
    public Image ruleButton;
    public Image shopButton;
    public Image newButton;
    public Text dateText;
    public TextMeshProUGUI textUsername;


    void Start()
    {
        nameScreen.SetActive(false);

        if (!File.Exists(Application.persistentDataPath + "/GameData.json"))
        {
            Debug.Log("게임 첫번째 실행 o"); 

            story.SetActive(true);
            black.SetActive(true);

            StartCoroutine(FadeTextToFullAlpha());
        }
        else
        {
            Debug.Log("게임 첫번째 실행 x");

            story.SetActive(false);
            black.SetActive(false);

            StartCoroutine(FadeTextToFullAlpha2());
        }



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

        StartCoroutine(openNameScreen());
    }

    //버튼 fade in fade out
    IEnumerator FadeTextToFullAlpha2() // 알파값 0 -> 1
    {
        //Debug.Log("button_view");
        startButton.color = new Color(startButton.color.r, startButton.color.g, startButton.color.b, 0);
        ruleButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, 0);
        shopButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, 0);
        newButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, 0);
        dateText.color = new Color(dateText.color.r, dateText.color.g, dateText.color.b, 0);

        while (startButton.color.a < 1.0f)
        {
            //Debug.Log("Time_" + startButton.color.a);
            startButton.color = new Color(startButton.color.r, startButton.color.g, startButton.color.b, startButton.color.a + (Time.deltaTime / 2.0f));
            ruleButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, ruleButton.color.a + (Time.deltaTime / 2.0f));

            shopButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, ruleButton.color.a + (Time.deltaTime / 2.0f));
            newButton.color = new Color(ruleButton.color.r, ruleButton.color.g, ruleButton.color.b, ruleButton.color.a + (Time.deltaTime / 2.0f));

            dateText.color = new Color(dateText.color.r, dateText.color.g, dateText.color.b, ruleButton.color.a + (Time.deltaTime / 2.0f));

            //yield return new WaitForSecondsRealtime(1);
            yield return null;
        }
        yield return null;
        //yield return new WaitForSeconds(5f);

    }

    IEnumerator openNameScreen()
    {
        nameScreen.SetActive(true);
        nameBackground.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha2());
        yield return null;
    }

}
