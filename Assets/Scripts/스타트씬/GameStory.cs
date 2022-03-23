using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStory : MonoBehaviour
{
    //public Text text;
    public Image background;
    public Image story;
    public Image fake_background;
    public Image curtain;
    public Image caption_back;
    public TextMeshProUGUI textCaption;
    public TextMeshProUGUI textUsername;

    public static int index = 0;
    public int currIndex = 0;
    public static bool isOk = false;
    public List<Sprite> storyImages;
    public List<string> storyCaptions;

    void Awake()
    {
        background.transform.gameObject.SetActive(false);
        story.transform.gameObject.SetActive(false);
        curtain.transform.gameObject.SetActive(false);
        caption_back.transform.gameObject.SetActive(false);
    }
    void Start()
    {
        StartCoroutine(FadeTextToFullAlpha());
    }


    IEnumerator FadeTextToFullAlpha() // 알파값 0 -> 1
    {
        fake_background.color = new Color(fake_background.color.r, fake_background.color.g, fake_background.color.b, 0);
        while (fake_background.color.a < 1.0f)
        {
            fake_background.color = new Color(fake_background.color.r, fake_background.color.g, fake_background.color.b, fake_background.color.a + (Time.deltaTime / 3.0f));
            yield return null;

        }
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(readystory());
    }

    IEnumerator readystory()
    {
        background.transform.gameObject.SetActive(true);
        story.transform.gameObject.SetActive(true);
        curtain.transform.gameObject.SetActive(true);
        fake_background.transform.gameObject.SetActive(false);
        StartCoroutine(FadeCurtainToZeroAlpha()); 
        yield return null;
    }

    IEnumerator FadeCurtainToZeroAlpha()  // 알파값 1 -> 0
    {
        curtain.color = new Color(curtain.color.r, curtain.color.g, curtain.color.b, 1);

        while (curtain.color.a > 0.0f)
        {
            curtain.color = new Color(curtain.color.r, curtain.color.g, curtain.color.b, curtain.color.a - (Time.deltaTime));
            yield return null;
        }

        StartCoroutine(ShowStoryImage());
    }

    IEnumerator ShowStoryImage() // 알파값 0 -> 1
    {
        caption_back.transform.gameObject.SetActive(true);
        changeCaption(index);

        yield return new WaitForSeconds(1f);
        isOk = true;
    }

    IEnumerator ChangeStoryImage()  // 알파값 1 -> 0
    {
        if (currIndex == 8)
        {
            caption_back.transform.gameObject.SetActive(false);
            textCaption.transform.gameObject.SetActive(false);
            StartCoroutine(FadeCurtainToFullAlpha());
        }
        else if (currIndex < 8)
        {
            story.sprite = storyImages[currIndex];
            changeCaption(currIndex);
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator FadeCurtainToFullAlpha()  // 알파값 1 -> 0
    {
        curtain.color = new Color(curtain.color.r, curtain.color.g, curtain.color.b, 0);

        while (curtain.color.a < 1.0f)
        {
            curtain.color = new Color(curtain.color.r, curtain.color.g, curtain.color.b, curtain.color.a + (Time.deltaTime));
            yield return null;
        }

        StartCoroutine(readyEndstory());
    }

    IEnumerator readyEndstory()
    {
        background.transform.gameObject.SetActive(false);
        story.transform.gameObject.SetActive(false);
        curtain.transform.gameObject.SetActive(false);
        fake_background.transform.gameObject.SetActive(true);
        StartCoroutine(FadeTextToZeroAlpha());
        yield return null;
    }

    IEnumerator FadeTextToZeroAlpha()  // 알파값 1 -> 0
    {
        fake_background.color = new Color(fake_background.color.r, fake_background.color.g, fake_background.color.b, 1);

        while (fake_background.color.a > 0.0f)
        {
            fake_background.color = new Color(fake_background.color.r, fake_background.color.g, fake_background.color.b, fake_background.color.a - (Time.deltaTime / 3.0f));
            yield return null;
        }
        StartCoroutine(openNameScreen());
    }

    IEnumerator openNameScreen()
    {
        SceneManager.LoadScene("usernameScene");
        yield return null;
    }

    public void changeCaption(int index)
    {
        textCaption.text = storyCaptions[index];
    }

    void Update()
    {
        if (currIndex < index)
        {
            currIndex = index;
            StartCoroutine(ChangeStoryImage());
        }
    }
}
